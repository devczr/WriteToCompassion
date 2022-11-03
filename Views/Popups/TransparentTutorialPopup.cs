using CommunityToolkit.Maui.Views;

namespace WriteToCompassion.Views.Popups;

public class TransparentTutorialPopup : Popup
{
    public TransparentTutorialPopup(Size popupSize) : this()
    {
        Size = popupSize;
        Color = Colors.Transparent;
    }

    public TransparentTutorialPopup()
    {
        Content = new Frame
        {
            CornerRadius = 25,
            HeightRequest = 50,
            WidthRequest = 50
        };
    }

}
