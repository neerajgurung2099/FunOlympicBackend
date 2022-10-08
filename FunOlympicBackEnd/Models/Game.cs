namespace FunOlympicBackEnd.Models
{
    public class Game
    {
        public int GameId { get;set; }
        public int GroupId { get; set; }
        public int Category { get; set; }
        public int ParticipantType { get; set; }

        public string GameName { get; set; }
        public string GameDescription { get; set; }
        public int TotalMatches { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool View { get; set; }
        public bool Active { get; set; }
        public List<Match> matches { get; set; }
    }
}
