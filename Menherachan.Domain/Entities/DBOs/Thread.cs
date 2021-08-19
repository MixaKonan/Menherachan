using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Menherachan.Domain.Entities.DBOs
{
    [Table("thread")]
    public class Thread
    {
        [Column("thread_id")]
        public int ThreadId { get; set; }
        [Column("board_id")]
        public int BoardId { get; set; }
        [Column("is_closed")]
        public bool IsClosed { get; set; }
        [Column("op_ip_hash")]
        public string OpIpHash { get; set; }
        [Column("anon_name")]
        public string AnonName { get; set; }
        [Column("bump_in_unix_time")]
        public long BumpInUnixTime { get; set; }

        public Board Board { get; set; }
        public ICollection<File> File { get; set; }
        public ICollection<Post> Post { get; set; }
        public ICollection<Report> Report { get; set; }
    }
}