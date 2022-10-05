namespace WriteToCompassion.Views;

public partial class EditorView : ContentPage
{
	public EditorView(EditorViewModel editorViewModel)
	{
		InitializeComponent();
		BindingContext = editorViewModel;
	}
}