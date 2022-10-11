
namespace WriteToCompassion.Models;

public class Cloud : ObservableObject
{
    private CloudAnimationType _animationType;
    public CloudAnimationType AnimationType
    {
        get => _animationType;
    
        set => SetProperty(ref _animationType, value);
    }

    public Guid CloudID { get; set; }

}
