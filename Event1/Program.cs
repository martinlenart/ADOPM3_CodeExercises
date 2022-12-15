// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System;

namespace Event1 // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
          Console.WriteLine("\nHuge friendlist");
          //FriendList.CreationProgress 
          var huge = FriendList.Factory.CreateRandom(1_000_000);
        }
    }
}
//Exercise
//1. Implement the event handler and assign it to the event CreationProgress