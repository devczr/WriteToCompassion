using SQLite;

namespace WriteToCompassion.Models;

public class Thought
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Content { get; set; }

    public int ReadCount { get; set; }

    public string ReadSessionID { get; set; }

    public DateTime TimeSaved { get; set; }


}
