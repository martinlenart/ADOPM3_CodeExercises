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
          
          //Assign your event handler to FriendList.CreationProgress 
          // Your code


          var huge = FriendList.Factory.CreateRandom(1_000_000);
        }

        //Declare your Eventhandler
        //Your code
    }
}
//Exercise
//1. In Friendlist implement the firing of an event, Creation Progress, during the CreateRandom() method
//   - parameter to the event is Nr of friends created
//2. In Program implement the event handler and assign it to the event CreationProgress