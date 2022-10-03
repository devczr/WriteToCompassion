
using WriteToCompassion.Controls;

namespace WriteToCompassion.Triggers;

public class BeginDrift : TriggerAction<CustomCloudControl>
{
    protected override async void Invoke(CustomCloudControl customCloudControl)
    {
        Shell.Current.DisplayAlert(" ok", $"trigger", "ok");

        if (customCloudControl != null)
        {
            await customCloudControl.BeginDrift();
        }
    }

}
