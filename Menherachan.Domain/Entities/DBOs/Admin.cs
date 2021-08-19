using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Menherachan.Domain.Entities.DBOs
{
    [Table("admin")]
    public class Admin
    {
        [Column("admin_id")]
        public int AdminId { get; set; }
        [Column("login")]
        public string Login { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("password_hash")]
        public string PasswordHash { get; set; }
        [Column("can_delete_posts")]
        public bool CanDeletePosts { get; set; }
        [Column("can_close_threads")]
        public bool CanCloseThreads { get; set; }
        [Column("has_access_to_panel")]
        public bool HasAccessToPanel { get; set; }
        [Column("can_ban_users")]
        public bool CanBanUsers { get; set; }
        [Column("nickname_color_code")]
        public string NicknameColorCode { get; set; }

        public virtual ICollection<Ban> Ban { get; set; }
        public virtual ICollection<Post> Post { get; set; }
    }
}