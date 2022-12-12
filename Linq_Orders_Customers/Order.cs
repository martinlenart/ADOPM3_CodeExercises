using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq_Orders_Customers
{
    public class Order : IOrder
    {
        public Guid OrderID { get; init; }
        public Guid CustomerID { get; init; }

        public int NrOfArticles { get; set; }
        public decimal Value { get; set; }
        public decimal Freight { get; set; }
        public decimal Total => Value + Freight;
        public decimal VAT => Total * 0.8M;

        public DateTime OrderDate { get; init; }
        public DateTime? DeliveryDate { get; set; }

        #region Implement IEquatable
        public bool Equals(IOrder other) => OrderID == other.OrderID;

        //Implement due to legacy reasons
        public override bool Equals(object obj) => Equals(obj as IOrder);
        public override int GetHashCode() => OrderID.GetHashCode();
        #endregion
 
        #region Class Factory for creating an instance filled with Random data
        public static class Factory
        {
            public static Order CreateWithRandomData(Guid CustomerID)
            {
                var rnd = new Random();
                while (true)
                {
                    try
                    {
                        var NrOfArticles = rnd.Next(1, 51);
                        var Value = (decimal)(rnd.NextDouble() + 0.001D) * 5000;
                        var Freight = (decimal)(rnd.NextDouble() + 0.001D) * 100;

                        int year = rnd.Next(2020, DateTime.Today.Year);
                        int month = rnd.Next(1, 13);
                        int day = rnd.Next(1, 31);

                        var OrderDate = new DateTime(year, month, day);
                        var DeliveryDate = OrderDate + new TimeSpan(rnd.Next(1, 31), 0, 0, 0);

                        var order = new Order (CustomerID)
                        {
                            OrderID = Guid.NewGuid(),
                            NrOfArticles = NrOfArticles,
                            Value = Value,
                            Freight = Freight,
                            OrderDate = OrderDate,
                            DeliveryDate = DeliveryDate
                        };

                        return order;
                    }
                    catch { }
                }
            }
        }
        #endregion

        public override string ToString() => $"{OrderID}: Value: {Value:C2} OrderDate: {OrderDate:d} DeliverDate: {DeliveryDate:d} CustomerID: {CustomerID}";

        //For the serialization only
        public Order() {}
        public Order (Guid CustomerID)
        {
            this.OrderID = Guid.NewGuid();
            this.CustomerID = CustomerID;
        }

        #region copy constructor
        public Order (IOrder src)
        {
            NrOfArticles = src.NrOfArticles;
            Value = src.Value;
            Freight = src.Freight;

            OrderDate = src.OrderDate;
            DeliveryDate = src.DeliveryDate;

            OrderID = src.OrderID;
            CustomerID = src.CustomerID;
        }
        #endregion
    }
}
