using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Menherachan.Domain.Entities.DBOs
{    [Table("board")]

    public class Board
    {
        [Key]
        [Column("board_id")]
        public int BoardId { get; set; }
        
        [Column("prefix")]
        public string Prefix { get; set; }
        
        [Column("postfix")]
        public string Postfix { get; set; }
        
        [Column("title")]
        public string Title { get; set; }
        
        [Column("description")]
        public string Description { get; set; }
        
        [Column("is_hidden")]
        public bool IsHidden { get; set; }
        
        [Column("anon_has_no_name")]
        public bool AnonHasNoName { get; set; }
        
        [Column("has_subject")]
        public bool HasSubject { get; set; }
        
        [Column("files_are_allowed")]
        public bool FilesAreAllowed { get; set; }
        
        [Column("file_limit")]
        public short FileLimit { get; set; }
        
        [Column("anon_name")]
        public string AnonName { get; set; }

        public virtual ICollection<File> File { get; set; }
        public virtual ICollection<Post> Post { get; set; }
        public virtual ICollection<Report> Report { get; set; }
        public virtual ICollection<Thread> Thread { get; set; }
    }
}