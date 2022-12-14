using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Text.Json;

namespace Serialization0
{
    public class FriendList
    {
        //Note myFriends { get; set; } - it needs to be a public property for Json serialization
        public List<Friend> myFriends { get; set; } = new List<Friend>();
        public Friend this[int idx]=> myFriends[idx];

        public override string ToString()
        {
            string sRet = "";
            foreach (var item in myFriends)
            {
                sRet += item.ToString() + "\n";
            }
            return sRet;
        }

        public static class Factory
        {
            public static FriendList CreateRandom(int NrOfItems)
            {

                var myList = new FriendList();
                for (int i = 0; i < NrOfItems; i++)
                {
                    var afriend = Friend.Factory.CreateRandom();
                    myList.myFriends.Add(afriend);
                }
                return myList;
            }
        }

        public void SerializeXml(string xmlFileName)
        {
            //Your Code
        }
        public static FriendList DeSerializeXml(string xmlFileName)
        {
            //Your Code
            return null; //your code
        }
        public void SerializeJson(string jsonFileName)
        {
            //Your Code
        }
        public static FriendList DeSerializeJson(string jsonFileName)
        {
            //Your Code
            return null; //your code
        }

        static string fname(string name)
        {
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            documentPath = Path.Combine(documentPath, "ADOP", "Serialization");
            if (!Directory.Exists(documentPath)) Directory.CreateDirectory(documentPath);
            return Path.Combine(documentPath, name);
        }
    }
}
