namespace WriteToCompassion.ViewModels;

[QueryProperty(nameof(Thought), "Thought")]
public partial class EditorViewModel : BaseViewModel
{
	[ObservableProperty]
	Thought thought;
	public EditorViewModel(ISettingsService settingsService) : base(settingsService)
	{

	}



}
