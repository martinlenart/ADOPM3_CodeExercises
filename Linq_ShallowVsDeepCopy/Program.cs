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
    public class OrderCustomer
    {
        public Customer cus { get; set; }
        public Order ord { get; set; }
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


            #region Shallow vs deep copy
            /*
            //All customers in Sweden
            var custSweden = OrderList
                .Join(CustomerList, o => o.CustomerID, c => c.CustomerID, (o, c) => new OrderCustomer { cus = c, ord=o })
                .Where(oc => oc.cus.Country == "Sverige");
 
            //Largest Customer
            //var largestInSweden = custSweden.OrderByDescending(oc => oc.ord.Value).First();
            var largestInSweden = custSweden.OrderByDescending(oc => oc.ord.Value).First();
            Console.WriteLine($"Largest customer in Sweden:\n{largestInSweden.cus}\n{largestInSweden.ord}");

            //Do something with the list
            DoSomething(custSweden);

            //Change the ordervalue of largest customer
            largestInSweden.ord.Value = 0;

            //Do something again with the list
            DoSomething(custSweden);

            */
            #endregion
        }

        private static void QueryCustomersWithLinq(IEnumerable<Customer> customers)
        {
        }

        private static void QueryOrdersWithLinq(IEnumerable<Customer> customers, IEnumerable<Order> orders)
        {


            Console.WriteLine("\ntop 5 Orders with customer joined in via Join:");
            var orderCustomer = orders.Join(customers, o => o.CustomerID, c => c.CustomerID,
                (o, c) => new OrderCustomer {ord = o, cus = c });

            orderCustomer.OrderByDescending(oc => oc.ord.Value).Take(5).Print();

            
            Console.WriteLine("\ntop 5 Customers from order value via GroupJoin:");
            var CustomerOrders = customers.GroupJoin(orders, c => c.CustomerID, o => o.CustomerID, (cust, orders) => new { cust, orders });
            foreach (var co in CustomerOrders.OrderByDescending(co => co.orders.Sum(o => o.Value)).Take(5))
            {
                Console.WriteLine($"Cust: {co.cust.CustomerID}, OrderValue: {co.orders.Sum(o => o.Value):C2}");
            }
            

        }

        private static void DoSomething(IEnumerable<OrderCustomer> oc)
        {
            Console.WriteLine($"\nOrder value in Sweden:{oc.Sum(oc=>oc.ord.Value):C2}");
        }
    }
}
///Exercises:
//1.    Antalet kunder, Antalet kunder i Sverige, Äldsta kundens födelsedag, Yngsta kundens födelsedag
//2.    Använd GroupBy för att lista antalet kunder per land
//3.    Antalet kunder med ett efternamn som slutar på 'son'

//4.    Antalet ordrar och totalt ordervärde av de 5 största ordrarna
//5.    Använd Join för att lista kund och ordervärde för de 5 största ordrarna

//6.    Använd GroupJoin för att lista de 5 största kunderna baserat på ordervärde
