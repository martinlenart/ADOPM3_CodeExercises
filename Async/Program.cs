using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace Async
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Random byte[], could be an image
            Random rand = new Random(0);
            byte[] image = new byte[10_000_000];
            rand.NextBytes(image);

            var file = StreamManager.WriteCompressBytes(fname("AnImage.bin"), image);
            Console.WriteLine($"Sync File written: {file}");

            byte[] image2 = StreamManager.ReadCompressBytes(file, image.Length);
            Console.WriteLine($"Sync File read: {file}, nrOfBytes: {image2.Length}");


            var file1 = await StreamManager.WriteCompressBytesAsync(fname("AnAsyncImage.bin"), image);
            Console.WriteLine($"Async File written: {file1}");

            byte[] image3 = await StreamManager.ReadCompressBytesAsync(fname("AnAsyncImage.bin"), image.Length);
            Console.WriteLine($"Async File read: {file1}, nrOfBytes: {image3.Length}");
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

//Exercises:
//1.  Make an Async version of StreamManager.WriteCompressBytes(..).
//2.  Call WriteCompressBytesAsync(..) using await pattern
//3.  To be able to use await in Main(), Main has to be async, therefore change
//    "void Main(string[] args)" to "async Task Main(string[] args)"
//4.  Create a method, byte[] StreamManager.ReadCompressBytes (string path, int NrOfBytes),
//    and test it with code in Main
//5.  Create an async version of the method from point 4 above and test it.


