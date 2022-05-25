using System;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace Streams
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create a random byte[]
            byte[] imageSrc = new byte[10_000_000];
            new Random().NextBytes(imageSrc);

            //Stream compressed to and from File
            var size = StreamManager.StreamToFile(imageSrc, fname("Streams_uncompressedbytes.bin"));
            var imageCopy = StreamManager.StreamFromFile(imageSrc.Length, fname("Streams_uncompressedbytes.bin"));
            var imageCopy1 = StreamManager.StreamFromFile(fname("Streams_uncompressedbytes.bin"));

            verifyContent(imageSrc, imageCopy);
            verifyContent(imageSrc, imageCopy1);
            Console.WriteLine($"File stream write/read {size:N0} bytes confirmed");


            static string fname(string name)
            {
                var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                documentPath = Path.Combine(documentPath, "AOOP2", "Examples");
                if (!Directory.Exists(documentPath)) Directory.CreateDirectory(documentPath);
                return Path.Combine(documentPath, name);
            }

            static void verifyContent(byte[] imageSrc, byte[] imageCopy)
            {
                //Verify that image contents are identical
                for (int i = 0; i < imageSrc.Length; i++)
                {
                    if (imageSrc[i] != imageCopy[i])
                        throw new BadImageFormatException();
                }
            }
        }


        //Exercises:
        //1.    Go through StreamManager.StreamFromFile() which does not take any size input,
        //      to understand how it works. Can you explain it?
        //2.    Modify StreamManager.StreamToFile() and both StreamManager.StreamFromFile() to
        //      include GZip compression
        //3.    Add Methods to stream the content of AppLog in project Logger
    }
}
