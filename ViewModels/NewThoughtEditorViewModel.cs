
using CommunityToolkit.Maui.Alerts;
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
    public ICommand ButtonSaveCommand { get; private set; }

    public string PlaceholderText { get; set; } = "Leave a kind note for yourself here...";

    public NewThoughtEditorViewModel(ISettingsService settingsService, ThoughtsService thoughtsService) : base(settingsService)
    {
        this.thoughtsService = thoughtsService;
        ButtonSaveCommand = new Command(
            execute: async () =>
            {
                await SaveAsync();
            },
            canExecute: () =>
            {
                if (string.IsNullOrEmpty(newThought.Content))
                {
                    return false;
                }
                else
                    return true;
            });
    }

    [RelayCommand]
    async Task UpdateThoughtContent(string newText)
    {


        NewThought.Content = newText;

        //Calls CanExecute defined in this class constructor to enable button if content is not null
        (ButtonSaveCommand as Command).ChangeCanExecute();
    }


    [RelayCommand]
    async Task DeleteThoughtAsync()
    {
        var result = await Shell.Current.DisplayAlert("Delete Thought?", "This cannot be undone.", "DELETE", "Cancel");

        if (result)
        {
            int id = NewThought.Id;
            await thoughtsService.DeleteThought(id);
            await Shell.Current.GoToAsync("//root/home");
        }
        else return;
    }
    async Task AddThoughtToDatabaseAsync()
    {
        try
        {
            IsBusy = true;
            //viewmodel calls into Service so our Database Logic isn't locked into our ViewModel

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
    async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(NewThought.Content))
        {
            var result = await Shell.Current.DisplayAlert("Save empty thought?", null, "Save", "Cancel");

            if (!result)
                return;
        }
        await AddThoughtToDatabaseAsync();
        await NavigateToThoughtsAsync();
    }



    // Editor Context Menu Flyouts
    [RelayCommand]
    async Task BoldAsync()
    {
        await Task.Run(() => EditorFontAttribute = FontAttributes.Bold);
    }

    [RelayCommand]
    async Task ItalicisedAsync()
    {
        await Task.Run(() => EditorFontAttribute = FontAttributes.Italic);
    }

    [RelayCommand]
    async Task DefaultAsync()
    {
        await Task.Run(() => EditorFontAttribute = FontAttributes.None);
    }

    // Navigation
    async Task NavigateToThoughtsAsync()
    {
        await Shell.Current.GoToAsync("//root/home");
    }

    [RelayCommand]
    async Task CancelAsync()
    {
        await Shell.Current.GoToAsync("//root/home");
    }

}
