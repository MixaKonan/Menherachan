using System.ComponentModel.DataAnnotations.Schema;

namespace Menherachan.Domain.Entities.DBOs
{
    [Table("ban")]
    public class Ban
    {
        [Column("ban_id")]
        public int BanId { get; set; }
        [Column("admin_id")]
        //[ForeignKey("admin_id")]
        public int AdminId { get; set; }
        [Column("anon_ip_hash")]
        public string AnonIpHash { get; set; }
        [Column("ban_time_in_unix_seconds")]
        public long BanTimeInUnixSeconds { get; set; }
        [Column("term")]
        public long Term { get; set; }
        [Column("reason")]
        public string Reason { get; set; }
        
        public Admin Admin { get; set; }
    }
}