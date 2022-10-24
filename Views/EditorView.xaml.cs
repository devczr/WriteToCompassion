namespace WriteToCompassion.Views;

public partial class EditorView : ContentPage
{
	public EditorView(EditorViewModel editorViewModel)
	{
		InitializeComponent();
		BindingContext = editorViewModel;
        ModifyEditor();
	}

    void ModifyEditor()
    {
        Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping("MyCustomization", (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.TextCursorDrawable = null;
#endif
        });
    }

}