using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Event1
{
    public class FriendList
    {
        /// <summary>
        /// Event is fired every 10_000 friend created.
        /// Parameter object? sender - list being created
        /// parameter TEventArgs e - int, being the number of friends created
        /// </summary>
        public static event EventHandler<int> CreationProgress;


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
            public static FriendList CreateRandom(int NrOfItems, Func<Friend,Friend> doIt = null)
            {

                var myList = new FriendList();
                for (int i = 0; i < NrOfItems; i++)
                {
                    var afriend = Friend.Factory.CreateRandom();
                    if (doIt != null)
                        afriend = doIt(afriend);

                    myList.myFriends.Add(afriend);

                    if (i%10_000 ==0)
                    {
                        CreationProgress?.Invoke(myList, i);
                    }
                }
                return myList;
            }
        }
    }
}
