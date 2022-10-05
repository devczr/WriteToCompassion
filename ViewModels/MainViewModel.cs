using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using WriteToCompassion.Views;
using WriteToCompassion.Models;
using WriteToCompassion.Services;
using WriteToCompassion.Services.Settings;
using WriteToCompassion.Services.Thoughts;

namespace WriteToCompassion.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {







        public MainViewModel(ThoughtsService thoughtsService, ISettingsService settingsService) 
            : base(settingsService)
        {
            Title = "Write To Compassion";

        }

        [RelayCommand]
        async Task GoToThoughtsAsync()
        {
            await Shell.Current.GoToAsync(nameof(ThoughtsPage));
        }

/*        [RelayCommand]
        async Task AddNoteAsync()
        {
            int result = 0;

            try
            {
                IsBusy = true;

                if (string.IsNullOrEmpty(EntryText))
                    return;

                //viewmodel calls into Service so our Database logic isn't locked into our ViewModel
                await thoughtService.AddThought(EntryText);

                EntryText = string.Empty;
                //refresh here
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
        }*/


        /*        [RelayCommand]
                async Task<Note> GetLastNote()
                {
                    try
                    {
                        IsBusy = true;
                        int noteCount = Thoughts.Count();
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
                }*/

        /*        [RelayCommand]
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
                }*/


        //testing modally displayed prompts
        /*        [RelayCommand]
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

                }*/

 /*       [RelayCommand]
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

        }*/

    }
}
