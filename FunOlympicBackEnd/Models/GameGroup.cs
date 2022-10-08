namespace FunOlympicBackEnd.Models
{
    public class GameGroup
    {
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public bool View { get; set; }
        public DateTime Created_Date { get; set; }
        public bool Active { get; set; }
        public List<Game> games { get; set; }
    }
}
