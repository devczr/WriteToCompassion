using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using WriteToCompassion.Views;
using WriteToCompassion.Models;
using WriteToCompassion.Services;
using WriteToCompassion.Services.Navigation;
using WriteToCompassion.Services.Settings;

namespace WriteToCompassion.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        NoteService noteService;

        [ObservableProperty]
        string entryText;

        [ObservableProperty]
        ObservableCollection<Note> notes;

        [ObservableProperty]
        ObservableCollection<Note> lastNote;



        public MainViewModel(NoteService noteService, INavigationService navigationService, ISettingsService settingsService) 
            : base(navigationService, settingsService)
        {
            this.noteService = noteService;
            Notes = new ObservableCollection<Note>();
            LastNote = new ObservableCollection<Note>();
            Title = "Write To Compassion";

        }

        [RelayCommand]
        async Task GoToThoughtsAsync()
        {
            await Shell.Current.GoToAsync(nameof(ThoughtsPage));
        }

        [RelayCommand]
        async Task AddNoteAsync()
        {
            int result = 0;

            try
            {
                IsBusy = true;
                //viewmodel calls into Service so our Database Logic isn't locked into our ViewModel

                if (string.IsNullOrEmpty(EntryText))
                    return;

                await noteService.AddNote(EntryText);

                EntryText = string.Empty;
                //using GetAllNotes sort of like a Refresh command here temporarily
                await GetAllNotesAsync();
                await GetLastNote();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error",
                    $"Unable to get save note: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

/*        async Task RemoveNoteAsync()
        {

        }*/

        [RelayCommand]
        async Task GetAllNotesAsync()
        {

            try
            {
                IsBusy = true;
                //viewmodel calls into Service so our Database Logic isn't locked into our ViewModel
                var notes = await noteService.GetAllNotes();

                Notes.Clear();

                foreach(var note in notes)
                    Notes.Add(note);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error",
                    $"Unable to get your saved notes: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }

        }

        [RelayCommand]
        async Task<Note> GetLastNote()
        {
            try
            {
                IsBusy = true;
                int noteCount = Notes.Count();
                LastNote.Clear();
                var noteToAdd = Notes.ElementAtOrDefault(noteCount-1);
                LastNote.Add(noteToAdd);
                return noteToAdd;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error",
                    $"Unable to get your saved notes: {ex.Message}", "OK");
                return null;
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task DeleteLastNote()
        {
            if (LastNote is null || LastNote.Count() is 0)
                return;
            
            try
            {
                IsBusy = true;
                int removeID = LastNote[0].Id;
                await noteService.RemoveNote(removeID);
                await GetAllNotesAsync();
                await GetLastNote();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error",
                    $"Unable to delete last note: {ex.Message}", "OK"); 
            }
            finally
            {
                IsBusy = false;
            }
        }


        //testing modally displayed prompts
        [RelayCommand]
        async Task DeleteSpecificNoteAsync()
        {
            try
            {
                IsBusy = true;
                string result = await Shell.Current.DisplayPromptAsync("Delete Note", "Enter the note's ID");
                if (string.IsNullOrEmpty(result))
                    return;

                //unsafe conversion of string to int, TODO: make this safer
                int removeID = Convert.ToInt32(result);
                await noteService.RemoveNote(removeID);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error",
                    $"Unable to delete last note: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }

        }


        //testing modally displayed prompts
        [RelayCommand]
        async Task DeleteAllNotes()
        {
            try
            {
                IsBusy = true;
                string result = await Shell.Current.DisplayPromptAsync("Are you sure you want to delete all notes? This cannot be undone.", "Type: DELETEALL  to delete all notes");
                if (string.IsNullOrEmpty(result))
                    return;
                result.Trim();
                await noteService.RemoveNote(result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error",
                    $"Unable to delete all notes: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }

        }

    }
}
