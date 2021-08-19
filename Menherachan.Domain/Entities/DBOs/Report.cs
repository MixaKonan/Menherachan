using System.ComponentModel.DataAnnotations.Schema;

namespace Menherachan.Domain.Entities.DBOs
{
    [Table("report")]
    public class Report
    {
        [Column("report_id")]
        public int ReportId { get; set; }
        [Column("board_id")]
        public int BoardId { get; set; }
        [Column("thread_id")]
        public int ThreadId { get; set; }
        [Column("post_id")]
        public int PostId { get; set; }
        [Column("reason")]
        public string Reason { get; set; }
        [Column("report_time_in_unix_seconds")]
        public long ReportTime { get; set; }
        
        public Board Board { get; set; }
        public Thread Thread { get; set; }
        public Post Post { get; set; }
    }
}