using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Delegate2
{
    //public delegate Friend DoYourOwnRandomStuff(Friend friend);
    //public delegate void SayHelloToFriends(Friend friend);
    
    public class FriendList
    {
        public  List<Friend> myFriends = new List<Friend>();
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

        public void SayHello(Action<Friend> sayHello)
        {
            foreach (var item in myFriends)
            {
                sayHello(item);
            }       
        }


        public static class Factory
        {
            public static FriendList CreateRandom(int NrOfItems, Func<Friend,Friend> CustomInit = null)
            {

                var myList = new FriendList();
                for (int i = 0; i < NrOfItems; i++)
                {
                    var afriend = Friend.Factory.CreateRandom();
                    if (CustomInit != null)
                        afriend = CustomInit(afriend);

                    myList.myFriends.Add(afriend);
                }
                return myList;
            }
        }

        public void WriteToDisk()
        {
            string filename = fname("friends.txt");

            using (FileStream fs = File.Create(filename))
            using (TextWriter writer = new StreamWriter(fs))
            {
                writer.WriteLine(this.ToString());
            }
        }
        public void SerializeXml()
        {
            var xs = new XmlSerializer(typeof(FriendList));

            using (Stream s = File.Create(fname("friends.xml")))
            {
                xs.Serialize(s, this);
            }
        }
        public void DeSerializeXml()
        {
            var xs = new XmlSerializer(typeof(FriendList));
            FriendList flist;
            using (Stream s = File.OpenRead(fname("friends.xml")))
            {
                flist = (FriendList)xs.Deserialize(s);
                this.myFriends = flist.myFriends;
            }
        }

        static string fname(string name)
        {
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            documentPath = Path.Combine(documentPath, "ADOP", "FriendList");
            if (!Directory.Exists(documentPath)) Directory.CreateDirectory(documentPath);
            return Path.Combine(documentPath, name);
        }
    }
}
