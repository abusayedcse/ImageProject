using System.Drawing;
using System.Drawing.Printing;

namespace ImageService.Helper
{
    public class ImageHelper
    {
        private string _path;
        public ImageHelper()
        {
            _path = BaseSettings.baseImagePath;
        }
        public bool RemoveDirectory(string directory)
        {
            bool isDeleted = false;
            string directoryPath = _path + directory;
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
                isDeleted = true;
            }
            return isDeleted;
        }
        public bool RemoveImage(string Path, string directory)
        {
            bool isDeleted = false;
            string deletePath = _path + Path;
            string directoryPath = _path + directory;
            if (File.Exists(deletePath))
            {
                File.Delete(deletePath);
                string[] files = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);
                if (files.Length == 0)
                    Directory.Delete(directoryPath);
                isDeleted = true;
            }
            return isDeleted;
        }
        public bool DirectoryMove(string Source, string Destination)
        {
            bool isSuccess = false;
            string DeleteDirectory = _path + Source;
            string DestinationDirectory = _path + Destination;
            if (Directory.Exists(DeleteDirectory))
            {
                if (!Directory.Exists(DestinationDirectory))
                    Directory.CreateDirectory(DestinationDirectory);
                Directory.Move(DeleteDirectory, DestinationDirectory);
                isSuccess = true;
            }
            return isSuccess;
        }
        public string GetImageUrl(string Path, string Image_64base_string, string ImageNameUnique)
        {
            string ImageUrl = string.Empty;
            if (!string.IsNullOrEmpty(Image_64base_string))
            {
                string updatePath = _path + Path;
                if (checkBase64(Image_64base_string))
                {
                    string type = GetImageType(Image_64base_string);
                    string UpdateImageUrl = ImageNameUnique + type;
                    string message = SaveImage(updatePath, UpdateImageUrl, Image_64base_string);
                    if (message == "Uploaded")
                    {
                        ImageUrl = Path + @"\" + UpdateImageUrl;
                    }
                }
            }
            return ImageUrl;
        }
        private bool checkBase64(string base64String)
        {
            try
            {
                return base64String.Contains("data:image/");
            }
            catch (Exception)
            {
                return false;
            }
        }
        private string SaveImage(string path, string imgName, string imageString)
        {
            try
            {

                var filePath = path + @"\" + imgName;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                String[] strings = imageString.Split(",");
                byte[] bytes = Convert.FromBase64String(strings[1]);
                Image image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                    image.Save(filePath);
                }
                return "Uploaded";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        private string GetImageType(string base64String)
        {
            String[] strings = base64String.Split(",");
            String extension;
            switch (strings[0])
            {
                case "data:image/jpeg;base64":
                    extension = ".jpeg";
                    break;
                case "data:image/jpg;base64":
                    extension = ".jpg";
                    break;
                case "data:image/png;base64":
                    extension = ".png";
                    break;
                case "data:image/gif;base64":
                    extension = ".gif";
                    break;
                case "data:image/bmp;base64":
                    extension = ".bmp";
                    break;
                case "data:image/x-icon;base64":
                    extension = ".ico";
                    break;
                case "data:image/svg+xml;base64":
                    extension = ".svg";
                    break;
                case "data:image/webp;base64":
                    extension = ".webp";
                    break;

                default:
                    extension = ".jpg";
                    break;
            }
            return extension;
        }
    }

    public class BaseSettings
    {
        public static string baseImagePath { get; set; } = @"C:\Temp\Images\";
    }
}
