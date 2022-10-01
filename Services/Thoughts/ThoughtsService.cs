using CommunityToolkit.Mvvm.Input;
using SQLite;
using System.Diagnostics;
using System.Linq;
using WriteToCompassion.Models;

namespace WriteToCompassion.Services.Thoughts;

public class ThoughtsService
{
	public ThoughtsService()
	{

	}

    private SQLiteAsyncConnection dbAsyncConn;

    private async Task Init()
    {
        if (dbAsyncConn != null)
            return;

        var dbPath = Path.Combine(FileSystem.Current.AppDataDirectory, "MyThoughts.db");

        dbAsyncConn = new SQLiteAsyncConnection(dbPath);

        await dbAsyncConn.CreateTableAsync<Thought>();
    }

    public async Task AddThought(string content)
    {
        int result = 0;
        try
        {
            await Init();

            //basic validation - how can i improve this?
            if (string.IsNullOrEmpty(content))
                return;

            result = await dbAsyncConn.InsertAsync(
                new Thought
                {
                    Content = content.Trim(),
                    TimeSaved = DateTime.Now,
                    Image = "cloud.png"
                });

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error",
                $"Unable to save. Please try again. Error Type: {ex.Message}", "OK");
        }
    }

    List<Thought> thoughtList = new();
    public async Task<List<Thought>> GetAllThoughts()
    {
        await Init();

        thoughtList = await dbAsyncConn.Table<Thought>().ToListAsync();
        return thoughtList;
    }


}
