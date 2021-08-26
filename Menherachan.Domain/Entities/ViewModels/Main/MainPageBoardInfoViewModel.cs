namespace Menherachan.Domain.Entities.ViewModels.Main
{
    public class MainPageBoardInfoViewModel
    {
        public string Prefix { get; set; }
        public int ThreadCount { get; set; }
        public int PostCount { get; set; }
        public int FileCount { get; set; }

        public MainPageBoardInfoViewModel()
        {
            
        }
        public MainPageBoardInfoViewModel(string prefix, int threadCount, int postCount, int fileCount)
        {
            Prefix = prefix;
            ThreadCount = threadCount;
            PostCount = postCount;
            FileCount = fileCount;
        }
    }
}