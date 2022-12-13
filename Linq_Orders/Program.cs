using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq_Orders
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IOrder> OrderList = new List<IOrder>();
            for (int i = 0; i < 50_000; i++)
            {
                OrderList.Add(Order.Factory.CreateWithRandomData());
            }

            Console.WriteLine($"OrderCount: {OrderList.Count()}\nOrderValue: {OrderList.Sum(o => o.Total):C2}");

            Console.WriteLine("\nLargest 5 orders:");
            OrderList.OrderByDescending(o => o.Total).Take(5).ToList().ForEach(o => Console.WriteLine(o));
            Console.WriteLine($"\nOrder count with total < 1000: {OrderList.Where(o => o.Total<1000).Count()}");
            Console.WriteLine($"\nOrder count with total < 1000: {OrderList.Count(o => o.Total < 1000)}");

            Console.WriteLine($"Freightsum of orders with total < 1000: {OrderList.Where(o => o.Total < 1000).Sum(o => o.Freight):C2}");

            Console.WriteLine("\nCountries with orders");
            var countries = OrderList.Select(o => o.Country);
            foreach (var item in countries.Distinct())
            {
                Console.WriteLine(item);
            }

            var lateDelCount = OrderList.Count(o =>
            {
                if (o.DeliveryDate == null) return false;

                var days = (o.DeliveryDate - o.OrderDate).Value.Days;
                return days > 15;
            });
            Console.WriteLine($"\nOrder count with delivery time > 15 days: {lateDelCount}");

            var finnishOrders = OrderList.Where(o => o.Country == "Finland");
            Console.WriteLine($"\nOrder count Finland: {finnishOrders.Count()}");
            Console.WriteLine($"Ordersum Finland: {finnishOrders.Sum(o => o.Total):C2}");

            Console.WriteLine("\nOrders and Order value per country");
            var groupedList = OrderList.GroupBy(o => o.Country, order => order);
            foreach (var group in groupedList)
            {
                Console.WriteLine($"Orders {group.Key}:");
                Console.WriteLine($"   Order count: {group.Count()}\n   Order value: {group.Sum(o => o.Total):C2}");
            }

            Console.WriteLine("\n5 largest Orders per country");
            foreach (var group in groupedList)
            {
                Console.WriteLine($"Orders {group.Key}:");
                var largestOrders = group.OrderByDescending(o => o.Total).Take(5);
                //largestOrders.ToList().ForEach(o => Console.WriteLine(o));

                foreach (var item in largestOrders)
                {
                    Console.WriteLine(item);
                }
            }
   
            var avgDays = OrderList.Where(o => o.DeliveryDate.HasValue).Average(o => (o.DeliveryDate.Value - o.OrderDate).Days);
            Console.WriteLine($"\nAverage Delivery time: {avgDays}");

        }
    }
}
//Exercises:
//1. Skriv ut antal ordrar, värdet av alla ordrar (tips Sum), de 5 största ordrarna, antal ordrar < 1000kr, summan av frakt för alla ordrar < 1000kr
//2. Skriv ut en lista på alla länder som det kommit ordrar från. Varje land ska skrivas ut bara en gång (tips Distinct)
//3. Skriv ut antal ordrar där leverans skett mer an 15 dagar efter orderdatum (tips Where)
//4. Antalet ordrar och värdet av alla ordrar i Finland

//5. Utmaning: Använd GroupBy för att lista land, antalet ordrar och värdet av ordrarna per land
//6. Utmaning: Använd GroupBy för att lista de 5 största ordrarna per land
//7. Utmaning: Använd Average för att räkna ut medel leveranstiden för alla ordrar
