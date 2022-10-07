


namespace FunOlympicBackEnd.Models
{

    public class Gallery
    {
        public int UkId { get; set; }
        public int ImageId { get; set; }
        public bool View { get; set; }
        public IFormFile Image { get; set; }
    }
}
