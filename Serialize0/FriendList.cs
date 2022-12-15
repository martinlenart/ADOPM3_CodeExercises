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
            var xs = new XmlSerializer(typeof(FriendList));
            using (Stream s = File.Create(fname(xmlFileName)))
            {
                xs.Serialize(s, this);
            }
        }
        public static FriendList DeSerializeXml(string xmlFileName)
        {
            //Your Code
            var xs = new XmlSerializer(typeof(FriendList));
            FriendList flist;
            using (Stream s = File.OpenRead(fname(xmlFileName)))
            {
                flist = (FriendList)xs.Deserialize(s);
                return flist;
            }
        }
        public void SerializeJson(string jsonFileName)
        {
            //Your Code
            string sJson = JsonSerializer.Serialize<FriendList>(this, new JsonSerializerOptions() { WriteIndented = true });
            
            using (Stream s = File.Create(fname(jsonFileName)))
            using (TextWriter writer = new StreamWriter(s))
                writer.Write(sJson);

        }
        public static FriendList DeSerializeJson(string jsonFileName)
        {
            //Your Code
            using (Stream s = File.OpenRead(fname(jsonFileName)))
            using (TextReader reader = new StreamReader(s))
            {
                string sTmp = reader.ReadToEnd();
                var flist = JsonSerializer.Deserialize<FriendList>(sTmp);
                return flist;
            }
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
