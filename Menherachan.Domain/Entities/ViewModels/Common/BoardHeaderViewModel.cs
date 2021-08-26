namespace Menherachan.Domain.Entities.ViewModels.Common
{
    public class BoardHeaderViewModel
    {
        public string Prefix { get; set; }
        public string Postfix { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }

        public BoardHeaderViewModel()
        {
            
        }

        public BoardHeaderViewModel(string prefix, string postfix, string title, string description, string link)
        {
            Prefix = prefix;
            Postfix = postfix;
            Title = title;
            Description = description;
            Link = link;
        }
    }
}