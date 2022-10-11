
namespace WriteToCompassion.Models;

public partial class Cloud : ObservableObject
{
    [ObservableProperty]
    private CloudAnimationType _animationType;

/*    public CloudAnimationType AnimationType
    {
        get => _animationType;
    
        set => SetProperty(ref _animationType, value);
    }*/

    public Guid CloudID { get; set; }

}
