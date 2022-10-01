using WriteToCompassion.Models;
using WriteToCompassion.ViewModels;
namespace WriteToCompassion.Views;

public partial class MainPage : ContentPage
{

	public MainPage(MainViewModel mainViewModel)
	{
		InitializeComponent();
		BindingContext = mainViewModel;
	}

}

