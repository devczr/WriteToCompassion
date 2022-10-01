using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WriteToCompassion.Services.Settings;
using WriteToCompassion.Services.Navigation;
using WriteToCompassion.Views;
using WriteToCompassion.Models;
using WriteToCompassion.Services;
using WriteToCompassion.Services.Thoughts;
using System.Collections.ObjectModel;
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

    [ObservableProperty]
    bool myBool;


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

    //Fade out lottie animation & fade in imagebutton
    [RelayCommand]
    async Task LottieLoadSuccess()
    {

    }

    [RelayCommand]
    async Task LottieLoadFailure()
    {

    }

    [RelayCommand]
    async Task CancelAnimationFromTap()
    {

    }


    [RelayCommand]
    async Task GoToMainPageAsync()
    {
        await Shell.Current.GoToAsync(nameof(MainPage));
    }



}

