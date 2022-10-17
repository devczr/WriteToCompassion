using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;

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
		editor.Unfocus();
	}


    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

    }

    private void editor_Completed(object sender, EventArgs e)
    {

    }
}