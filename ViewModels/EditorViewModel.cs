using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using WriteToCompassion.Services.Thoughts;
using WriteToCompassion.Views;
using WriteToCompassion.Views.Popups;

namespace WriteToCompassion.ViewModels;

[QueryProperty(nameof(Thought), "Thought")]
public partial class EditorViewModel : BaseViewModel
{
    ThoughtsService thoughtsService;

    [ObservableProperty]
    Thought thought;

    private string originalContent;

    public EditorViewModel(ISettingsService settingsService, ThoughtsService thoughtsService) : base(settingsService)
    {
        this.thoughtsService = thoughtsService;
    }

    [RelayCommand]
    async Task DeleteThoughtAsync()
    {
        var result = await Shell.Current.DisplayAlert("Delete Thought?", "This cannot be undone.", "Delete", "Cancel");

        if (result)
        {
            int id = Thought.Id;
            await thoughtsService.DeleteThought(id);
            await Shell.Current.GoToAsync("//root/library");
        }
        else return;
    }

    [RelayCommand]
    async Task CancelAsync()
    {
        if (!string.Equals(originalContent, Thought.Content))
            await DiscardOrSaveAsync();

        else
            await Shell.Current.GoToAsync("//root/library");
    }

    async Task DiscardOrSaveAsync()
    {
        var result = await Shell.Current.DisplayAlert("Save your changes?", null, "Save", "Discard");

        if (result)
        {
            await thoughtsService.UpdateThought(Thought);
            await NavigateToLibraryAsync();
            await ShortToast("Edit saved.");
        }
        else
        {
            await NavigateToLibraryAsync();
            await ShortToast("Edit discarded.");
        }

    }


    [RelayCommand]
    async Task UpdateThoughtContent(string newText)
    {
        await Task.Run(() => Thought.Content = newText);
    }


    // Thought QueryProperty is null in the constructor, so the NavigatedTo Event sets the original content here
    // Is there a better MVVM way to set this?
    [RelayCommand]
    async Task SetOriginalContentAsync()
    {
        await Task.Run(() => originalContent = Thought.Content);
    }

    [RelayCommand]
    async Task SaveAsync()
    {
        if (string.IsNullOrEmpty(Thought.Content))
        {
            await ShortToast("Please enter a message before saving.");

            return;
        }

        if (string.IsNullOrWhiteSpace(Thought.Content))
        {
            var result = await Shell.Current.DisplayAlert("Save empty thought?", null, "Save", "Cancel");

            if (!result)
                return;
        }
        await thoughtsService.UpdateThought(Thought);
        await NavigateToLibraryAsync();
    }

    // Navigation
    async Task NavigateToLibraryAsync()
    {
        await Shell.Current.GoToAsync("//root/library");
    }
}
