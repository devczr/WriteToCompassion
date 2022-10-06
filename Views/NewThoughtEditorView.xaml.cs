namespace WriteToCompassion.Views;

public partial class NewThoughtEditorView : ContentPage
{
	public NewThoughtEditorView(NewThoughtEditorViewModel newThoughtEditorViewModel)
	{
		InitializeComponent();
		BindingContext = newThoughtEditorViewModel;
	}
}