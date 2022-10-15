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

    private void saveButton_Clicked(object sender, EventArgs e)
    {

    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
		editor.Focus();
    }

    private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
    {

    }
}