namespace WriteToCompassion.Views;

public partial class EditorView : ContentPage
{
    bool showMore = false;
    bool showMoreBusy = false;
    public EditorView(EditorViewModel editorViewModel)
    {
        InitializeComponent();
        BindingContext = editorViewModel;
        ModifyEditor();
        moreButtonStack.Opacity = 0;
        moreButtonStack.InputTransparent = true;
        showMore = false;
        showMoreBusy = false;
    }


    async void FadeOnMoreButtonClicked(object sender, EventArgs e)
    {
        await ToggleMoreStack();
    }

    async Task ToggleMoreStack()
    {
        if (showMoreBusy)
            return;

        if (!showMore)
        {
            showMoreBusy = true;
            await Task.WhenAll
            (
                moreButtonStack.TranslateTo(-10, 0, 50, Easing.SinIn),
                moreButtonStack.FadeTo(1, 100, Easing.SinIn)
            );
            moreButtonStack.InputTransparent = false;
            showMore = true;
            showMoreBusy = false;
        }
        else
        {
            showMoreBusy = true;
            await Task.WhenAll
            (
                moreButtonStack.FadeTo(0, 50, Easing.SinIn),
                moreButtonStack.TranslateTo(0, 0, 20, Easing.SinIn)
            );
            moreButtonStack.InputTransparent = true;
            showMore = false;
            showMoreBusy = false;
        }
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

    async void HandleOutOfMoreButtonStackBoundsTapped(object sender, TappedEventArgs e)
    {
        await ToggleMoreStack();
    }
}