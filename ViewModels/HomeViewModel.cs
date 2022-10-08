﻿
using CommunityToolkit.Maui.Alerts;
using System.Collections.ObjectModel;
using WriteToCompassion.Services.Thoughts;
using WriteToCompassion.Views;

namespace WriteToCompassion.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
    public ObservableCollection<CustomCloudControl> CloudControls { get; set; } = new();

    public ObservableCollection<Thought> UnreadThoughts { get; } = new();

    public ObservableCollection<Thought> AllThoughts { get; } = new();

    ThoughtsService thoughtsService;

    [ObservableProperty]
    string popText;

    [ObservableProperty]
    bool animateCloudLottie = false;

    [ObservableProperty]
    double cloudScale = 0.5;

    #region CloudAnimationTypes for MVVM Bindings

    private CloudAnimationType _cloud1Animation;
    public CloudAnimationType Cloud1Animation
    {
        get => _cloud1Animation;
        set
        {
            if (_cloud1Animation != value)
            {
                _cloud1Animation = value;
                OnPropertyChanged();

            }

        }
    }

    private CloudAnimationType _cloud2Animation;
    public CloudAnimationType Cloud2Animation
    {
        get => _cloud2Animation;
        set
        {
            if (_cloud2Animation != value)
            {
                _cloud2Animation = value;
                OnPropertyChanged();

            }

        }
    }

    private CloudAnimationType _cloud3Animation;
    public CloudAnimationType Cloud3Animation
    {
        get => _cloud3Animation;
        set
        {
            if (_cloud3Animation != value)
            {
                _cloud3Animation = value;
                OnPropertyChanged();

            }

        }
    }

    private CloudAnimationType _cloud4Animation;
    public CloudAnimationType Cloud4Animation
    {
        get => _cloud4Animation;
        set
        {
            if (_cloud4Animation != value)
            {
                _cloud4Animation = value;
                OnPropertyChanged();

            }

        }
    }

    private CloudAnimationType _cloud5Animation;
    public CloudAnimationType Cloud5Animation
    {
        get => _cloud5Animation;
        set
        {
            if (_cloud5Animation != value)
            {
                _cloud5Animation = value;
                OnPropertyChanged();

            }

        }
    }

    #endregion


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

            PopulateCloudCollection();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error",
                $"Unable to get your saved notes: {ex.Message}", "OK");
        }
    }

    private void PopulateCloudCollection()
    {
        for (int i = 0; i < 5; i++)
        {
            CustomCloudControl c = new();
            c.CloudAnimation = CloudAnimationType.None;
            CloudControls.Add(c);
        }

    }


    [RelayCommand]
    async Task InitUnreadDriftAsync()
    {
        var cloudCount = Math.Clamp(UnreadThoughts.Count,0,5);
 //       await Shell.Current.DisplayAlert("ok", InactiveClouds.Count.ToString(), "ok");
        for (int i = 0; i < cloudCount; i++)
        {
        }
    }

    [RelayCommand]
    async Task TestDrifting()
    {
        CloudControls[0].CloudAnimation = CloudAnimationType.Drift;
        /*        Cloud1Animation = CloudAnimationType.Drift;
                Cloud2Animation = CloudAnimationType.Drift;
                Cloud3Animation = CloudAnimationType.Drift;
                Cloud4Animation = CloudAnimationType.Drift;
                Cloud5Animation = CloudAnimationType.Drift;*/
    }



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