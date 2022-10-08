namespace FunOlympicBackEnd.Models
{
    public class GameGroup
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public bool View { get; set; }
        public DateTime Created_Date { get; set; }
        public bool Active { get; set; }
        public List<Game> Games { get; set; }
    }
}
