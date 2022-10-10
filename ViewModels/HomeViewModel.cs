
using CommunityToolkit.Maui.Alerts;
using System.Collections.ObjectModel;
using System.Linq;
using WriteToCompassion.Models;
using WriteToCompassion.Services.Thoughts;
using WriteToCompassion.Views;

namespace WriteToCompassion.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
    public ObservableCollection<Cloud> Clouds { get; set; } = new();

    public ObservableCollection<Thought> UnreadThoughts { get; } = new();

    public ObservableCollection<Thought> AllThoughts { get; } = new();


    ThoughtsService thoughtsService;

    
    [ObservableProperty]
    bool animateCloudLottie = false;

    [ObservableProperty]
    double cloudScale = 0.5;

    [ObservableProperty]
    int maxClouds = 2;

    [ObservableProperty]
    bool unreadOnly = true;

    [ObservableProperty]
    string dragText = "";

    [ObservableProperty]
    bool cloudTextVisible;

    public HomeViewModel(ThoughtsService thoughtsService, ISettingsService settingsService)
            : base(settingsService)
    {
        this.thoughtsService = thoughtsService;
    }

    [RelayCommand]
    async Task GetAllThoughtsAsync()
    {
        try
        {
            //viewmodel calls into Service so our Database Logic isn't locked into our ViewModel
            var thoughts = await thoughtsService.GetAllThoughts();

            foreach (var thought in thoughts)
                AllThoughts.Add(thought);

            thoughts = thoughts.Where(t => t.ReadCount == 0).ToList();
            
            UnreadThoughts.Clear();
            
            foreach (var thought in thoughts)
                UnreadThoughts.Add(thought);

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error",
                $"Unable to get your saved notes: {ex.Message}", "OK");
        }
        finally
        {
            await InitDriftAsync();
        }
    }


    [RelayCommand]
    async Task InitDriftAsync()
    {
        int cloudCount;

        if (UnreadOnly)
            cloudCount = Math.Clamp(UnreadThoughts.Count, 0, MaxClouds);
        else
            cloudCount = Math.Clamp(AllThoughts.Count, 0, MaxClouds);


        for (int i = 0; i < cloudCount; i++)
        {
            Cloud c = new()
            {
                CloudAnimationType = CloudAnimationType.Drift,
                CloudID = Guid.NewGuid()
            };
            Clouds.Add(c);
        }
    }

    [RelayCommand]
    async Task DanceAsync(CustomCloudControl customCloudControl)
    {
        
        int index = Clouds.IndexOf(await GetCloudById(customCloudControl));
        await Shell.Current.DisplayAlert("index is", index.ToString(), "Ok");
/*        int index = Clouds.IndexOf(cAT);
        CloudControls[index].CloudAnimation = CloudAnimationType.Dance;*/
    }

    private async Task<Cloud> GetCloudById(CustomCloudControl customCloudControl)
    {
        var result = await Task.Run(() => 
        Clouds.Where(c => c.CloudID == customCloudControl.CloudControlID)
        .FirstOrDefault());
        return result;
    }

    [RelayCommand]
    async Task CloudSwipedAsync(CustomCloudControl customCloudControl)
    {
/*        if (customCloudControl is null)
            return;

        int index = CloudControls.IndexOf(customCloudControl);
        await Shell.Current.DisplayAlert($"Swipe up--- {index}", "cloud command", "ok");*/
    }

    [RelayCommand]
    private void PopulateClouds()
    {

        int count = Clouds.Count;
        for(int i = 0; i < count; i++)
        {
            Clouds[i].CloudAnimationType = CloudAnimationType.Drift;
        }
    }


    //navigation
    [RelayCommand]
    async Task GoToSettingsAsync()
    {
        await Shell.Current.GoToAsync(nameof(SettingsView));
    }

    [RelayCommand]
    async Task GoToNewThoughtEditorAsync()
    {
        await Shell.Current.GoToAsync("//root/newthought");
    }

}