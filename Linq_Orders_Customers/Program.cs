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
        }

        private static void QueryCustomersWithLinq(IEnumerable<ICustomer> customers)
        {
            Console.WriteLine($"Nr of Customer: {customers.Count()}");
            Console.WriteLine("\nFirst 5 Customer:");
            customers.Take(5).Print();

            Console.WriteLine($"\nNumber of Customers in Sweden {customers.Where(cust => cust.Country == "Sverige").Count()}");
            Console.WriteLine($"\nOldest customer is born {customers.OrderBy(cust => cust.BirthDate).First().BirthDate:d}");
            Console.WriteLine($"Youngest customer is born {customers.OrderBy(cust => cust.BirthDate).Last().BirthDate:d}");

            Console.WriteLine($"\nNumber of customers per country");
            var groups = customers.GroupBy(cust => cust.Country);
            foreach (var item in groups)
            {
                Console.WriteLine($"{item.Key} has {item.Count()} number of customers");
            }

            Console.WriteLine($"\nNumber of customers that ends LastName with 'son': {customers.Where(cust => cust.LastName.EndsWith("son")).Count()}");
        }

        private static void QueryOrdersWithLinq(IEnumerable<ICustomer> customers, IEnumerable<IOrder> orders)
        {
            Console.WriteLine($"\nNr of orders: {orders.Count()}");
            Console.WriteLine($"Total order value: {orders.Sum(order => order.Total):C2}");

            Console.WriteLine("\ntop 5 Order list:");
            orders.OrderByDescending(order => order.Value).Take(5).Print();

            Console.WriteLine("\ntop 5 Orders with customer joined in via Join:");
            var orderCustomer = orders.Join(customers, o => o.CustomerID, c => c.CustomerID, (o, c) => new { o, c });
            orderCustomer.OrderByDescending(oc => oc.o.Value).Take(5).Print();

            Console.WriteLine("\ntop 5 Customers from order value via GroupJoin:");
            var CustomerOrders = customers.GroupJoin(orders, c => c.CustomerID, o => o.CustomerID, (cust, orders) => new { cust, orders });
            foreach (var co in CustomerOrders.OrderByDescending(co => co.orders.Sum(o => o.Value)).Take(5))
            {
                Console.WriteLine($"Cust: {co.cust.CustomerID}, Ordercount: {co.orders.Count()}, OrderValue: {co.orders.Sum(o => o.Value):C2}");
            }
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
