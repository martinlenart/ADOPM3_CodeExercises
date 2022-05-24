using System;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace Async
{
	public static class StreamManager
	{
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
    }
}

