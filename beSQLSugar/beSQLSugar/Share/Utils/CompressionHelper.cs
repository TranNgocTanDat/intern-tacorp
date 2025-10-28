using System.IO.Compression;
using System.Text;

namespace beSQLSugar.Share.Utils
{
    public static class CompressionHelper
    {
        // Nén string -> Base64
        public static string Compress(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;

            var bytes = Encoding.UTF8.GetBytes(text);
            using var msi = new MemoryStream(bytes);
            using var mso = new MemoryStream();
            using (var gs = new GZipStream(mso, CompressionMode.Compress))
            {
                msi.CopyTo(gs);
            }
            return Convert.ToBase64String(mso.ToArray());
        }

        // Giải nén Base64 -> string
        public static string Decompress(string compressedText)
        {
            if (string.IsNullOrEmpty(compressedText)) return compressedText;

            var bytes = Convert.FromBase64String(compressedText);
            using var msi = new MemoryStream(bytes);
            using var mso = new MemoryStream();
            using (var gs = new GZipStream(msi, CompressionMode.Decompress))
            {
                gs.CopyTo(mso);
            }
            return Encoding.UTF8.GetString(mso.ToArray());
        }
    }
}
