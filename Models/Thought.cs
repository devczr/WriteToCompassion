using SQLite;

namespace WriteToCompassion.Models;

public class Thought
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Content { get; set; }
    public string Image { get; set; }
    public DateTime TimeSaved { get; set; }

    public DateTime TimeRead { get; set; }


}
