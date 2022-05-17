using System;

namespace Linq_Orders_Customers
{
    public interface IOrder : IEquatable<IOrder>
    {
        public Guid OrderID { get; }
        public Guid CustomerID { get; }

        public int NrOfArticles { get; set; }
        public decimal Value { get; set; }
        public decimal Freight { get; set; }
        public decimal Total { get; }
        public decimal VAT { get; }

        public DateTime OrderDate { get; }
        public DateTime? DeliveryDate { get; set; }
    }
}
