using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Linq_ShallowVsDeepCopy
{
    static class LinqExtensions
    {
        public static void Print<T>(this IEnumerable<T> collection)
        {
            collection.ToList().ForEach(item => Console.WriteLine(item));
        }
    }
 
    class Program
    {
        const int NrOfCustomers = 10_000;
 
        static void Main(string[] args)
        {
            //Create Order and customer Lists
            List<Customer> CustomerList = new List<Customer>();

            var rnd = new Random();
            for (int c = 0; c < NrOfCustomers; c++)
            {
                var cus = Customer.Factory.CreateWithRandomData();
                CustomerList.Add(cus);
            }

 
            // Oldest Customer in Sweden
            var oldestCustomerSweden = CustomerList.Where(c => c.Country == "Sverige")
                .OrderByDescending(c => c.BirthDate).First();
            Console.WriteLine($"Oldest customer in Sweden:\n{oldestCustomerSweden}");


            //All customers in Sweden

            #region exercise 1
            //Shallow copy - default in Linq
            //var custSweden = CustomerList.Where(c => c.Country == "Sverige");

            //Deep copy - and force Linq to enumerate
            var custSweden = CustomerList.Where(c => c.Country == "Sverige").Select(c => new Customer(c)).ToList();
            #endregion

            //Do something with the list
            CustomerCountPerCountry(custSweden);

            //BUG - Change the ordervalue of largest customer
            oldestCustomerSweden.Country = null;

            //Do something again with the list
            CustomerCountPerCountry(custSweden);


        }

        private static void CustomerCountPerCountry(IEnumerable<Customer> cus)
        {
            foreach (var group in cus.GroupBy(c => c.Country))
            {
                Console.WriteLine($"{group.Key}: {group.Count()}");
            }
        }
    }
}
///Exercises:
//1.    Explore the difference what happens with Shallow vs Deep copy of the customer list
