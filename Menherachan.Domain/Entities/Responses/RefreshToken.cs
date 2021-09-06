using System;

namespace Menherachan.Domain.Entities.Responses
{
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public RefreshToken()
        {
            CreatedAt = DateTime.Now;
        }

        public override string ToString()
        {
            return Token;
        }
    }
}