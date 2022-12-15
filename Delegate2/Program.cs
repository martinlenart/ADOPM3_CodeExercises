// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System;

namespace Delegate2 // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var friends = FriendList.Factory.CreateRandom(100);
 
            Console.WriteLine("\nHello to Finland");
            friends.SayHello(HelloFinland);

            Console.WriteLine("\nHello to Gavle");
            friends.SayHello(HelloGavle);

            Console.WriteLine("\nHello to Scandinavia");
            friends.SayHello(HelloScandinavia);

            var gavleOnly = FriendList.Factory.CreateRandom(100, AllGavle);

        }

        public static void HelloFinland(Friend friend)
        {
        }
        public static void HelloGavle(Friend friend)
        {
        }
        public static void HelloScandinavia(Friend friend)
        {
        }


        public static Friend AllJohn(Friend orgFriend)
        {
            orgFriend.FirstName = "John";
            return orgFriend;
        }

        public static Friend AllGavle(Friend orgFriend)
        {
            var newAddress = orgFriend.Address;
            newAddress.City = "Gavle";

            orgFriend.Address = newAddress;
            return orgFriend;
        }
    }
}

//Exercise
// 1. Go through the creation of gavleOnly List and understand the process when the delegate is invoked
// 2. Implement the 3 Hello delegates so you say Hello to the appropriate persons