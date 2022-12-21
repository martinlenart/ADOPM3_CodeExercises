namespace Event2
{
    internal class Program
    {
        class EventList<T>: List<T>
        {
            public EventHandler<T> ItemAdded;
            public EventHandler<T> ItemRemoved;
        }

        static void Main(string[] args)
        {
            EventList<int> numbers = new EventList<int>();
            EventList<string> cities = new EventList<string>();

            //Random filling of Lists
            const int listSize = 10;

            var rnd = new Random();
            var capitals = "Stockholm, Copenhagen, Oslo, Helsinki, Berlin, Madrid, Lissabon".Split(", ");
            for (int i = 0; i < listSize; i++)
            {
                numbers.Add (rnd.Next(100, 1000 + 1));
                cities.Add(capitals[rnd.Next(0, capitals.Length)]);
            }

            //Remove items
            var n = numbers[numbers.Count - 1]; //just to ensure a number is in the list
            numbers.Remove(n);                  //remove the number
            var c = cities[cities.Count - 1];  //just to ensure a city is in the list
            cities.Remove(c);                  //remove the city


            //Write out the lists using delegate
            Console.WriteLine("\nnumbers:");
            numbers.ForEach(WriteItem);
            Console.WriteLine("\n\ncities:");
            cities.ForEach(WriteItem);
            Console.WriteLine("\n\n");
        }

        //Generic method to write an item, used as delegate
        static void WriteItem<T>(T item)
        {
            Console.Write($"{item}, ");
        }

        //Eventhandlers
        static public void ItemAddedHandler<T> (object? sender, T item)
        {
            Console.WriteLine($"{item} added");
        }
        static public void ItemRemovedHandler<T>(object? sender, T item)
        {
            Console.WriteLine($"{item} removed");
        }

    }
}

//Exercises:
//1. Modify EventList<T> so an event is fired whenever an items is added, Add(), or removed, Remove(), to the list
//2. Assign the Eventhandlers, ItemAddedHandler and ItemRemovedHandler the the list and run the code to see the result