// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System;

namespace Delegate2 // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var johnOnly = FriendList.Factory.CreateRandom(100, AllJohn);
            var gavleOnly = FriendList.Factory.CreateRandom(100, AllGavle);

            var friends = FriendList.Factory.CreateRandom(100);
 
            Console.WriteLine("\nHello to Finland");
            friends.SayHello(HelloFinland);

            Console.WriteLine("\nHello to Gavle");
            friends.SayHello(HelloGavle);

            Console.WriteLine("\nHello to Scandinavia");
            friends.SayHello(HelloScandinavia);

            Console.WriteLine("\nAlternative Hello to All");
            Action<Friend> AllHello = HelloFinland;
            AllHello += HelloGavle;
            AllHello += HelloScandinavia;
            friends.SayHello(AllHello);

        }

        public static void HelloFinland(Friend friend)
        {
            if (friend.Address.Country == "Finland")
            {
                Console.WriteLine($"Hello {friend.FirstName}, {friend.Address.Country} from Finland");
            }
        }
        public static void HelloGavle(Friend friend)
        {
            if (friend.Address.City == "Gavle")
            {
                Console.WriteLine($"Hello {friend.FirstName}, {friend.Address.City} from Gavle");
            }
        }
        public static void HelloScandinavia(Friend friend)
        {
            if (friend.Address.Country != "Finland")
            {
                Console.WriteLine($"Hello {friend.FirstName}, {friend.Address.Country} from Scandinavia");
            }
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
// 1. Go through the creation of gavleOnly and johnOnly List and understand the process when the delegate is invoked
// 2. Implement the 3 Hello delegates so you say Hello to the appropriate persons