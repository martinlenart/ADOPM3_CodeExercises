using System;

namespace ExtensionMethods
{
    public static class IntExt
    {
        public static int Fac(this int number)
        {
            var answer = 1;
            for (int i = number; i > 0; i--)
            {
                answer *= i;
            }
            return answer;
        }
    }
 
    class Program
    {
        
        static void Main(string[] args)
        {

            int i = 5;

            Console.WriteLine(i.Fac());

            Console.WriteLine(25.Fac());

            Console.WriteLine(Fac2(25));
        }

        public static int Fac2(int number)
        {
            var answer = 1;
            for (int i = number; i > 0; i--)
            {
                answer *= i;
            }
            return answer;
        }

    }
}

