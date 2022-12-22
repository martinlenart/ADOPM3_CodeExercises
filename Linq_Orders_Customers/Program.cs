using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Linq_Orders_Customers
{
    static class LinqExtensions
    {
        public static void Print<T>(this IEnumerable<T> collection)
        {
            collection.ToList().ForEach(item => Console.WriteLine(item));
        }
    }
    public class OrderCustomer
    {
        public IOrder order { get; set; }
        public ICustomer customer { get; set; }
    }

    public class CustomerOrders
    {
        public ICustomer customer { get; set; }
        public IEnumerable<IOrder> orders { get; set; }
    }

    class Program
    {
        const int NrOfCustomers = 10_000;
        const int MaxNrOfOrdersPerCustomer = 20;

        static void Main(string[] args)
        {
            //Create Order and customer Lists
            List<Order> OrderList = new List<Order>();
            List<Customer> CustomerList = new List<Customer>();

            var rnd = new Random();
            for (int c = 0; c < NrOfCustomers; c++)
            {
                var cus = Customer.Factory.CreateWithRandomData();
                CustomerList.Add(cus);

                //Create a random number of order for the customer. Could be 0
                for (int o = 0; o < rnd.Next(0, MaxNrOfOrdersPerCustomer + 1); o++)
                {
                    OrderList.Add(Order.Factory.CreateWithRandomData(cus.CustomerID));
                }
            }

            QueryCustomersWithLinq(CustomerList);
            QueryOrdersWithLinq(CustomerList, OrderList);
        }

        private static void QueryCustomersWithLinq(IEnumerable<ICustomer> customers)
        {
            Console.WriteLine($"Nr of Customer: {customers.Count()}");
        }

        private static void QueryOrdersWithLinq(IEnumerable<ICustomer> customers, IEnumerable<IOrder> orders)
        {
            Console.WriteLine($"\nNr of orders: {orders.Count()}");
        }
    }
}

///Exercises:
//1.    Antalet kunder, Antalet kunder i Sverige, Äldsta kundens födelsedag, Yngsta kundens födelsedag
//2.    Använd GroupBy för att lista antalet kunder per land
//3.    Antalet kunder med ett efternamn som slutar på 'son'

//4.    Antalet ordrar och totalt ordervärde av de 5 största ordrarna
//5.    Använd Join för att lista kund och ordervärde för de 5 största ordrarna.
//          Hint: använd Join för att skapa en lista av  OrderCustomer

//6.    Använd GroupJoin för att lista de 5 största kunderna baserat på ordervärde
//          Hint: använd GroupJoin för att skapa en lista av  CustomerOrders
