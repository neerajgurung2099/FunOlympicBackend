using System.Net.Mime;
using static System.Net.Mime.MediaTypeNames;

namespace FunOlympicBackEnd.Classes
{
    public class ImageHelper
    {
        private string folderPath = @"D:\NeerajGurung\Assignment\FunOlympic\fun-olympic-frontend\public\images";

         public async Task<string> UploadImage(IFormFile file)
        {
                if(!System.IO.Directory.Exists(folderPath))
                {
                    System.IO.Directory.CreateDirectory(folderPath);
                }

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName).ToLower();
                string filePath = Path.Combine(folderPath, fileName );
                using (Stream stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return fileName;
        }
    }
}
