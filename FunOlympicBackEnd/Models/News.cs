namespace FunOlympicBackEnd.Models
{
    public class News
    {
        public int NewsId { get; set; }
        public string NewsTitle { get; set; }
        public string NewsDescription { get; set; }
        public int GroupId { get; set; }
        public bool View { get; set; }
    }
}
