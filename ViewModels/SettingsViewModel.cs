using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WriteToCompassion.Models;
using WriteToCompassion.Services;
using WriteToCompassion.Services.Settings;
using WriteToCompassion.Services.Thoughts;
using WriteToCompassion.Views;
using WriteToCompassion.Views.Popups;
using CommunityToolkit.Maui.Alerts;

namespace WriteToCompassion.ViewModels;

public partial class SettingsViewModel : BaseViewModel
{
    private readonly ISettingsService settingsService;
    private readonly ThoughtsService thoughtService;

    public ObservableCollection<Thought> Thoughts { get; } = new();

    [ObservableProperty]
    string entryEditorText;

    [ObservableProperty]
    string thoughtsCount;


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
    async Task GoToEditor(Thought thought)
    {
        if (thought is null)
            return;

        await Shell.Current.GoToAsync(nameof(EditorView), true, new Dictionary<string, object>
        {
            {"Thought", thought }
        });
    }



    [RelayCommand]
    async Task GoToThoughtsAsync()
    {
        await Shell.Current.GoToAsync(nameof(ThoughtsPage));
    }


    [RelayCommand]
    async Task EditThought(Thought thought)
    {
        if (thought == null)
        {
            await Shell.Current.DisplayAlert("Unable to edit thought", "Null reference", "OK");
            return;
        }
        EntryEditorText = thought.Content;
        Shell.Current.DisplaySnackbar(EntryEditorText);
    }

    public void UpdateEntryEditText(string id)
    {
        Shell.Current.DisplaySnackbar(id);
    }


    [RelayCommand]
    async Task ProcessPopupChanges(Thought thought)
    {
        if (thought == null)
        {
            await Shell.Current.DisplayAlert("Tnull", "ok", "OK");
            return;
        }

        var monkeyID = thought.Id.ToString();
        var monkeyContent = thought.Content;
        await Shell.Current.DisplayAlert(monkeyID, monkeyContent, "OK");
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
