using static System.Net.Mime.MediaTypeNames;

namespace FunOlympicBackEnd.Classes
{
    public class ImageHelper
    {
        private string folderPath = @"d:\funolympic-images";

         public async Task<string> UploadImage(IFormFile file)
        {
                if(!System.IO.Directory.Exists(folderPath))
                {
                    System.IO.Directory.CreateDirectory(folderPath);
                }
                string filePath = Path.Combine(folderPath, Guid.NewGuid().ToString());
                using (Stream stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return filePath;
        }
    }
}
