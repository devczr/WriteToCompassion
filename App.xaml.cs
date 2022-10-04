#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif
using WriteToCompassion.Services.Settings;


namespace WriteToCompassion;

public partial class App : Application
{
    private readonly ISettingsService _settingsService;

    const int WindowWidth = 450;
    const int WindowHeight = 800;
    public App(ISettingsService settingsService)
    {
        _settingsService = settingsService;
        InitializeComponent();

        //Windows app window size help on dotnet/maui github
        //https://github.com/dotnet/maui/discussions/2370
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
        {
#if WINDOWS
            var mauiWindow = handler.VirtualView;
            var nativeWindow = handler.PlatformView;
            nativeWindow.Activate();
            IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
            WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
            AppWindow appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
            appWindow.Resize(new SizeInt32(WindowWidth, WindowHeight));
#endif
        });
        MainPage = new AppShell();
    }
}


