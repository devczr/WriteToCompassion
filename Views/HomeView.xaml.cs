using Microsoft.Maui;

namespace WriteToCompassion.Views;

public partial class HomeView : ContentPage
{
	public HomeView(HomeViewModel homeViewModel)
	{
		InitializeComponent();
		BindingContext = homeViewModel;
	}

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        ScreenHelper.UpdateScreenXYValues(width, height);
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }


/*    private async void AnimateBox()
    {
        var startingX = box.TranslationX;
        var startingY = box.TranslationY;
        do
        {
            box.CancelAnimations();
            await box.TranslateTo(startingX - 50, startingY - 50, 3000);
            await box.TranslateTo(startingX - 50, startingY, 3000);
            await box.TranslateTo(startingX, startingY - 50, 3000);
            await box.TranslateTo(startingX, startingY, 3000);
        } while (true);
    }*/

    private void OnBoxPanUpdated(object sender, PanUpdatedEventArgs e)
    {

    }

    private void OnSwiped(object sender, SwipedEventArgs e)
    {
        Shell.Current.DisplayAlert("helper", e.Direction.ToString(), "ok");
    }

    /*        switch (e.StatusType)
        {
            case GestureStatus.Running:
                HandleTouch(e.TotalX, e.TotalY);
                break;
            case GestureStatus.Completed:
                HandleTouchEnd(swipedDirection);
                break;
        }*/


    /*    private void HandleTouch(double eTotalX, double eTotalY)
        {
            swipedDirection = null;
            const int delta = 50;
            if (eTotalX > delta)
            {
                swipedDirection = SwipeDirection.Right;
            }
            else if (eTotalX < -delta)
            {
                swipedDirection = SwipeDirection.Left;
            }
            else if (eTotalY > delta)
            {
                swipedDirection = SwipeDirection.Down;
            }
            else if (eTotalY < -delta)
            {
                swipedDirection = SwipeDirection.Up;
            }
        }

        private void HandleTouchEnd(SwipeDirection? swiped)
        {
            if (swiped == null)
            {
                return;
            }

            switch (swiped)
            {
                case SwipeDirection.Right when Children.Count > 0:
                    cards.Push(Children[^1]);
                    Children.RemoveAt(Children.Count - 1);
                    break;
                case SwipeDirection.Left when cards.Count > 0:
                    Children.Add(cards.Pop());
                    break;
            }
        }*/
}