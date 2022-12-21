using System;
using System.Collections.Generic;
using System.Drawing;

namespace Lambda0
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Initialization
            int[] numbers = new int[20];
            string[] cities = new string[20];

            //Random Initialization
            var rnd = new Random();
            var names = "Stockholm, Copenhagen, Oslo, Helsinki, Berlin, Madrid, Lissabon".Split(',');
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = rnd.Next(100, 1000 + 1);
                cities[i] = names[rnd.Next(0, names.Length)].Trim();
            }

            WriteLists(numbers, cities);
            #endregion

            #region Exercise 2
            Console.WriteLine("Delegates I");
            Console.WriteLine($"\n{nameof(numbers)} output by delegate");
            Array.ForEach(numbers, myInt=> Console.WriteLine(myInt));


            Console.WriteLine($"\n{nameof(cities)} output by delegate");
            Array.ForEach(cities, WriteString);

            Console.WriteLine($"\n{nameof(numbers)} output by generic delegate");
            Array.ForEach(numbers, WriteItem<int>);
            Console.WriteLine($"\n{nameof(cities)} output by generic delegate");
            Array.ForEach(cities, WriteItem<string>);

            Console.WriteLine("\nDelegates II");
            var evenlist = Array.FindAll(numbers, IsEven);
            Array.ForEach(evenlist, WriteItem);

            Console.WriteLine();
            var temp = Array.FindAll(cities, IsLongName);
            Array.ForEach(temp, WriteItem);

            Console.WriteLine("\nDelegates III");
            Console.WriteLine(Array.Find(numbers, IsLargeNumber));
            Console.WriteLine(Array.FindLast(cities, IsLongestName));
            #endregion
        }

        #region Initialization
        static void WriteLists(int[] _numbers, string[] _cities)
        {

            Console.WriteLine($"{nameof(_numbers)}:");
            foreach (var item in _numbers)
                Console.WriteLine(item);

            Console.WriteLine($"\n{nameof(_cities)}:");
            foreach (var item in _cities)
                Console.WriteLine(item);

        }
        #endregion

        #region Delegates declarations
        static void WriteInts(int myInt)
        {
            Console.WriteLine(myInt);
        }
        static void WriteString(string myString)
        {
            Console.WriteLine(myString);
        }
        static void WriteItem<T>(T item)
        {
            Console.WriteLine(item);
        }
        public static bool IsEven(int item) => item % 2 == 0;
        public static bool IsLongName(string item) => item.Length > 6;

        static bool IsLargeNumber(int item) => item > 500;
        static bool IsLongestName(string item) => item.Length > 8;
        #endregion

    }
}

//Exercises Delegates => Lamda Expressions
//1.  Go through the code above and try to understand the usage of delegates
//2.  Redo Exercises from in region Exercise 2 using Lambda Expressions in all Array.ForEach(), Array.FindAll(),
//    Array.Find(), Array.FindLast()
//3.  Use Array.ForEach() and Lambda (with a captured variable sum) to calculate the sum of all the
//    elements in the array numbers
//4.  Use Array.ForEach() and Lambda (with a captured variable) to find the largest element in the array numbers
