using WriteToCompassion.Views;
using WriteToCompassion.Services.Thoughts;
using System.Collections.ObjectModel;


namespace WriteToCompassion.ViewModels;
public partial class ThoughtsViewModel : BaseViewModel
{
    ThoughtsService thoughtsService;

    [ObservableProperty]
    ObservableCollection<Thought> thoughts;

    [ObservableProperty]
    string popText;

    [ObservableProperty]
    bool animateCloudLottie = false;

    [ObservableProperty]
    double cloudScale = 0.5;

    #region CloudAnimationTypes for MVVM Bindings
    private CloudAnimationType _cloudAnimation;
    public CloudAnimationType CloudAnimation
    {
        get => _cloudAnimation;
        set
        {
            if (_cloudAnimation != value)
            {
                _cloudAnimation = value;
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


    public ThoughtsViewModel(ThoughtsService thoughtsService, ISettingsService settingsService)
            : base(settingsService)
    {
        this.thoughtsService = thoughtsService;
        Thoughts = new ObservableCollection<Thought>();
    }

    [RelayCommand]
    async Task GetAllThoughtsAsync()
    {
        try
        {
            //viewmodel calls into Service so our Database Logic isn't locked into our ViewModel
            var thoughts = await thoughtsService.GetAllThoughts();

            Thoughts.Clear();

            foreach (var thought in thoughts)
                Thoughts.Add(thought);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error",
                $"Unable to get your saved notes: {ex.Message}", "OK");
        }
    }


    [RelayCommand]
    async Task GoToSettings()
    {
        await Shell.Current.GoToAsync(nameof(SettingsView));
    }

    [RelayCommand]
    async Task GoToNewThoughtEditorAsync()
    {
        await Shell.Current.GoToAsync(nameof(NewThoughtEditorView));
    }


    [RelayCommand]
    async Task DriftCloud()
    {
        CloudAnimation = CloudAnimationType.Drift;
        Cloud2Animation = CloudAnimationType.Drift;
    }

    [RelayCommand]
    async Task HoverCloud()
    {
/*        cloud1Animation = CloudAnimationType.Hover;
        cloud2Animation = CloudAnimationType.Hover;
        await Shell.Current.DisplaySnackbar("hover");*/
    }

    [RelayCommand]
    async Task CancelCloud()
    {
        CloudAnimation = CloudAnimationType.None;
        Cloud2Animation = CloudAnimationType.None;
/*        cloud1Animation = CloudAnimationType.None;
        cloud2Animation = CloudAnimationType.None;*/
    }
}

