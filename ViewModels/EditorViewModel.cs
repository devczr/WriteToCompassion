using WriteToCompassion.Services.Thoughts;
using WriteToCompassion.Views;

namespace WriteToCompassion.ViewModels;

[QueryProperty(nameof(Thought), "Thought")]
public partial class EditorViewModel : BaseViewModel
{
	ThoughtsService thoughtsService;

    [ObservableProperty]
	Thought thought;

	public EditorViewModel(ISettingsService settingsService, ThoughtsService thoughtsService) : base(settingsService)
	{
		this.thoughtsService = thoughtsService;
    }

	[RelayCommand]
	async Task UpdateThoughtWithDatabase()
	{
        await thoughtsService.UpdateThought(Thought);
        await Shell.Current.GoToAsync(nameof(ThoughtCollectionView));
	}

	[RelayCommand]
	async Task UpdateThoughtContent(string newText)
	{
		await Task.Run(()=>Thought.Content = newText);
	}


}
