
using CommunityToolkit.Maui.Alerts;
using System.Collections.ObjectModel;
using WriteToCompassion.Services.Thoughts;
using WriteToCompassion.Views;

namespace WriteToCompassion.ViewModels;

public partial class LibraryViewModel : BaseViewModel
{
    private readonly ISettingsService settingsService;

    private readonly ThoughtsService thoughtsService;
    public ObservableCollection<Thought> UnreadThoughts { get; } = new();

    public ObservableCollection<Thought> AllThoughts { get; } = new();

    [ObservableProperty]
    bool isRefreshing;

    public LibraryViewModel(ISettingsService settingsService, ThoughtsService thoughtsService) : base(settingsService)
    {
        this.settingsService = settingsService;
        this.thoughtsService = thoughtsService;
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
            UnreadThoughts.Clear();
            AllThoughts.Clear();

            var thoughts = await thoughtsService.GetAllThoughts();
            
            if (settingsService.UnreadOnly)
            {
                thoughts = thoughts.Where(t => t.ReadCount == 0).ToList();
                foreach (var thought in thoughts)
                    UnreadThoughts.Add(thought);
            }
            else
            {
                foreach (var thought in thoughts)
                    AllThoughts.Add(thought);
            }

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

    [RelayCommand]
    async Task ClearCollectionsAsync()
    {
        await Task.Run(() => UnreadThoughts.Clear());
        await Task.Run(() => AllThoughts.Clear());
    }
}
