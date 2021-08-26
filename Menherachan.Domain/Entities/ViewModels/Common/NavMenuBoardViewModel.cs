namespace Menherachan.Domain.Entities.ViewModels.Common
{
    public class NavMenuBoardViewModel
    {
        public string Prefix { get; set; }
        public string Postfix { get; set; }

        public NavMenuBoardViewModel()
        {
            
        }

        public NavMenuBoardViewModel(string prefix, string postfix)
        {
            Prefix = prefix;
            Postfix = postfix;
        }
    }
}