using System.ComponentModel.DataAnnotations.Schema;

namespace Menherachan.Domain.Entities.DBOs
{
    [Table("file")]
    public class File
    {
        [Column("file_id")]
        public int FileId { get; set; }
        [Column("board_id")]
        public int BoardId { get; set; }
        [Column("thread_id")]
        public int ThreadId { get; set; }
        [Column("post_id")]
        public int PostId { get; set; }
        [Column("file_name")]
        public string FileName { get; set; }
        [Column("thumbnail_name")]
        public string ThumbnailName { get; set; }
        [Column("info")]
        public string Info { get; set; }

        public Board Board { get; set; }
        public Post Post { get; set; }
        public Thread Thread { get; set; }
    }
}