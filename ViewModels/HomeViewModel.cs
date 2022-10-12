﻿
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
    //HomeView grid bindable layout bound to this Clouds observable collection
    //number of clouds visible = Clouds.Count
    public ObservableCollection<Cloud> Clouds { get; set; } = new();


    //Read & unread thoughts retrieved from db via thoughtsservice
    public List<Thought> AllThoughts { get; } = new();
    public List<Thought> UnreadThoughts { get; } = new();

    //handles database logic
    ThoughtsService thoughtsService;

    //holds user preferences
    private readonly ISettingsService settingsService;



    //User can adjust in Settings
    [ObservableProperty]
    double cloudScale = 0.5;

    [ObservableProperty]
    int maxClouds = 30;

    [ObservableProperty]
    bool unreadOnly = true;


    //general XAML Bindings
    [ObservableProperty]
    bool animateCloudLottie = false;

    [ObservableProperty]
    string cloudContent = "";

    [ObservableProperty]
    bool cloudTextVisible;

    public HomeViewModel(ThoughtsService thoughtsService, ISettingsService settingsService)
            : base(settingsService)
    {
        this.settingsService = settingsService;
        this.thoughtsService = thoughtsService;
    }
    

    //mct:EventToCommand behavior on HomeView contentpage behavior calls this when "NavigatedTo" fires
    [RelayCommand]
    async Task GetAllThoughtsAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            //clear list that xaml clouds are bound to
            CloudContent = "";
            Clouds.Clear();
            UnreadThoughts.Clear();
            AllThoughts.Clear();

            //viewmodel calls into Service so our Database Logic isn't locked into our ViewModel
            var thoughts = await thoughtsService.GetAllThoughts();

            if (settingsService.UnreadOnly)
            {
                thoughts = thoughts.Where(t => t.ReadCount == 0).ToList();
                foreach (var thought in thoughts)
                    UnreadThoughts.Add(thought);
            }
            else
            {
                foreach (var thought in thoughts)
                    AllThoughts.Add(thought);
            }

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
            IsBusy = false;
        }
    }

    //Add to Clouds collection which signals bindable layout to spawn CustomCloudControls
    [RelayCommand]
    async Task InitDriftAsync()
    {
        int cloudCount;

        //the number of clouds added to Clouds collection depends on:
        // 1) how many thoughts the user has saved in database
        // 2) their MaxClouds setting
        // 3) their "show only unread" or "show all" setting
        //    a thought is considered Unread if its content has never been displayed (swipe up gesture)


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


    //Cloud Animations

    //CustomCloudControls have a CloudAnimation enum property bound to a matching type on the Clouds collection
    //all UI changes in CustomCloudControl animations are based on changes to the BindableProperty CloudAnimationProperty
    //animations and properties are defined in the CustomCloudControl codebehind
    //Any change in animation must be made by changing the [ObservableProperty] animationType in the Cloud model
    //changing animation without raising Cloud model inotifypropertychanged may desynch the relationship between View & ViewModel properties
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


    //Swipe Up
    [RelayCommand]
    async Task CloudSwipedAsync(Cloud swipedCloud)
    {
        if (swipedCloud is null)
            return;

        var index = await Task.Run(() => Clouds.IndexOf(swipedCloud));
        Clouds[index].AnimationType = CloudAnimationType.Display;
        await UpdateContentProperty(index);
    }

    //A HomeView label is bound to the CloudContent
    //Content is chosen simply by matching the index of the list with the collection
    //TODO: Randomize how a Thought is chosen when cloud is swiped
    private async Task UpdateContentProperty(int index)
    {
        if (UnreadOnly)
            CloudContent = await Task.Run(()=>UnreadThoughts[index].Content);
        else
            CloudContent = await Task.Run(() => AllThoughts[index].Content);
    }




    //Navigation
    [RelayCommand]
    async Task GoToSettingsAsync()
    {
        await Shell.Current.GoToAsync(nameof(SettingsView));
    }

    //Hardcoded strings helping with TabBar failing to close the editors when using nameof
    [RelayCommand]
    async Task GoToNewThoughtEditorAsync()
    {
        await Shell.Current.GoToAsync("//root/newthought");
    }

}