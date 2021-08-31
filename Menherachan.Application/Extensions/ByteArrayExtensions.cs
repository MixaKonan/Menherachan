using System.Text;

namespace Menherachan.Application.Extensions
{
    public static class ByteArrayExtensions
    {
        public static string GetFormattedString(this byte[] arr)
        {
            var stringBuilder = new StringBuilder();

            foreach (var @byte in arr)
            {
                stringBuilder.AppendFormat("{0:x2}", @byte);
            }

            return stringBuilder.ToString();
        }
    }
}