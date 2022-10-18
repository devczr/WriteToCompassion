using System.Diagnostics;
using CommunityToolkit.Maui.Alerts;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WriteToCompassion.Services.Thoughts;
using WriteToCompassion.Views;
using Microsoft.Maui.Networking;

namespace WriteToCompassion.ViewModels;

public partial class LibraryViewModel : BaseViewModel
{
    private readonly ISettingsService settingsService;

    private readonly ThoughtsService thoughtsService;
    public ObservableCollection<Thought> UnreadThoughts { get; } = new();

    public ObservableCollection<Thought> AllThoughts { get; } = new();

    [ObservableProperty]
    bool isRefreshing;

    [ObservableProperty]
    bool exists;

    /*    [ObservableProperty]
        SelectionMode selectionMode = SelectionMode.Multiple;*/

    /*    [ObservableProperty]
        public ObservableCollection<Thought> selectedThoughts = new();*/




    private ObservableCollection<Thought> _thoughts;

    private ObservableCollection<object> _selectedThoughts = new ObservableCollection<object>();
    private SelectionMode _selectionMode = SelectionMode.None;
    public Thought SelectedItem { get; set; }
    public SelectionMode SelectionMode { get => _selectionMode; set => SetProperty(ref _selectionMode, value); }
    public ObservableCollection<Thought> Thoughts { get => _thoughts; set => _thoughts = value; }
    public ObservableCollection<object> SelectedThoughts { get => _selectedThoughts; set => _selectedThoughts = value; }

    public Command ShareCommand { get; set; }

    public Command<Thought> LongPressCommand { get; private set; }

    public Command ClearCommand { get; private set; }

    public Command<Thought> TappedCommand { get; private set; }


    public LibraryViewModel(ISettingsService settingsService, ThoughtsService thoughtsService) : base(settingsService)
    {
        this.settingsService = settingsService;
        this.thoughtsService = thoughtsService;

        InitThoughtsAsync();
        LongPressCommand = new Command<Thought>(OnLongPress);
        ClearCommand = new Command(OnClear);
        TappedCommand = new Command<Thought>(OnTapped);
    }

    private async void OnTapped(Thought thought)
    {

        if (thought is null)
            return;

        if (_selectionMode != SelectionMode.None)
        {
            Debug.WriteLine($"Added {thought.Content}");
            if (_selectedThoughts.Contains(thought))
                SelectedThoughts.Remove(thought);
            else
                SelectedThoughts.Add(thought);
        }
        else
        {
            await Shell.Current.GoToAsync(nameof(EditorView), true, new Dictionary<string, object>
            {
                {"Thought", thought }
            });
        }
    }

    private void OnClear()
    {
        SelectionMode = SelectionMode.None;
    }


    private void OnLongPress(Thought obj)
    {
        Debug.WriteLine("LongPressed");
        if (_selectionMode == SelectionMode.None)
        {
            SelectionMode = SelectionMode.Multiple;
            SelectedThoughts.Add(obj);
        }
    }


    [RelayCommand]
    async Task InitThoughtsAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

//           _selectedThoughts = new ObservableCollection<object>();

//            UnreadThoughts.Clear();
//            AllThoughts.Clear();
            var thoughts = await thoughtsService.GetThoughtCollection();

            foreach (var thought in thoughts)
                AllThoughts.Add(thought);


        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error",
                $"Unable to load thought library: {ex.Message}", "OK");
        }
        finally
        {
            OnPropertyChanged(nameof(AllThoughts));
            IsBusy = false;
        }
    }

    [RelayCommand]
    async Task RefreshThoughtsAsync()
    {
        if (IsBusy)
            return;

        try
        {

            IsBusy = true;
            var thoughts = await thoughtsService.GetThoughtCollection();

            if (AllThoughts.Count != 0)
                AllThoughts.Clear();

            foreach (var thought in thoughts)
                AllThoughts.Add(thought);

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error",
                $"Unable to load thought library: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }


    [RelayCommand]
    async Task GoToEditor(Thought thought)
    {
        if (thought is null)
            return;

        await Shell.Current.GoToAsync(nameof(EditorView), true, new Dictionary<string, object>
        {
            {"Thought", thought }
        });
    }

    [RelayCommand]
    async Task GetAllThoughtsAsync()
    {
/*        try
        {
            IsBusy = true;
            UnreadThoughts.Clear();
            AllThoughts.Clear();

            var thoughts = await thoughtsService.GetAllThoughts();

            foreach (var thought in thoughts)
                AllThoughts.Add(thought);

            *//*            if (settingsService.UnreadOnly)
                        {
                            thoughts = thoughts.Where(t => t.ReadCount <= 0).ToList();
                            foreach (var thought in thoughts)
                                UnreadThoughts.Add(thought);
                        }
                        else
                        {
                            foreach (var thought in thoughts)
                                AllThoughts.Add(thought);
                        }*//*

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error",
                $"Unable to get your saved thoughts: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }*/
    }

/*    [RelayCommand]
    async Task LongPressAsync(object thought)
    {
        if (thought is null)
            return;

        if (selectionMode is SelectionMode.None)
        {
            SelectionMode = SelectionMode.Multiple;
            SelectedThoughts.Add(thought);
        }
    }*/



/*    [RelayCommand]
    async Task TapTestAsync(Thought thought)
    {
        if (SelectedThoughts.Contains(thought))
        {
            SelectedThoughts.Remove(thought);
            await Shell.Current.DisplayAlert("tap", "Thought removed from selection", "Ok");
        }
        else
        {
            SelectedThoughts.Add(thought);
            await Shell.Current.DisplayAlert("tap", "Thought added to selection", "Ok");
        }
    }*/

        /*[RelayCommand]
    async Task TapAsync(Thought thought)
    {
        if (thought is null)
            return;

        if(SelectionMode is not SelectionMode.None)
        {
            if (SelectedThoughts.Contains(thought))
            {
                SelectedThoughts.Remove(thought);
            }
            else
            {
                SelectedThoughts.Add(thought);
            }
        }
        else
        {

            await Shell.Current.DisplaySnackbar("off to editor now");
        }


        if (selectionMode is SelectionMode.None)
        {
            SelectionMode = SelectionMode.Multiple;
            SelectedThoughts.Add(thought);
        }
    }*/

    [RelayCommand]
    async Task ClearCollectionsAsync()
    {
        await Task.Run(() => UnreadThoughts.Clear());
        await Task.Run(() => AllThoughts.Clear());
    }

    [RelayCommand]
    async Task RefreshTrueAsync()
    {
        IsRefreshing = true;
    }


    // Navigation
    [RelayCommand]
    async Task GoToNewThoughtEditorAsync()
    {
        await Shell.Current.GoToAsync(nameof(NewThoughtEditorView));
    }

    [RelayCommand]
    async Task GoToHomeAsync()
    {
        await Shell.Current.GoToAsync(nameof(HomeView));
    }

}
