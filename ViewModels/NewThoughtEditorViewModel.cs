
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System.Threading;
using System.Windows.Input;
using WriteToCompassion.Services.Thoughts;
using WriteToCompassion.Views;

namespace WriteToCompassion.ViewModels;

public partial class NewThoughtEditorViewModel : BaseViewModel
{
    ThoughtsService thoughtsService;

    [ObservableProperty]
    Thought newThought = new();

    [ObservableProperty]
    FontAttributes editorFontAttribute;

    public string PlaceholderText { get; set; } = "Leave a kind note for yourself here...";

    public NewThoughtEditorViewModel(ISettingsService settingsService, ThoughtsService thoughtsService) : base(settingsService)
    {
        this.thoughtsService = thoughtsService;
    }

    // Buttons
    [RelayCommand]
    async Task CancelAsync()
    {
        if (!string.IsNullOrWhiteSpace(NewThought.Content))
        {
            await DiscardOrSaveAsync("..");
        }
        else
            await Shell.Current.GoToAsync("..");
    }


    [RelayCommand]
    async Task SaveAsync()
    {
        if (string.IsNullOrEmpty(NewThought.Content))
        {
            await ShortToast("Please enter a message before saving.");
            return;
        }

        if (string.IsNullOrWhiteSpace(NewThought.Content))
        {
            var result = await Shell.Current.DisplayAlert("Save empty thought?", null, "Save", "Cancel");

            if (!result)
                return;
        }
        await AddThoughtToDatabaseAsync();
        await GoToThoughtsAsync();
        await ShortToast("Thought saved.");
    }


    // Database
    async Task AddThoughtToDatabaseAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            if (NewThought.Content is null)
                NewThought.Content = " ";

            await thoughtsService.AddThought(NewThought.Content);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error",
                $"Unable to save thought: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    async Task DeleteThoughtAsync()
    {
        var result = await Shell.Current.DisplayAlert("Delete Thought?", "This cannot be undone.", "DELETE", "Cancel");

        if (result)
        {
            if (IsBusy)
                return;
            
            try
            {
                IsBusy = true;
                int id = NewThought.Id;
                await thoughtsService.DeleteThought(id);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error",
                    $"Unable to delete thought: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }

            await GoToThoughtsAsync();
        }
        else return;
    }


    // Utility
    async Task DiscardOrSaveAsync(ShellNavigationState state)
    {
        var result = await Shell.Current.DisplayAlert("Save your thought?", null, "Save", "Discard");

        if (result)
        {
            await AddThoughtToDatabaseAsync();
            await Shell.Current.GoToAsync(state);
            await ShortToast("Thought saved.");
        }
        else
            await Shell.Current.GoToAsync(state);
    }


    [RelayCommand]
    private void UpdateThoughtContent(string newText)
    {
        NewThought.Content = newText;
    }




    // Navigation

    [RelayCommand]
    async Task GoToThoughtsAsync()
    {
        await Shell.Current.GoToAsync(nameof(HomeView));
    }

    [RelayCommand]
    async Task GoToLibraryAsync()
    {
        await Shell.Current.GoToAsync(nameof(LibraryView));
    }

    [RelayCommand]
    async Task CheckForSaveThenGoToThoughtsAsync()
    {
        if (!string.IsNullOrWhiteSpace(NewThought.Content))
        {
            await DiscardOrSaveAsync(nameof(HomeView));
        }
        else
            await Shell.Current.GoToAsync(nameof(HomeView));
    }
    [RelayCommand]
    async Task CheckForSaveThenGoToLibraryAsync()
    {
        if (!string.IsNullOrWhiteSpace(NewThought.Content))
        {
            await DiscardOrSaveAsync(nameof(LibraryView));
        }
        else
            await Shell.Current.GoToAsync(nameof(LibraryView));
    }



}
