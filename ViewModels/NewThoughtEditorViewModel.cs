﻿
using WriteToCompassion.Services.Thoughts;
using WriteToCompassion.Views;

namespace WriteToCompassion.ViewModels;

public partial class NewThoughtEditorViewModel : BaseViewModel
{
    ThoughtsService thoughtsService;

    [ObservableProperty]
    Thought newThought = new();


    public NewThoughtEditorViewModel(ISettingsService settingsService, ThoughtsService thoughtsService) : base(settingsService)
    {
        this.thoughtsService = thoughtsService;
    }

    [RelayCommand]
    async Task UpdateThoughtContent(string newText)
    {
        await Task.Run(()=>NewThought.Content = newText);
    }

    


    [RelayCommand]
    async Task DeleteThoughtAsync()
    {
        var result = await Shell.Current.DisplayAlert("Delete Thought?", "This cannot be undone.", "DELETE", "Cancel");

        if (result)
        {
            int id = NewThought.Id;
            await thoughtsService.DeleteThought(id);
            await Shell.Current.GoToAsync(nameof(ThoughtCollectionView));
        }
        else return;
    }
    async Task AddThoughtAsync()
    {
        try
        {
            IsBusy = true;
            //viewmodel calls into Service so our Database Logic isn't locked into our ViewModel

            if (string.IsNullOrEmpty(NewThought.Content))
                return;

            await thoughtsService.AddThought(NewThought.Content);
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
    async Task SaveAndNavigateToThoughts()
    {
        await AddThoughtAsync();
        await Shell.Current.GoToAsync(nameof(ThoughtsPage));
    }

    [RelayCommand]
    async Task CancelAsync()
    {
        await Shell.Current.GoToAsync(nameof(ThoughtsPage));
    }

}
