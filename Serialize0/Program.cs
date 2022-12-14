// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System;

namespace Serialization0 // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var friendsToDisk = FriendList.Factory.CreateRandom(1_000);
            Console.WriteLine(friendsToDisk.myFriends.Count);

            friendsToDisk.SerializeXml("Friends.xml");
            var xmlFriends = FriendList.DeSerializeXml("Friends.xml");

            Console.WriteLine(xmlFriends?.myFriends.Count);

            friendsToDisk.SerializeJson("Friends.json");
            var jsonFriends = FriendList.DeSerializeJson("Friends.json");

            Console.WriteLine(jsonFriends?.myFriends.Count);
        }
    }
}
//Exercise:
//1. Implement SerializeXml(), DeSerializeXml(), SerializeJson(), and DeSerializeJson()
//2. Open the xml file in Excel