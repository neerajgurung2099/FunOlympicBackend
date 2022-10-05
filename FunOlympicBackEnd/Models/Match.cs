namespace FunOlympicBackEnd.Models
{
    public class Match
    {
        public int MatchId { get; set; }
        public string MatchTitle { get; set; }
        public int GameId { get; set; }
        public int CreatedDate { get; set; }
        public bool View { get; set; }
        public string LiveLink { get; set; }
        public TimeOnly StartTime { get; set; }
        public DateOnly StartDate { get; set; }
        public bool Active { get; set; }
        public List<MatchParticipant> matchParticipants { get; set; }
    }
}
