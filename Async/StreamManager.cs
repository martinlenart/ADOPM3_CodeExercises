using System;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace Async
{
	public static class StreamManager
	{

        public static Task<string> WriteCompressBytesAsync(string path, byte[] array)
        {
            return Task.Run(() => WriteCompressBytes(path, array));
        }

        public static string WriteCompressBytes(string path, byte[] array)
        {
            using (Stream s = File.Create(path))
            using (Stream ds = new GZipStream(s, CompressionMode.Compress))
            using (BinaryWriter w = new BinaryWriter(ds))
            {
                w.Write(array);
                return path;
            }
        }

        public static Task<byte[]> ReadCompressBytesAsync(string path, int NrOfBytes)
        {
            return Task.Run(() => ReadCompressBytes(path, NrOfBytes));
        }

        public static byte[] ReadCompressBytes(string path, int NrOfBytes)
        {
            using (Stream s = File.OpenRead(path))
            using (Stream ds = new GZipStream(s, CompressionMode.Decompress))
            using (BinaryReader r = new BinaryReader(ds))
            {
                byte[] imageRead = r.ReadBytes(NrOfBytes);
                return imageRead;
            }
        }
    }
}

