namespace WriteToCompassion.Views;

public partial class EditorView : ContentPage
{
    bool showMore;
    bool showMoreBusy;
    public EditorView(EditorViewModel editorViewModel)
    {
        InitializeComponent();
        BindingContext = editorViewModel;
        moreButtonStack.Opacity = 0;
        showMore = false;
        showMoreBusy = false;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

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
            moreButtonStack.FadeTo(1, 100, Easing.SinIn);
            moreButtonStack.InputTransparent = false;
            showMore = true;
            showMoreBusy = false;
        }
        else
        {
            showMoreBusy = true;

            moreButtonStack.FadeTo(0, 50, Easing.SinIn);

            moreButtonStack.InputTransparent = true;
            showMore = false;
            showMoreBusy = false;
        }
    }
    async void HandleOutOfMoreButtonStackBoundsTapped(object sender, TappedEventArgs e)
    {
        await ToggleMoreStack();
    }
}