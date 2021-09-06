using System;
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
        public DateTime CreatedAt { get; set; }
        [Column("expires_at")]
        public DateTime ExpiresAt { get; set; }
    }
}