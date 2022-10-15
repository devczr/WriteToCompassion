
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using WriteToCompassion.Services.Settings;

namespace WriteToCompassion.ViewModels
{
    public abstract partial class BaseViewModel : ObservableObject, IBaseViewModel
    {
        private bool _isInitialized;
        public bool IsInitialized
        {
            get => _isInitialized;
            set => SetProperty(ref _isInitialized, value);
        }


        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;
        public bool IsNotBusy => !isBusy;


        public ISettingsService SettingsService { get; private set; }

        public BaseViewModel(ISettingsService settingsService)
        {
            SettingsService = settingsService;
        }

        public virtual void ApplyQueryAttributes(IDictionary<string, object> query)
        {
        }
        [ObservableProperty]
        string title;


        public virtual Task InitializeAsync()
        {
            return Task.CompletedTask;
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
}
