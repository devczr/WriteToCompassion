
using CommunityToolkit.Mvvm.ComponentModel;
using WriteToCompassion.Services.Navigation;
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

        public INavigationService NavigationService { get; private set; }

        public ISettingsService SettingsService { get; private set; }

        public BaseViewModel(INavigationService navigationService, ISettingsService settingsService)
        {
            NavigationService = navigationService;
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


    }
}
