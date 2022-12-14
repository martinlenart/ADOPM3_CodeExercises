// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System;

namespace Streams0 // Note: actual namespace depends on the project name.
{
    public class Program
    {
        static void Main(string[] args)
        {
            var friendsToDisk = FriendList.Factory.CreateRandom(1_000);
            
            var s = friendsToDisk.WriteToDisk("Friends.txt");
            Console.WriteLine(s);

            s = friendsToDisk.WriteToDiskCompressed("Friends.zip");
            Console.WriteLine(s);

            s = friendsToDisk.UncompressToDisk("Friends.zip", "Friends2.txt");
            Console.WriteLine(s);

        }
    }
}

//Exercises:
//1. Implement friendsToDisk.WriteToDisk, friendsToDisk.WriteToDiskCompressed and friendsToDisk.UncompressToDisk
//   Write to disk and study the files created. The content of the txt files and compare the size to the compressed file