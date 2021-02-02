using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeZero.Shared.Extensions.IO
{
    public static class StreamExtensions
    {
        public static byte[] GetAllBytes(this Stream stream)
        {
            using var memoryStream = new MemoryStream();
            stream.Position = 0;
            stream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }

        public static async Task<byte[]> GetAllBytesAsync(this Stream stream, CancellationToken cancellationToken = default)
        {
            using var memoryStream = new MemoryStream();
            stream.Position = 0;
            await stream.CopyToAsync(memoryStream, cancellationToken);
            return memoryStream.ToArray();
        }

        public static Task CopyToAsync(this Stream stream, Stream destination, CancellationToken cancellationToken)
        {
            stream.Position = 0;
            return stream.CopyToAsync(
                destination,
                81920, //this is already the default value, but needed to set to be able to pass the cancellationToken
                cancellationToken
            );
        }

        /// <summary>
        /// Write File in UTF8 from MemoryStream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="path"></param>
        public static void WriteToFileUtf8(this MemoryStream stream, string path)
        {
            using var writer = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
            Encoding enc = new UTF8Encoding(false, false);
            var chars = enc.GetString(stream.ToArray());
            var bytes = enc.GetBytes(chars.ToCharArray());
            writer.Write(bytes, 0, bytes.Length);
        }
    }
}
