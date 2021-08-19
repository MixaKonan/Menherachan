using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Menherachan.Domain.Entities.DBOs
{
    [Table("post")]
    public class Post
    {
        [Column("post_id")]
        public int PostId { get; set; }
        [Column("board_id")]
        public int BoardId { get; set; }
        [Column("thread_id")]
        public int ThreadId { get; set; }
        [Column("admin_id")]
        public int? AdminId { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("subject")]
        public string Subject { get; set; }
        [Column("comment")]
        public string Comment { get; set; }
        [Column("anon_name")]
        public string AnonName { get; set; }
        [Column("is_pinned")]
        public bool IsPinned { get; set; }
        [Column("is_written_by_op")]
        public bool IsWrittenByOp { get; set; }
        [Column("time_in_unix_seconds")]
        public long CreatedAt { get; set; }
        [Column("anon_ip_hash")]
        public string AnonIpHash { get; set; }
        
        public Board Board { get; set; }
        public Thread Thread { get; set; }
        public Admin Admin { get; set; }
        public ICollection<File> File { get; set; }
    }
}