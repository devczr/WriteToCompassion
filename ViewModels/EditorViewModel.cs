using CommunityToolkit.Maui.Alerts;
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
	async Task DeleteThoughtAsync()
	{
		var result = await Shell.Current.DisplayAlert("Delete Thought?", "This cannot be undone.", "DELETE", "Cancel");

		if (result)
		{
			int id = Thought.Id;
			await thoughtsService.DeleteThought(id);
			await Shell.Current.GoToAsync(nameof(ThoughtCollectionView));
		}
		else return;
    }

	[RelayCommand]
	async Task UpdateThoughtContent(string newText)
	{
		await Task.Run(()=>Thought.Content = newText);
	}


}
