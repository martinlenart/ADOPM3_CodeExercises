using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSafeData
{
    class Program
    {
        public class Vehicle
        {
            public string RegistrationNumber { get; set; }
            public string Owner { get; set; }
        }

        //Thread Safe Datastructure
        public class VehicleStorage
        {
            object _locker = new object();
            List<Vehicle> _vehicles = new List<Vehicle>();

            public void SetData (string regNr, string owner)
            {
                lock (_locker)
                {
                    var v = new Vehicle() { RegistrationNumber = regNr, Owner = owner };
                    _vehicles.Add(v);
                }
            }

            public Vehicle GetData (int idx)
            {
                lock(_locker)
                {
                    return _vehicles[idx];
                }
            }

            public bool CheckConsistency()
            {
                lock (_locker)
                {
                    foreach (var v in _vehicles)
                    {
                        //Verify data consistency - give error if not consistent
                        if ((v.RegistrationNumber, v.Owner) != ("ABC 123", "Kalle Anka") &&
                            (v.RegistrationNumber, v.Owner) != ("HKL 556", "Musse Pigg"))
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
        }

        static void Main(string[] args)
        {
            var myCar = new VehicleStorage();
            var rnd = new Random();

            var t1 = Task.Run(() =>
            {
                var rnd = new Random();
                for (int i = 0; i < 1000; i++)
                {
                    //Write Data to Vehicle, "ABC 123", "Kalle Anka"
                    myCar.SetData("ABC 123", "Kalle Anka");

                    //introduce some system delay
                    Task.Delay(rnd.Next(1, 5)).Wait();

                    //Read Data from Vehicle
                    var t = myCar.GetData(i);

                    if (!myCar.CheckConsistency())
                    {
                        Console.WriteLine("Data inconsistent!");
                    }
                }
                Console.WriteLine("t1 Finished");
            });

            var t2 = Task.Run(() =>
            {
                var rnd = new Random();
                for (int i = 0; i < 1000; i++)
                {
                    //Write Data to Vehicle, "HKL 556", "Musse Pigg"
                    myCar.SetData("HKL 556", "Musse Pigg");

                    //introduce some system delay
                    Task.Delay(rnd.Next(1, 5)).Wait();

                    //Read Data from Vehicle
                    var t = myCar.GetData(i);

                    //Verify data consistency - give error if not consistent
                    if (!myCar.CheckConsistency())
                    {
                        Console.WriteLine("Data inconsistent!");
                    }
                }
                Console.WriteLine("t2 Finished");
            });

            Task.WaitAll(t1, t2);
            Console.WriteLine("All Finished");
        }


    }
}
/*  Exercise
    1. Make class Vehicle Thread safe using lock(...)
    2.  - Have task t1 write 1000 times "ABC 123", "Kalle Anka" to myCar
        - Have task t2 write 1000 times "HKL 556", "Musse Pigg" to myCar
        - Verify data consistency
        - Discuss in the group what is data consistency in case of class Vehicle. Is your code living up to it?
*/
