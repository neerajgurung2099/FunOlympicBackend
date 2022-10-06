namespace FunOlympicBackEnd.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public int CountryId { get; set; }
        public int GroupId { get; set; }
        public int ImageId { get; set; }
        public string TeamName { get; set; }
        public string TeamDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Active { get; set; }
    }
}
