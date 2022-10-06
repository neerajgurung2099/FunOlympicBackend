using Microsoft.AspNetCore.Hosting.Server;

namespace FunOlympicBackEnd.Models
{
    public class MatchParticipant
    {
        public int MatchParticipantId { get;set; }
        public int MatchId { get; set; }
        public int ParticipantId { get; set; }
        public decimal Points { get; set; }
    }
}
