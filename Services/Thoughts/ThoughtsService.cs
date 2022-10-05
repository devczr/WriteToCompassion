﻿using CommunityToolkit.Mvvm.Input;
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

        var dbPath = Path.Combine(FileSystem.Current.AppDataDirectory, "ThoughtRepo.db");

        dbAsyncConn = new SQLiteAsyncConnection(dbPath);

        await dbAsyncConn.CreateTableAsync<Thought>();
    }

    public async Task AddThought(string content)
    {
        int result;
        try
        {
            await Init();

            if (string.IsNullOrEmpty(content))
                return;

            result = await dbAsyncConn.InsertAsync(
                new Thought
                {
                    ReadCount = 0,
                    Content = content.Trim(),
                    TimeSaved = DateTime.Now,
                }); 

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert($"Error",
                $"Unable to save. If the problem persists, please restart the app and try again. \n {ex.Message}", "OK");
        }
    }

    public async Task DeleteThought(int id)
    {
        try
        {
            await Init();
            await dbAsyncConn.DeleteAsync<Thought>(id);

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert($"Error",
                $"Unable to delete. If the problem persists, please restart the app and try again. \n {ex.Message}", "OK");
        }
    }

    public async Task UpdateThought(Thought thought)
    {
        int result;
        try
        {
            await Init();

            if (thought is null)
                return;
            result = await dbAsyncConn.UpdateAsync(thought);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert($"Error",
                $"Unable to save. If the problem persists, please restart the app and try again. \n {ex.Message}", "OK");
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
