
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

    [ObservableProperty]
    bool saveShowing= false;
    public string PlaceholderText { get; set; } = "Leave a kind note for yourself here...";

    public NewThoughtEditorViewModel(ISettingsService settingsService, ThoughtsService thoughtsService) : base(settingsService)
    {
        this.thoughtsService = thoughtsService;
    }

    [RelayCommand]
    async Task UpdateThoughtContent(string newText)
    {


        NewThought.Content = newText;
        if(string.IsNullOrEmpty(NewThought.Content))
        {
            SaveShowing = false;
        }
        else
        {
            SaveShowing = true;
        }
        //Calls CanExecute defined in this class constructor to enable button if content is not null
        //        (ButtonSaveCommand as Command).ChangeCanExecute();
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
        if (string.IsNullOrEmpty(NewThought.Content))
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            string text = "Please enter a message before saving.";
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;
            var toast = Toast.Make(text, duration, fontSize);

            await toast.Show(cts.Token);
            return;
        }

        if (string.IsNullOrWhiteSpace(NewThought.Content))
        {
            var result = await Shell.Current.DisplayAlert("Save empty thought?", null, "Save", "Cancel");

            if (!result)
                return;
        }
        await AddThoughtToDatabaseAsync();
        await NavigateToThoughtsAsync();
    }


/*    async void SaveThoughtAsync()
    {
        if (string.IsNullOrWhiteSpace(NewThought.Content))
        {
            var result = await Shell.Current.DisplayAlert("Save empty thought?", null, "Save", "Cancel");

            if (!result)
                return;
        }
        await AddThoughtToDatabaseAsync();
        await NavigateToThoughtsAsync();
    }*/



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
    [RelayCommand]
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
