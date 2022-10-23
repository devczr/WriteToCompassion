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

    private bool initialized;

    public ObservableCollection<Thought> UnreadThoughts { get; } = new();

    public ObservableCollection<Thought> AllThoughts { get; } = new();

    [ObservableProperty]
    bool isRefreshing;

    [ObservableProperty]
    bool canRefresh = true;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotMultiSelect))]
    bool isMultiSelect;
    public bool IsNotMultiSelect => !isMultiSelect;

    [ObservableProperty]
    int countSelected;

    [ObservableProperty]
    int collectionSpan = 2;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotOneColumn))]
    bool isOneColumn = false;
    public bool IsNotOneColumn => !isOneColumn;

    [ObservableProperty]
    string sortBy = "Newest to Oldest";

    private ObservableCollection<object> _selectedThoughts = new ObservableCollection<object>();
    private SelectionMode _selectionMode = SelectionMode.None;
    public Thought SelectedItem { get; set; }
    public SelectionMode SelectionMode { get => _selectionMode; set => SetProperty(ref _selectionMode, value); }

    public ObservableCollection<object> SelectedThoughts { get => _selectedThoughts; set => _selectedThoughts = value; }

    public Command<Thought> LongPressCommand { get; private set; }

    public Command<Thought> TappedCommand { get; private set; }

    
    public LibraryViewModel(ISettingsService settingsService, ThoughtsService thoughtsService) : base(settingsService)
    {
        this.settingsService = settingsService;
        this.thoughtsService = thoughtsService;

        RefreshThoughtsAsync();
        LongPressCommand = new Command<Thought>(OnLongPress);
        TappedCommand = new Command<Thought>(OnTapped);
        IsOneColumn = false;
    }



    private async void OnTapped(Thought thought)
    {

        if (thought is null)
            return;

        if (_selectionMode != SelectionMode.None)
        {
            AddOrRemoveThought(thought);
        }
        else
        {
            await Shell.Current.GoToAsync(nameof(EditorView), true, new Dictionary<string, object>
            {
                {"Thought", thought }
            });
        }
    }

    private void OnLongPress(Thought thought)
    {
        if (thought is null)
            return;
        
        if (_selectionMode == SelectionMode.None)
        {
            IsMultiSelect = true;
            CanRefresh = false;
            SelectionMode = SelectionMode.Multiple;
            AddOrRemoveThought(thought);
        }
        else if(_selectionMode == SelectionMode.Multiple)
        {
            AddOrRemoveThought(thought); 
        }
    }

    // Add/Remove thought from Selected collection. If removing makes the selection count = 0, cancel multiselection
    private void AddOrRemoveThought(Thought thought)
    {
        if (_selectedThoughts.Contains(thought))
        {
            SelectedThoughts.Remove(thought);

            if (SelectedThoughts.Count <= 0)
                MultiSelectCancel();
            else
                CountSelected -= 1;
        }

        else
        {
            SelectedThoughts.Add(thought);
            CountSelected += 1;
        }

    }


    // Multi Select
    [RelayCommand]
    private void MultiSelectCancel()
    {
        SelectionMode = SelectionMode.None;
        IsMultiSelect = false;
        SelectedThoughts.Clear();
        CountSelected = 0;
        CanRefresh = true;
    }

    [RelayCommand]
    async Task MultiSelectDeleteAsync()
    {
        if (CountSelected <= 0)
            return;

        string warningText = CountSelected == 1 ? "Delete Thought?" : $"Delete {CountSelected} Thoughts?";

        var result = await Shell.Current.DisplayAlert(warningText, "This cannot be undone.", "DELETE", "Cancel");

        if (result)
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                for(int i = 0; i < SelectedThoughts.Count; i++)
                {
                    int id = (SelectedThoughts[i] as Thought).Id;
                    await thoughtsService.DeleteThought(id);
                }

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error",
                    $"Unable to delete thought: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
                MultiSelectCancel();
                await RefreshThoughtsAsync();
            }

        }
        else return;
    }

    [RelayCommand]
    async Task ChangeLayoutAsync()
    {
        if (CollectionSpan is 1)
        {
            IsOneColumn = false;
            await Task.Run(() => CollectionSpan = 2);
        }
        else
        {
            IsOneColumn = true;
            await Task.Run(() => CollectionSpan = 1);
        }
        await Shell.Current.DisplayAlert("Ok", SortBy, "Ok");

    }

    // Collection
    [RelayCommand]
    async Task RefreshThoughtsAsync()
    {
        if (IsBusy)
            return;

        try
        {

            IsBusy = true;
            var thoughts = await thoughtsService.GetThoughtCollection("");

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
            OnPropertyChanged(nameof(AllThoughts));
            IsBusy = false;
            IsRefreshing = false;
        }
    }

    // Navigation
    [RelayCommand]
    async Task GoToNewThoughtEditorAsync()
    {
        await Shell.Current.GoToAsync(nameof(NewThoughtEditorView));
    }

    [RelayCommand]
    async Task GoToEditorAsync(Thought thought)
    {
        if (thought is null)
            return;

        await Shell.Current.GoToAsync(nameof(EditorView), true, new Dictionary<string, object>
        {
            {"Thought", thought }
        });
    }

    [RelayCommand]
    async Task GoToHomeAsync()
    {
        await Shell.Current.GoToAsync(nameof(HomeView));
    }

    [RelayCommand]
    async Task GoToSettingsAsync()
    {
        await Shell.Current.GoToAsync(nameof(SettingsView));
    }

}


/*[RelayCommand]
async Task ClearCollectionsAsync()
{
    await Task.Run(() => UnreadThoughts.Clear());
    await Task.Run(() => AllThoughts.Clear());
}*/

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

