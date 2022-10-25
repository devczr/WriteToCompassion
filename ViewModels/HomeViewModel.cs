
using CommunityToolkit.Maui.Alerts;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using WriteToCompassion.Models;
using WriteToCompassion.Services.Thoughts;
using WriteToCompassion.Views;

namespace WriteToCompassion.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
    //HomeView grid bindable layout bound to this Clouds observable collection
    //number of clouds visible = Clouds.Count
    public ObservableCollection<Cloud> Clouds { get; set; } = new();


    //Read & unread thoughts retrieved from db via thoughtsservice
    public List<Thought> UserThoughts { get; } = new();

    public List<Thought> SortedThoughts { get; } = new();


    //handles database logic
    ThoughtsService thoughtsService;

    //holds user preferences
    private readonly ISettingsService settingsService;



    //User can adjust in Settings
    [ObservableProperty]
    double cloudScale = 1;

    [ObservableProperty]
    int maxClouds = 5;

    private bool instantText = false;

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
            //delaying here due to OnSizeAllocated firing 3 times on app startup
            //also gives time for the page to load in between NavigatedTo and actual page visiblity
            //            await Task.Delay(1500);
            await InitDriftAsync();
            IsBusy = false;
        }
    }

    //Add to Clouds collection which signals bindable layout to spawn CustomCloudControls
    [RelayCommand]
    async Task InitDriftAsync()
    {
        await Task.Delay(500);
        

        //the number of clouds added to Clouds collection depends on:
        // 1) how many thoughts the user has saved in database
        // 2) their MaxClouds setting
        // 3) their "Unread Only" or "All" setting
        //    a thought is considered Unread if its content has never been displayed (swipe up gesture)

        int cloudCount = Math.Clamp(UserThoughts.Count, 0, MaxClouds);

        for (int i = 0; i < cloudCount; i++)
        {
            Cloud c = new()
            {
                AnimationType = CloudAnimationType.Drift,
                CloudID = Guid.NewGuid()
            };
            Clouds.Add(c);
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

            var index = await Task.Run(() => Clouds.IndexOf(swipedCloud));
            
            Clouds[index].AnimationType = CloudAnimationType.Display;
            
            if (instantText)
                await UpdateContentInstantly(index);
            else
                await UpdateContent(index);


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
            await thoughtsService.UpdateThought(thought);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error",
                $"Trouble updating read count with database {ex.Message}", "OK");
        }
    }


    // Updates xaml label
    //Content is chosen simply by matching the index of the list with the collection
    //TODO: Randomize how a Thought is chosen when cloud is swiped
    private async Task UpdateContent(int index)
    {
        StringBuilder sb = new();

        var charArray = await Task.Run(() => UserThoughts[index].Content.ToCharArray());

        for(int i = 0; i < charArray.Length; i++)
        {
            sb.Append(charArray[i]);
            await Task.Delay(25);
            CloudContent = sb.ToString();
        }

        await UpdateReadCount(UserThoughts[index]);

    }

    private async Task UpdateContentInstantly(int index)
    {

        CloudContent = await Task.Run(() => UserThoughts[index].Content);

        await UpdateReadCount(UserThoughts[index]);
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
        await Shell.Current.GoToAsync(nameof(LibraryView));
    }

    [RelayCommand]
    async Task ClearCloudsAsync()
    {
        for (int i = 0; i < Clouds.Count; i++)
        {
            Clouds[i].AnimationType = CloudAnimationType.None;
        }
        Clouds.Clear();
        GetUserThoughtsAsync();
    }

}