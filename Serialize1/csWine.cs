using System;
using Helpers;

namespace _05_Wines_Interfaces
{
    public enum enGrapeType { Reissling, Tempranillo, Chardonay, Shiraz, CabernetSavignoin, Syrah }
    public enum enWineType { Red, White, Rose }
    public enum enCountry { Germany, France, Spain }

    public class csWine
	{
        public string Name { get; set; }

        public enCountry Country { get; set; }
        public enWineType WineType { get; set; }
        public enGrapeType GrapeType { get; set; }

        public decimal Price { get; set; }

        public override string ToString()
            => $"Wine {Name} from {Country} is {WineType} and made from grapes {GrapeType}."
            + $" The price is {Price:N2} Sek. ({this.GetType().Name})";


        public csWine Seed (csSeedGenerator rnd)
        {
            Name = rnd.FromString("Chattaux de bueff, Chattaux de paraply, PutiPuti, NamNam");

            GrapeType = rnd.FromEnum<enGrapeType>();
            WineType = rnd.FromEnum<enWineType>();
            Country = rnd.FromEnum<enCountry>();
            Price = rnd.Next(50, 150);
            return this;
        }

        public csWine(csWine original)
        {
            this.Name = original.Name;
            this.GrapeType = original.GrapeType;
            this.Country = original.Country;
            this.WineType = original.WineType;
            this.Price = original.Price;
        }
        public csWine()
        {

        }
	}
}

