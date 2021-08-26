namespace Menherachan.Domain.Entities.ViewModels.Common
{
    public class AdminViewModel
    {
        public int AdminId { get; set; }
        public string Nickname { get; set; }
        public string ColorCode { get; set; }

        public AdminViewModel()
        {
            
        }

        public AdminViewModel(int adminId, string nickname, string colorCode)
        {
            AdminId = adminId;
            Nickname = nickname;
            ColorCode = colorCode;
        }
    }
}