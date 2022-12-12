// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System;

namespace IEnumerable // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nFriendList - friends1");
            var friendList1 = FriendList.Factory.CreateRandom(100);
            Console.WriteLine($"[12]: {friendList1[12]}");
            
            //modify the code in FriendList so you can iterate over all friends in friendList1
            //using a for loop
            //Your code:
            Console.WriteLine("\nfriendList1 using for - loop");

            //modify the code in FriendList so you can iterate over all friends in friendList1
            //using a foreach loop
            //Your code:
            Console.WriteLine("\nfriendList1 using foreach - loop");

            //create a list from friendList1 using simply copyfriends = friendList1.ToList()
            //Very that you have created a deepcopy by modifying an element and compare
            //Your code:
            Console.WriteLine("\ncopyfriends using foreach - loop");
        }
    }
}
//Exercises make a class an enumerable
//1. Modify the code according to the instructions above.