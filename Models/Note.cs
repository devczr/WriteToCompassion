using SQLite;

namespace WriteToCompassion.Models
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Inscription { get; set; }

        public DateTime TimeRead { get; set; }
        
    }

}