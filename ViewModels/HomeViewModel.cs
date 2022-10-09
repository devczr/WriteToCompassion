
using CommunityToolkit.Maui.Alerts;
using System.Collections.ObjectModel;
using WriteToCompassion.Models;
using WriteToCompassion.Services.Thoughts;
using WriteToCompassion.Views;

namespace WriteToCompassion.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
    public ObservableCollection<CustomCloudControl> CloudControls { get; set; } = new();

    public ObservableCollection<Thought> UnreadThoughts { get; } = new();

    public ObservableCollection<Thought> AllThoughts { get; } = new();


    ThoughtsService thoughtsService;

    
    [ObservableProperty]
    bool animateCloudLottie = false;

    [ObservableProperty]
    double cloudScale = 0.5;

    [ObservableProperty]
    int maxClouds = 7;

    [ObservableProperty]
    bool unreadOnly = true;

    [ObservableProperty]
    string dragText = "";

    [ObservableProperty]
    bool cloudTextVisible;

    #region CloudAnimationTypes for MVVM Bindings

    private CloudAnimationType _cloud1Animation;
    public CloudAnimationType Cloud1Animation
    {
        get => _cloud1Animation;
        set
        {
            if (_cloud1Animation != value)
            {
                _cloud1Animation = value;
                OnPropertyChanged();

            }

        }
    }

    private CloudAnimationType _cloud2Animation;
    public CloudAnimationType Cloud2Animation
    {
        get => _cloud2Animation;
        set
        {
            if (_cloud2Animation != value)
            {
                _cloud2Animation = value;
                OnPropertyChanged();

            }

        }
    }

    private CloudAnimationType _cloud3Animation;
    public CloudAnimationType Cloud3Animation
    {
        get => _cloud3Animation;
        set
        {
            if (_cloud3Animation != value)
            {
                _cloud3Animation = value;
                OnPropertyChanged();

            }

        }
    }

    private CloudAnimationType _cloud4Animation;
    public CloudAnimationType Cloud4Animation
    {
        get => _cloud4Animation;
        set
        {
            if (_cloud4Animation != value)
            {
                _cloud4Animation = value;
                OnPropertyChanged();

            }

        }
    }

    private CloudAnimationType _cloud5Animation;
    public CloudAnimationType Cloud5Animation
    {
        get => _cloud5Animation;
        set
        {
            if (_cloud5Animation != value)
            {
                _cloud5Animation = value;
                OnPropertyChanged();

            }

        }
    }

    #endregion


    public HomeViewModel(ThoughtsService thoughtsService, ISettingsService settingsService)
            : base(settingsService)
    {
        this.thoughtsService = thoughtsService;
    }

    [RelayCommand]
    async Task GetAllThoughtsAsync()
    {
        try
        {
            //viewmodel calls into Service so our Database Logic isn't locked into our ViewModel
            var thoughts = await thoughtsService.GetAllThoughts();

            foreach (var thought in thoughts)
                AllThoughts.Add(thought);

            thoughts = thoughts.Where(t => t.ReadCount == 0).ToList();
            
            UnreadThoughts.Clear();
            
            foreach (var thought in thoughts)
                UnreadThoughts.Add(thought);

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error",
                $"Unable to get your saved notes: {ex.Message}", "OK");
        }
        finally
        {
            await InitDriftAsync();
        }
    }


    [RelayCommand]
    async Task InitDriftAsync()
    {
        int cloudCount;

        if (UnreadOnly)
            cloudCount = Math.Clamp(UnreadThoughts.Count, 0, MaxClouds);
        else
            cloudCount = Math.Clamp(AllThoughts.Count, 0, MaxClouds);


        for (int i = 0; i < cloudCount; i++)
        {
            CustomCloudControl c = new()
            {
                CloudAnimation = CloudAnimationType.Drift
            };
            CloudControls.Add(c);
        }
    }

    [RelayCommand]
    async Task DanceAsync(CustomCloudControl customCloudControl)
    {
        if (customCloudControl is null)
            return;

        int index = CloudControls.IndexOf(customCloudControl);
        CloudControls[index].CloudAnimation = CloudAnimationType.Dance;
    }


    [RelayCommand]
    async Task CloudSwipedAsync(CustomCloudControl customCloudControl)
    {
        if (customCloudControl is null)
            return;

        int index = CloudControls.IndexOf(customCloudControl);
        await Shell.Current.DisplayAlert($"Swipe up--- {index}", "cloud command", "ok");
    }




    //navigation
    [RelayCommand]
    async Task GoToSettingsAsync()
    {
        await Shell.Current.GoToAsync(nameof(SettingsView));
    }

    [RelayCommand]
    async Task GoToNewThoughtEditorAsync()
    {
        await Shell.Current.GoToAsync("//root/newthought");
    }

}