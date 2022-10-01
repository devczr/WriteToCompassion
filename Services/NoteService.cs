using CommunityToolkit.Mvvm.Input;
using SQLite;
using System.Diagnostics;
using System.Linq;
using WriteToCompassion.Models;
//TODO: remove class after completing ThoughtsService


namespace WriteToCompassion.Services
{
    public partial class NoteService
    {
        public NoteService()
        {

        }

        private SQLiteAsyncConnection dbAsyncConn;

        private async Task Init()
        {
            if (dbAsyncConn != null)
                return;

            var dbPath = Path.Combine(FileSystem.Current.AppDataDirectory, "MyNotes.db");

            dbAsyncConn = new SQLiteAsyncConnection(dbPath);

            await dbAsyncConn.CreateTableAsync<Note>();
        }

        public async Task AddNote(string inscription)
        {
            int result = 0;
            try
            {
                await Init();

                //basic validation - how can i improve this?
                if (string.IsNullOrEmpty(inscription))
                    return;
            
                result = await dbAsyncConn.InsertAsync(
                    new Note { 
                        Inscription = inscription.Trim(), 
                        TimeRead = DateTime.Now
                    });
           
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error",
                    $"Unable to save note : {ex.Message}", "OK");
            }
        }

        //TODO: flesh out remove note
        public async Task RemoveNote(int id)
        {
            await Init();
            await dbAsyncConn.DeleteAsync<Note>(id);
        }

        public async Task RemoveNote(string s)
        {
            if (s is "DELETEALL" || s is "DELETE ALL")
            {
                await Init();
                await dbAsyncConn.DeleteAllAsync<Note>();
                return;
            }
            else
            {

                await Shell.Current.DisplayAlert("Invalid entry",
    $"Notes were not deleted. Try again if this was a mistake.", "OK");
                return;

            }

        }



        List<Note> noteList = new ();
        public async Task<List<Note>> GetAllNotes()
        {
            await Init();

            noteList = await dbAsyncConn.Table<Note>().ToListAsync();
            return noteList;

        }







    }
}

