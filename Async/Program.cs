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
            byte[] imageSrc = new byte[10_000_000];
            rand.NextBytes(imageSrc);

            //Write and read synchronously
            var syncFileName = StreamManager.WriteCompressBytes(fname("AnImage.bin"), imageSrc);
            Console.WriteLine($"Sync File written: {syncFileName}");

            byte[] imageCopy1 = StreamManager.ReadCompressBytes(syncFileName, imageSrc.Length);
            Console.WriteLine($"Sync File read: {syncFileName}, nrOfBytes: {imageCopy1.Length}");

            byte[] imageCopy2 = StreamManager.ReadCompressBytes(syncFileName);
            Console.WriteLine($"Sync File read: {syncFileName}, nrOfBytes: {imageCopy2.Length}");

            //Verify that image contents are identical
            for (int i = 0; i < imageSrc.Length; i++)
            {
                if (imageSrc[i] != imageCopy1[i] || imageSrc[i] != imageCopy2[i])
                    throw new BadImageFormatException();
            }

            //Write and read Asynchronously
            Console.WriteLine();
            var asyncFileName = await StreamManager.WriteCompressBytesAsync(fname("AnAsyncImage.bin"), imageSrc);
            Console.WriteLine($"Async File written: {asyncFileName}");

            byte[] imageCopy3 = await StreamManager.ReadCompressBytesAsync(asyncFileName, imageSrc.Length);
            Console.WriteLine($"Async File read: {asyncFileName}, nrOfBytes: {imageCopy3.Length}");

            byte[] imageCopy4 = await StreamManager.ReadCompressBytesAsync(asyncFileName);
            Console.WriteLine($"Async File read: {asyncFileName}, nrOfBytes: {imageCopy4.Length}");

            //Verify that image contents are identical
            for (int i = 0; i < imageSrc.Length; i++)
            {
                if (imageSrc[i] != imageCopy3[i] || imageSrc[i] != imageCopy4[i])
                    throw new BadImageFormatException();
            }

            Console.WriteLine("\nAll images are identical");

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


