using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WriteToCompassion.Models;
using WriteToCompassion.Services;
using WriteToCompassion.Services.Settings;
using WriteToCompassion.Services.Thoughts;
using WriteToCompassion.Views;

namespace WriteToCompassion.ViewModels;

public partial class SettingsViewModel : BaseViewModel
{
    private readonly ISettingsService settingsService;
    private readonly ThoughtsService thoughtService;

    public ObservableCollection<Thought> Thoughts { get; } = new();

    [ObservableProperty]
    string entryText;


    public SettingsViewModel(ISettingsService settingsService, ThoughtsService thoughtsService) : base(settingsService)
    {
        this.settingsService = settingsService;
        this.thoughtService = thoughtsService;
        Title = "Settings";
    }

    [RelayCommand]
    private void ToggleDisplayTutorial()
    {
        if (settingsService.DisplayTutorial)
        {
            settingsService.DisplayTutorial = false;
            Shell.Current.DisplayAlert("Tutorial Disabled", "Preference saved. Tutorial will not show on app startup.", "OK");
        }

        else
        {
            settingsService.DisplayTutorial = true;
            Shell.Current.DisplayAlert("Tutorial Enabled", "Preference saved. Tutorial will show on next app startup.", "OK");
        }
    }

    [RelayCommand]
    async Task GoToThoughtsAsync()
    {
        await Shell.Current.GoToAsync(nameof(ThoughtsPage));
    }

    [RelayCommand]
    async Task GetAllThoughtsAsync()
    {
        try
        {
            IsBusy = true;
            //viewmodel calls into Service so our Database Logic isn't locked into our ViewModel
            var thoughts = await thoughtService.GetAllThoughts();

            Thoughts.Clear();

            foreach (var thought in thoughts)
                Thoughts.Add(thought);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error",
                $"Unable to get your saved thoughts: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
