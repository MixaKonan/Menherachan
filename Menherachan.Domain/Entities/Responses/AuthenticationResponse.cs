using System.IdentityModel.Tokens.Jwt;

namespace Menherachan.Domain.Entities.Responses
{
    public class AuthenticationResponse
    {
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Bearer { get; set; }
    }
}