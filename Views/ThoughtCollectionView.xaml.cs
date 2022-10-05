using CommunityToolkit.Maui.Alerts;

namespace WriteToCompassion.Views;

public partial class ThoughtCollectionView : ContentPage
{
	public ThoughtCollectionView(ThoughtCollectionViewModel thoughtCollectionViewModel)
	{
		InitializeComponent();
		BindingContext = thoughtCollectionViewModel;
	}

}