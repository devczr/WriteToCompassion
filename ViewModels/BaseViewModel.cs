namespace WriteToCompassion.ViewModels;

public abstract partial class BaseViewModel : ObservableObject
{

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;
    public bool IsNotBusy => !isBusy;


    public ISettingsService SettingsService { get; private set; }

    public BaseViewModel(ISettingsService settingsService)
    {
        SettingsService = settingsService;
    }


    // Toast
    public virtual async Task ShortToast(string message)
    {
        CancellationTokenSource cts = new CancellationTokenSource();

        ToastDuration duration = ToastDuration.Short;

        double fontSize = 14;

        var toast = Toast.Make(message, duration, fontSize);

        await toast.Show(cts.Token);
    }

}
