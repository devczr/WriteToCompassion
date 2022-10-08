
using WriteToCompassion.Services.Thoughts;
using WriteToCompassion.Views;

namespace WriteToCompassion.ViewModels;

public partial class NewThoughtEditorViewModel : BaseViewModel
{
    ThoughtsService thoughtsService;

    [ObservableProperty]
    Thought newThought = new();

    //The large empty string in placeholder is used as a workaround for MAUI Editor bug on Android where the editor boundary is clipped to the length of the placeholder. The extra space ensures users can type across the length of the entire control
    public string PlaceholderText { get; set; } = "Leave a kind note for yourself here...";

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
            await Shell.Current.GoToAsync("//root/home");
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
                $"Unable to save thought: {ex.Message}", "OK");
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
        
        //TODO: update with nameof() once MAUI shell TabBar correctly recognizes it as navigation 
        await Shell.Current.GoToAsync("//root/home");
    }

    [RelayCommand]
    async Task CancelAsync()
    {
        await Shell.Current.GoToAsync("//root/home");
    }

}
