using System;

namespace Linq_Orders
{
    public interface IOrder : IEquatable<IOrder>
    {
        public Guid OrderID { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }

        public int NrOfArticles { get; set; }
        public decimal Value { get; set; }
        public decimal Freight { get; set; }
        public decimal Total { get; }
        public decimal VAT { get; }

        public DateTime OrderDate { get; }
        public DateTime? DeliveryDate { get; set; }
    }
}
