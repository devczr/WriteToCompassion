namespace WriteToCompassion.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
    static Page Page => Application.Current?.MainPage ?? throw new NullReferenceException();

    //HomeView grid bindable layout bound to this Clouds observable collection
    //number of clouds visible = Clouds.Count
    public ObservableCollection<Cloud> Clouds { get; set; } = new();


    //Read & unread thoughts retrieved from db via thoughtsservice
    public List<Thought> UserThoughts { get; } = new();

    public List<Thought> SortedThoughts { get; set; } = new();


    //handles database logic
    ThoughtsService thoughtsService;

    //holds user preferences
    private readonly ISettingsService settingsService;

    private int currentCloudIndex = 0;

    //User can adjust in Settings
    [ObservableProperty]
    double cloudScale = 1;

    [ObservableProperty]
    int maxClouds = 5;

    private bool instantText = false;

    [ObservableProperty]
    bool displayTutorialPopups;

    //general XAML Bindings
    [ObservableProperty]
    bool animateCloudLottie = false;

    [ObservableProperty]
    string cloudContent = "";

    [ObservableProperty]
    bool contentBoxBusy;

    public HomeViewModel(ThoughtsService thoughtsService, ISettingsService settingsService)
            : base(settingsService)
    {
        this.settingsService = settingsService;
        this.thoughtsService = thoughtsService;
        cloudScale = settingsService.CloudScale;
        maxClouds = settingsService.MaxClouds;
        instantText = settingsService.InstantText;
        displayTutorialPopups = settingsService.DisplayTutorial;
        CheckTutorial();
    }

    async void CheckTutorial()
    {
        if(settingsService.DisplayTutorial)
        {
            var mainTutPopup = new TutorialPopup();
           var result = await Page.ShowPopupAsync(mainTutPopup);
            if(result is bool boolResult)
            {
                if(boolResult)
                {
                    //skip the rest of tutorial
                }
                else 
                { 
                    //tapped outside or continue
                    var nextTutPopup = new TutorialNewThoughtIconPopup();
                    nextTutPopup.HorizontalOptions = Microsoft.Maui.Primitives.LayoutAlignment.Center;
                    nextTutPopup.VerticalOptions = Microsoft.Maui.Primitives.LayoutAlignment.End;
                    await Page.ShowPopupAsync(nextTutPopup);
                    settingsService.DisplayTutorial = false;
                }
            }
        }
    }

    //mct:EventToCommand behavior on HomeView contentpage behavior calls this when "NavigatedTo" fires
    [RelayCommand]
    async Task GetUserThoughtsAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            //clear list that xaml clouds are bound to
            CloudContent = "";
            Clouds.Clear();
            UserThoughts.Clear();

            //viewmodel calls into Service so our Database Logic isn't locked into our ViewModel
            var thoughts = await thoughtsService.GetThoughts();

                foreach (var thought in thoughts)
                    UserThoughts.Add(thought);

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error",
                $"Unable to get your saved notes: {ex.Message}", "OK");
        }
        finally
        {
            await InitDriftAsync();
            IsBusy = false;
        }
    }

    //Add to Clouds collection which signals bindable layout to spawn CustomCloudControls
    [RelayCommand]
    async Task InitDriftAsync()
    {
        if (UserThoughts.Count <= 0)
            return;

        //the number of clouds added to Clouds collection depends on:
        // 1) how many thoughts the user has saved in database
        // 2) their MaxClouds setting
        // 3) their "Unread Only" or "All" setting
        //    a thought is considered Unread if its content has never been displayed (swipe up gesture)

        await SortAndRandomizeThoughts();

        int cloudCount = Math.Clamp(SortedThoughts.Count, 0, MaxClouds);

        for (int i = 0; i < cloudCount; i++)
        {
            Cloud c = new()
            {
                AnimationType = CloudAnimationType.Drift,
            };
            Clouds.Add(c);
        }
    }

    private async Task SpawnCloudAfterSwipe(int previousCloud)
    {
        Clouds.RemoveAt(previousCloud);
        SortedThoughts.RemoveAt(previousCloud);

        if (SortedThoughts.Count > Clouds.Count)
        {
            Cloud c = new()
            {
                AnimationType = CloudAnimationType.Drift,
            };
            Clouds.Add(c);
        }
        else if((SortedThoughts.Count <= Clouds.Count) && (Clouds.Count > 0))
        {
            return;
        }
        else
        {
            SortedThoughts.Clear();
            SessionService.GenSessionID();
            await ShortToast("Nice! All thoughts have been read. Repeating list.");
            await InitDriftAsync();
        }
    }

    // Cloud Animations

    /* CustomCloudControls have a CloudAnimation enum property bound to a matching type on the Clouds collection
     all UI changes in CustomCloudControl animations are based on changes to the BindableProperty CloudAnimationProperty
     animations and properties are defined in the CustomCloudControl codebehind
     Any change in animation must be made by changing the [ObservableProperty] animationType in the Cloud model
     changing animation without raising Cloud model inotifypropertychanged may desynch the relationship between View & ViewModel properties*/
    [RelayCommand]
    async Task DanceAsync(Cloud cloudToDance)
    {
        var index = await Task.Run(() => Clouds.IndexOf(cloudToDance));

        Clouds[index].AnimationType = CloudAnimationType.Dance;
    }

    [RelayCommand]
    async Task HoverToDriftAsync(Cloud cloudToDance)
    {
        var index = await Task.Run(() => Clouds.IndexOf(cloudToDance));

        Clouds[index].AnimationType = CloudAnimationType.HoverToDrift;
    }


    // Swipe Up
    [RelayCommand]
    async Task CloudSwipedAsync(Cloud swipedCloud)
    {
        if ((swipedCloud is null) || (ContentBoxBusy))
            return;

        try
        {
            ContentBoxBusy = true;

            currentCloudIndex = await Task.Run(() => Clouds.IndexOf(swipedCloud));
            
            Clouds[currentCloudIndex].AnimationType = CloudAnimationType.Display;
            
            if (instantText)
                await UpdateContentInstantly(currentCloudIndex);
            else
                await UpdateContent(currentCloudIndex);

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error",
                $"Unable to display your thought: {ex.Message}", "OK");
        }
        finally
        {
            ContentBoxBusy = false;
        }
    }

    private async Task UpdateReadCount(Thought thought)
    {
        try
        {
            thought.ReadCount++;
            thought.MostRecentReadSessionID = SessionService.SessionID;
            await thoughtsService.UpdateThought(thought);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error",
                $"Trouble updating read count with database {ex.Message}", "OK");
        }
    }


    private async Task SortAndRandomizeThoughts()
    {
        var x = UserThoughts.SkipWhile(t => t.MostRecentReadSessionID == SessionService.SessionID).ToList();
        SortedThoughts = ShuffleService.FYShuffle(x);
    }

    private async Task UpdateContent(int index)
    {
        StringBuilder sb = new();

        var charArray = await Task.Run(() => SortedThoughts[index].Content.ToCharArray());

        for(int i = 0; i < charArray.Length; i++)
        {
            sb.Append(charArray[i]);
            await Task.Delay(25);
            CloudContent = sb.ToString();
        }

        await UpdateReadCount(SortedThoughts[index]);
        SpawnCloudAfterSwipe(currentCloudIndex);
    }

    private async Task UpdateContentInstantly(int index)
    {

        CloudContent = await Task.Run(() => SortedThoughts[index].Content);

        await UpdateReadCount(SortedThoughts[index]);

        await Task.Delay(1500);
        SpawnCloudAfterSwipe(currentCloudIndex);
    }


    //Navigation
    [RelayCommand]
    async Task GoToNewThoughtEditorAsync()
    {
        await Shell.Current.GoToAsync(nameof(NewThoughtEditorView), true);
    }

    [RelayCommand]
    async Task GoToLibraryAsync()
    {
        await Shell.Current.GoToAsync(nameof(LibraryView), true);
    }


}