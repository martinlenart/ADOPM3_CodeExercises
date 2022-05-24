using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Serialization
{
    static class LinqExtensions
    {
        public static void Print<T>(this IEnumerable<T> collection)
        {
            collection.ToList().ForEach(item => Console.WriteLine(item));
        }
    }
    public class CustomerOrders
    {
        public Customer cus { get; set; }
        public List<Order> orders { get; set; }
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
                for (int o = 0; o < rnd.Next(0, MaxNrOfOrdersPerCustomer+1); o++)
                {
                    OrderList.Add(Order.Factory.CreateWithRandomData(cus.CustomerID));
                }
            }


            //All customers in Lettand
            var cusLettand = CustomerList.Where(c => c.Country == "Lettland").ToList();
            //cusLettand.Take(10).Print();


            var xs = new XmlSerializer(typeof(List<Customer>));

            using (Stream s = File.Create(fname("LettlandKunder.xml")))
                xs.Serialize(s, cusLettand);

            Console.WriteLine(cusLettand.Count());
            Console.WriteLine(fname("LettlandKunder.xml"));


            List<Customer> anotherList = new List<Customer>();
            using (Stream s = File.OpenRead(fname("LettlandKunder.xml")))
                anotherList = (List<Customer>)xs.Deserialize(s);


            Console.WriteLine(anotherList.Count());


            Console.WriteLine("\ntop 10 Customers from order value via GroupJoin:");
            var CustomerOrders = CustomerList.GroupJoin(OrderList, c => c.CustomerID, o => o.CustomerID,
                (cust, orders) => new CustomerOrders {  cus = cust, orders = orders.ToList()});



            var topOrders = CustomerOrders.OrderByDescending(co => co.orders.Sum(o => o.Value)).Take(10).ToList();
            foreach (var co in topOrders)
            {
                Console.WriteLine($"Cust: {co.cus.CustomerID}, Ordercount: {co.orders.Count()}, OrderValue: {co.orders.Sum(o => o.Value):C2}");
            }


            var xs1 = new XmlSerializer(typeof(List<CustomerOrders>));

            using (Stream s = File.Create(fname("TopOrders.xml")))
                xs1.Serialize(s, topOrders);


        }


        static string fname(string name)
        {
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            documentPath = Path.Combine(documentPath, "Nisse");
            if (!Directory.Exists(documentPath)) Directory.CreateDirectory(documentPath);
            return Path.Combine(documentPath, name);
        }
    }
}
///Exercises:
//1.    Serialisera alla kunder i Lettland till en xml fil.
//2.    Öppna filen i Excel och Word och undersök innehållet.
//3.    Serialisera kund och orderdata för de 10 största kunderna till en xml fil. Öppna i Excel
//4.    Serialisera alla kunder i Sverige till en Json fil. Öppna file och titta på innehållet
