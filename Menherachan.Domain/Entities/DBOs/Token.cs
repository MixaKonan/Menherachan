using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Menherachan.Domain.Entities.DBOs
{
    [Table("token")]
    public class Token
    {
        [Key]
        [Column("token_id")]
        public int TokenId { get; set; }
        [Column("token")]
        public string TokenString { get; set; }
        [Column("created_at")]
        public int CreatedAt { get; set; }
        [Column("expires_at")]
        public int ExpiresAt { get; set; }
    }
}