using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq_Orders_Customers
{
    public class Customer : ICustomer
    {
        public Guid CustomerID { get; init; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Adress { get; set; }
        public int ZipCode { get; set; }
        public string Country { get; set; }
        public DateTime BirthDate { get; set; }

        #region Implement IEquatable
        public bool Equals(ICustomer other) => CustomerID == other.CustomerID;

        //Implement due to legacy reasons
        public override bool Equals(object obj) => Equals(obj as ICustomer);
        public override int GetHashCode() => CustomerID.GetHashCode();
        #endregion

        public override string ToString() => $"{CustomerID}: {FirstName} {LastName}, {Adress}, {ZipCode}, {Country}";

        #region Class Factory for creating an instance filled with Random data
        public static class Factory
        {
            public static Customer CreateWithRandomData()
            {
                var rnd = new Random();
                while (true)
                {
                    try
                    {
                        string[] _firstnames = "Fred John Mary Jane Oliver Marie Per Thomas Ann Susanne".Split(' ');
                        var FirstName = _firstnames[rnd.Next(0, _firstnames.Length)];

                        string[] _lastnames = "Johnsson Pearsson Smith Ewans Andersson Svensson Shultz Perez".Split(' ');
                        var LastName = _lastnames[rnd.Next(0, _lastnames.Length)];

                        string[] _adress = "Backvagen, Ringvagen, Box, Smith street, Graaf strasse, Vasagatan, Odenplan, Birger Jarlsgatan".Split(',');
                        var Adress = _adress[rnd.Next(0, _adress.Length)].Trim() + " " + rnd.Next(1, 100);

                        var ZipCode = rnd.Next(10000, 99999);

                        string[] _country = "Sverige Norge Finland Lettland Tyskland Spanien".Split(' ');
                        var Country = _country[rnd.Next(0, _country.Length)];


                        int year = rnd.Next(1940, DateTime.Today.Year - 20);
                        int month = rnd.Next(1, 13);
                        int day = rnd.Next(1, 31);

                        var BirthDate = new DateTime(year, month, day);
                        var customer = new Customer
                        {
                            CustomerID = Guid.NewGuid(),
                            FirstName = FirstName,
                            LastName = LastName,
                            ZipCode = ZipCode,
                            Country = Country,
                            BirthDate = BirthDate
                        };

                        return customer;
                    }
                    catch { }
                }
            }
        }
        #endregion

        public Customer()
        {
            this.CustomerID = Guid.NewGuid();
        }
        public Customer(ICustomer src)
        {
            FirstName = src.FirstName;
            LastName = src.LastName;
            Adress = src.Adress;
            ZipCode = src.ZipCode;
            Country = src.Country;

            BirthDate = src.BirthDate;
        }
    }
}
