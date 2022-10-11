
using CommunityToolkit.Maui.Alerts;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using WriteToCompassion.Models;
using WriteToCompassion.Services.Thoughts;
using WriteToCompassion.Views;

namespace WriteToCompassion.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
    public ObservableCollection<Cloud> Clouds { get; set; } = new();

    public List<Thought> UnreadThoughts { get; } = new();

    public List<Thought> AllThoughts { get; } = new();


    ThoughtsService thoughtsService;


    [ObservableProperty]
    bool animateCloudLottie = false;

    [ObservableProperty]
    double cloudScale = 0.5;

    [ObservableProperty]
    int maxClouds = 30;

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
            //clear list that xaml clouds are bound to
            Clouds.Clear();

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
            //delaying here due to OnSizeAllocated firing 3 times on app startup
            //also gives time for the page to load in between NavigatedTo and actual page visiblity
            await Task.Delay(1500);
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
                AnimationType = CloudAnimationType.Drift,
                CloudID = Guid.NewGuid()
            };
            Clouds.Add(c);
        }
    }

    [RelayCommand]
    async Task DanceAsync(Cloud cloudToDance)
    {
        var index = await Task.Run(() => Clouds.IndexOf(cloudToDance));

        Clouds[index].AnimationType = CloudAnimationType.Dance;
    }



    [RelayCommand]
    async Task HoverToDriftAsync(Cloud cloudToDance)
    {
        var index = await Task.Run(() => Clouds.IndexOf(cloudToDance));

        Clouds[index].AnimationType = CloudAnimationType.HoverToDrift;
    }


    [RelayCommand]
    async Task TestCloudsProperties()
    {
        Clouds[0].AnimationType = CloudAnimationType.None;
    }

    [RelayCommand]
    async Task CloudSwipedAsync(Cloud swipedCloud)
    {
        if (swipedCloud is null)
            return;

        int index = Clouds.IndexOf(swipedCloud);
        await Shell.Current.DisplayAlert($"Swipe up--- {index}", "cloud command", "ok");
    }

    [RelayCommand]
    private void PopulateClouds()
    {

        int count = Clouds.Count;
        for (int i = 0; i < count; i++)
        {
            Clouds[i].AnimationType = CloudAnimationType.Drift;
        }
    }


    [RelayCommand]
    async Task SizeAlertAsync()
    {
        await Shell.Current.DisplaySnackbar("size allocated!");
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