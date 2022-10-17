namespace WriteToCompassion.Views;

public partial class NewThoughtEditorView : ContentPage
{
	public NewThoughtEditorView(NewThoughtEditorViewModel newThoughtEditorViewModel)
	{
		InitializeComponent();
		BindingContext = newThoughtEditorViewModel;
	}

	protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
	{
		base.OnNavigatedFrom(args);

		//clearing editor as completion event not firing - known issue
		editor.Text = "";
	}

}