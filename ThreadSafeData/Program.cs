using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSafeData
{
    class Program
    {
        //Thread Safe Datastructure
        public class Vehicle
        {
            string RegistrationNumber;
            string Owner;
        }
        static void Main(string[] args)
        {
            var myCar = new Vehicle();
            var rnd = new Random();

            var t1 = Task.Run(() =>
            {
                var rnd = new Random();
                for (int i = 0; i < 1000; i++)
                {
                    //Write Data to Vehicle, "ABC 123", "Kalle Anka"

                    //introduce some system delay
                    //Task.Delay(rnd.Next(1, 5)).Wait();

                    //Read Data from Vehicle

                    //Verify data consistency - give error if not consistent                   
                 }
                Console.WriteLine("t1 Finished");
            });

            var t2 = Task.Run(() =>
            {
                var rnd = new Random();
                for (int i = 0; i < 1000; i++)
                {
                    //Write Data to Vehicle, "HKL 556", "Musse Pigg"

                    //introduce some system delay
                    //Task.Delay(rnd.Next(1, 5)).Wait();

                    //Read Data from Vehicle

                    //Verify data consistency - give error if not consistent                   
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
        - Have task t2 write 1000 times "ABC 123", "HKL 556", "Musse Pigg"
        - Verify data consistency
        - Discuss in the group what is data consistency in case of class Vehicle. Is your code living up to it?
*/
