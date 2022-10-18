namespace WriteToCompassion.Views;

public partial class LibraryView : ContentPage
{
    LibraryViewModel libraryViewModel;

	public LibraryView(LibraryViewModel libraryViewModel)
	{
		InitializeComponent();
		BindingContext = libraryViewModel;
        this.libraryViewModel = libraryViewModel;
	}


}