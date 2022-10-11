using System.Text;
namespace WriteToCompassion.Views;

public partial class HomeView : ContentPage
{
    private readonly HomeViewModel homeViewModel;
	public HomeView(HomeViewModel homeViewModel)
	{
		InitializeComponent();
		BindingContext = homeViewModel;
        this.homeViewModel = homeViewModel;
	}

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        ScreenHelper.UpdateScreenXYValues(cloudGrid.Width, cloudGrid.Height, contentBorder.Width, contentBorder.Height, cloudlottie.Height);
     
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }


    private void OnMeasureClicked(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Base: ");
        sb.Append("   w " + base.Width.ToString("F1"));
        sb.AppendLine("   h " + base.Height.ToString("F1"));
        sb.Append("pad: ");
        sb.Append("   horiz   " + base.Padding.HorizontalThickness);
        sb.AppendLine("   vert   " + base.Padding.VerticalThickness);
        sb.Append("Grid 0: ");
        sb.Append("   w " + contentBorder.Width.ToString("F1"));
        sb.AppendLine("   h " + contentBorder.Height.ToString("F1"));
        sb.Append("pad: ");
        sb.Append("   horiz   " + contentBorder.Padding.HorizontalThickness);
        sb.AppendLine("   vert   " + contentBorder.Padding.VerticalThickness);
        sb.Append("Grid 1: ");
        sb.Append("   w " + cloudGrid.Width.ToString("F1"));
        sb.AppendLine("   h " + cloudGrid.Height.ToString("F1"));
        sb.Append("pad: ");
        sb.Append("   horiz   " + cloudGrid.Padding.HorizontalThickness);
        sb.AppendLine("   vert   " + cloudGrid.Padding.VerticalThickness);
        sizeLabel.Text = sb.ToString();
    }

}