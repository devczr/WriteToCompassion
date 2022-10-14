
namespace WriteToCompassion.Models;

public partial class Cloud : ObservableObject
{
    [ObservableProperty]
    private CloudAnimationType _animationType;

    public Guid CloudID { get; set; }

}
