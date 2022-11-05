namespace WriteToCompassion;

public partial class AppShell : Shell
{
	public AppShell()
	{

		AppShell.InitializeRouting();
		InitializeComponent();
	}

	private static void InitializeRouting()
	{
        Routing.RegisterRoute(nameof(HomeView), typeof(HomeView));
        Routing.RegisterRoute(nameof(NewThoughtEditorView), typeof(NewThoughtEditorView));
        Routing.RegisterRoute(nameof(LibraryView), typeof(LibraryView));
        Routing.RegisterRoute(nameof(SettingsView), typeof(SettingsView));
        Routing.RegisterRoute(nameof(EditorView), typeof(EditorView));
    }
}
