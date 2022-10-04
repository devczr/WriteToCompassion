using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WriteToCompassion.Services.Settings;
using WriteToCompassion.Services.Navigation;
using WriteToCompassion.Views;
using WriteToCompassion.Models;
using WriteToCompassion.Services;
using WriteToCompassion.Services.Thoughts;
using System.Collections.ObjectModel;
using WriteToCompassion.Controls;
using CommunityToolkit.Maui.Alerts;
using System.Windows.Input;

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


    public ICommand TriggerAnimationCommand { get; set; }

    public ThoughtsViewModel(ThoughtsService thoughtsService, INavigationService navigationService, ISettingsService settingsService)
            : base(navigationService, settingsService)
    {
        this.thoughtsService = thoughtsService;
        Thoughts = new ObservableCollection<Thought>();
    }


    [RelayCommand]
    async Task AddThoughtAsync()
    {
        int result = 0;

        try
        {
            IsBusy = true;
            //viewmodel calls into Service so our Database Logic isn't locked into our ViewModel

            if (string.IsNullOrEmpty(PopText))
                return;

            await thoughtsService.AddThought(PopText);

            PopText = string.Empty;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error",
                $"Unable to get save note: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
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
    async Task GoToSettingsAsync()
    {
        await Shell.Current.GoToAsync(nameof(SettingsView));
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


    [RelayCommand]
    async Task GoToMainPageAsync()
    {
        await Shell.Current.GoToAsync(nameof(MainPage));
    }



}

