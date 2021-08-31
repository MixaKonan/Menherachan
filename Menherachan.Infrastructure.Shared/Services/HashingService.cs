using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Menherachan.Application.Extensions;
using Menherachan.Application.Interfaces.Services;

namespace Menherachan.Infrastructure.Shared.Services
{
    public class HashingService : IHashingService
    {
        public static async Task<string> GetHashFromStringAsync(string str)
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(str));
            var result = await new SHA256Managed().ComputeHashAsync(stream);
            return result.GetFormattedString();
        }
    }
}