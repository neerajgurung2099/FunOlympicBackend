namespace FunOlympicBackEnd.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        public int CountryId { get; set; }
        public int GroupId { get; set; }
        public int ImageId { get; set; }
        public string PlayerName { get; set; }
        public string PlayerDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Active { get; set; }

    }
}
