using System;
using Helpers;

namespace _05_Wines_Interfaces
{
    public enum enGrapeType { Reissling, Tempranillo, Chardonay, Shiraz, CabernetSavignoin, Syrah }
    public enum enWineType { Red, White, Rose }
    public enum enCountry { Germany, France, Spain }

    public interface IWine
	{
        public string Name { get; set; }

        public enCountry Country { get; set; }
        public enWineType WineType { get; set; }
        public enGrapeType GrapeType { get; set; }

        public decimal Price { get; set; }

        public string ToString();

        public IWine Seed(csSeedGenerator rnd);
    }
}

