
using Microsoft.Maui.Controls;
using WriteToCompassion.Animations;

namespace WriteToCompassion.Triggers;

public class CancelAnimation : TriggerAction<VisualElement>
{
    protected override async void Invoke(VisualElement sender)
    {
        sender.AbortAnimation("Drift");
    }
}
