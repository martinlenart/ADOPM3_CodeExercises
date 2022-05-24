using System;
using System.IO;
using System.IO.Compression;

namespace Async
{
    class Program
    {
        static void Main(string[] args)
        {
            //Random byte[], could be an image
            Random rand = new Random(0);
            byte[] image = new byte[10_000_000];
            rand.NextBytes(image);

            var file = StreamManager.WriteCompressBytes(fname("AnImage.bin"), image);
            Console.WriteLine($"File written: {file}");
        }

        static string fname(string name)
        {
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            documentPath = Path.Combine(documentPath, "AOOP2", "Examples");
            if (!Directory.Exists(documentPath)) Directory.CreateDirectory(documentPath);
            return Path.Combine(documentPath, name);
        }
    }
}

