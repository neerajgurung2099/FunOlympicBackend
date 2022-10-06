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
        public string StartTime { get; set; }
        public string StartDate { get; set; }
        public bool Active { get; set; }
        public DateTime StartDateTime { get; set; }
        public List<MatchParticipant> matchParticipants { get; set; }
        
    }
}
