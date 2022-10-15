
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
    }

    [RelayCommand]
    async Task CancelAsync()
    {
        if (!string.IsNullOrWhiteSpace(NewThought.Content))
            await DiscardOrSaveAsync();
        else
            await NavigateToThoughtsAsync();
    }

    async Task DiscardOrSaveAsync()
    {
        var result = await Shell.Current.DisplayAlert("Save your thought?", null, "Save", "Discard");

        if (result)
        {
            await AddThoughtToDatabaseAsync();
            await NavigateToThoughtsAsync();
            await ShortToast("Thought saved.");
        }
        else
        {
            await NavigateToThoughtsAsync();
        }

    }



    [RelayCommand]
    async Task DeleteThoughtAsync()
    {
        var result = await Shell.Current.DisplayAlert("Delete Thought?", "This cannot be undone.", "DELETE", "Cancel");

        if (result)
        {
            int id = NewThought.Id;
            await thoughtsService.DeleteThought(id);
            await NavigateToThoughtsAsync();
        }
        else return;
    }

    async Task AddThoughtToDatabaseAsync()
    {
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




    // Navigation
    [RelayCommand]
    async Task NavigateToThoughtsAsync()
    {
        await Shell.Current.GoToAsync("//root/home");
    }

}
