using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq_Orders
{
    public class Order : IOrder
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }

        public int NrOfArticles { get; set; }
        public decimal Value { get; set; }
        public decimal Freight { get; set; }
        public decimal Total => Value + Freight;
        public decimal VAT => Total * 0.8M;

        public DateTime OrderDate { get; private set; }
        public DateTime? DeliveryDate { get; set; }

        public Guid OrderID { get; init; }

        #region Implement IEquatable
        public bool Equals(IOrder other) => OrderID == other.OrderID;

        //Implement due to legacy reasons
        public override bool Equals(object obj) => Equals(obj as IOrder);
        public override int GetHashCode() => OrderID.GetHashCode();
        #endregion

        public override string ToString() => $"{OrderID}: Value: {Value:C2} OrderDate: {OrderDate:d} DeliverDate: {DeliveryDate:d} Country: {Country}\n";

        #region Class Factory for creating an instance filled with Random data
        public static class Factory
        {
            public static Order CreateWithRandomData()
            {
                var rnd = new Random();
                while (true)
                {
                    try
                    {
                        string[] _firstnames = "Fred John Mary Jane Oliver Marie".Split(' ');
                        var FirstName = _firstnames[rnd.Next(0, _firstnames.Length)];

                        string[] _lastnames = "Johnsson Pearsson Smith Ewans Andersson".Split(' ');
                        var LastName = _lastnames[rnd.Next(0, _lastnames.Length)];

                        string[] _country = "Sverige Norge Finland Lettland Tyskland Spanien".Split(' ');
                        var Country = _country[rnd.Next(0, _country.Length)];

                        var NrOfArticles = rnd.Next(1, 51);
                        var Value = (decimal)(rnd.NextDouble() + 0.001D) * 5000;
                        var Freight = (decimal)(rnd.NextDouble() + 0.001D) * 100;

                        int year = rnd.Next(2020, DateTime.Today.Year);
                        int month = rnd.Next(1, 13);
                        int day = rnd.Next(1, 31);

                        var OrderDate = new DateTime(year, month, day);
                        var DeliveryDate = OrderDate + new TimeSpan(rnd.Next(1, 31), 0, 0, 0);

                        var order = new Order
                        {
                            OrderID = Guid.NewGuid(),
                            FirstName = FirstName,
                            LastName = LastName,
                            Country = Country,
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

        public Order ()
        {
            this.OrderID = Guid.NewGuid();
        }
        public Order (IOrder src)
        {
            FirstName = src.FirstName;
            LastName = src.LastName;
            Country = src.Country;

            NrOfArticles = src.NrOfArticles;
            Value = src.Value;
            Freight = src.Freight;

            OrderDate = src.OrderDate;
            DeliveryDate = src.DeliveryDate;

            OrderID = src.OrderID;
        }
    }
}
