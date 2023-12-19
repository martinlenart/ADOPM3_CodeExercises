using System;
namespace ADOPM3_02_18a;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, Factory!\n");

        var fm = new csFactoryMontor();
        var fa = new csFactoryAlarm(fm);
        var sm = new csServiceMan(fm);


        fm.StatusCode = 0;
        fm.CheckStatus();

        fm.StatusCode = 1;
        fm.CheckStatus();

        fm.StatusCode = 2;
        fm.CheckStatus();

        fm.StatusCode = 3;
        fm.CheckStatus();

        Console.WriteLine("\nFactory check complete");
    }
}
/* Exercises 
   1. Write a class ServiceLogger that stores every Factory Alarm which is an error
   2. Before Factory Check complete, the list of of factory alarams should be written out
*/

