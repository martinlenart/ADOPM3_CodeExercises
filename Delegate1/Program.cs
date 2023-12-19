using System;
using System.Collections.Generic;

namespace Delegate1
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Starting point
            Console.WriteLine("Starting point");
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

            //WriteLists(numbers, cities);
            #endregion

            #region Exercises 1-4
            Console.WriteLine("Delegates I Exercises");
            Array.ForEach<int>(numbers, Write<int>);
            Array.ForEach<string>(cities, Write<string>);

            Array.ForEach(numbers, Write);
            Array.ForEach(cities, Write);
            #endregion

            #region Exercises 5-6
            Console.WriteLine("\nDelegates II Exercises");


            var evenNumbers = Array.FindAll(numbers, i => i % 2 == 0);
            Array.ForEach(evenNumbers, i =>
            {
                Console.WriteLine($"{i,-20}");
            });
            #endregion

            #region Exercises 7-8
            Console.WriteLine("\nDelegates III Exercises");

            Console.WriteLine("Over 500");
            var MaxVal = 500;

            var largeNumbers = Array.FindAll(numbers, i => i > MaxVal);
            Array.ForEach(largeNumbers, i =>
            {
                Console.WriteLine($"{i,-20}");
            });

            Console.WriteLine("Over 300");
            MaxVal = 300;

            largeNumbers = Array.FindAll(numbers, i => i > MaxVal);
            Array.ForEach(largeNumbers, i =>
            {
                Console.WriteLine($"{i,-20}");
            });


            int sum = 0;
            Array.ForEach(largeNumbers, i => sum += i);
            Console.WriteLine(sum);


            #endregion
        }

        #region Starting point
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

        #region Exercises 1-4
        static void WriteInt(int i)
        {
            Console.Write($"{i,-8}");
        }

        static void WriteString(string i)
        {
            Console.Write($"{i,-8}");
        }

        static void Write<T>(T i)
        {
            Console.WriteLine($"{i,-20}");
        }

        #endregion

        #region Exercises 5-6
        static bool Even (int i)
        {
            return i % 2 == 0;
        }

        #endregion

        #region Exercises 7-8

        static bool FindLarge(int i) => i > 500;
        #endregion

    }
}
//Exercises

//Delegates I
//1.  Explore Array.ForEach and write a delegate that prints numbers to the console using Array.ForEach()
//2.  Explore Array.ForEach and write a delegate that prints cities to the console using Array.ForEach()
//3.  Use Generics <T> to write one delegate that prints T[] to the console using Array.ForEach()
//4.  Print out both lists using the Method from 3.

//Delegates II
//5.  Explore Array.FindAll() and write a delegate returns an int[] of all even numbers.
//    Print out the new array using the pattern from 4 - 6
//6.  Explore Array.FindAll() and write a delegate returns an string[] of all cities with a name with more than 6 letters.
//    Print out the new array using the pattern from 4 - 6

//Delegates III
//7. Explore Array.Find() and write a delegate that finds the first number in numbers > 500. Print out the number
//8. Explore Array.FindLast() and write a delegate that finds the last city in the cities with a name longer than 8 letters


