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

            Console.WriteLine($"OrderCount: {OrderList.Count()}");
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
