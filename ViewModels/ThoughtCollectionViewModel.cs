
using CommunityToolkit.Maui.Alerts;
using System.Collections.ObjectModel;
using WriteToCompassion.Services.Thoughts;
using WriteToCompassion.Views;

namespace WriteToCompassion.ViewModels;

public partial class ThoughtCollectionViewModel : BaseViewModel
{
    private readonly ThoughtsService thoughtService;
    public ObservableCollection<Thought> Thoughts { get; } = new();

    [ObservableProperty]
    bool isRefreshing;

    public ThoughtCollectionViewModel(ISettingsService settingsService, ThoughtsService thoughtsService) : base(settingsService)
	{
        this.thoughtService = thoughtsService;
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
            IsRefreshing = false;
        }
    }
}
