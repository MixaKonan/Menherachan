namespace Menherachan.Domain.Entities.ViewModels.Common
{
    public class FileViewModel
    {
        public string FileName { get; set; }
        public string ThumbnailName { get; set; }
        public string Info { get; set; }
        
        public FileViewModel()
        {
            
        }
        
        public FileViewModel(string fileName, string thumbnailName, string info)
        {
            FileName = fileName;
            ThumbnailName = thumbnailName;
            Info = info;
        }
    }
}