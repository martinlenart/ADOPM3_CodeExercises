// See https://aka.ms/new-console-template for more information

using System;

namespace Task3
{ 
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string s1 = null, s2 = null, s3 = null;
            Task<string> t1 = null, t2 = null, t3 = null;
            try
            {
                Console.WriteLine("Syncron calls");
                s1 = SayHello("Good Morning", 10, 1000, false);
                s2 = SayHello("Good Afternoon", 5, 2000, false);
                s3 = SayHello("Good Evening", 15, 500, false);

                Console.WriteLine(s1);
                Console.WriteLine(s2);
                Console.WriteLine(s3);

                Console.WriteLine("\n\nAsyncron calls");
                //Ex3 - make the calls to SayHelloAsync
                var r1 = await SayHelloAsync("Good Morning", 10, 1000, false); 
                var r2 = await SayHelloAsync("Good Afternoon", 5, 2000, false);
                var r3 = await SayHelloAsync("Good Evening", 15, 500, false);

                Console.WriteLine(r1);
                Console.WriteLine(r2);
                Console.WriteLine(r3);
            }
            catch (Exception ex)
            {
                //Your code
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Main terminated");
                Console.ReadLine();
            }
        }

        //Ex2 - complete below async declaration of SayHello 
        //static public Task<string> SayHelloAsync(string message, int iterations, int msDelay, bool causeError = false) => ....

        static public Task<string> SayHelloAsync(string message, int iterations, int msDelay, bool causeError = false) =>
            Task.Run(() => SayHello(message, iterations, msDelay, causeError));

        static public string SayHello(string message, int iterations, int msDelay, bool causeError = false)
        {
            var rnd = new Random();
            int errorIteration = rnd.Next(0, iterations);
            for (int i = 0; i< iterations; i++)
            {
                Console.WriteLine($"{i,4}:{message}");
                Task.Delay(msDelay);
                
                if (causeError && (i == errorIteration))
                {
                    throw new Exception($"Error saying: {message}");
                }
            }
            return $"All good saying: {message}";
        }
    }
}

//Exercise:
//1. Modify above code to follow the async/await pattern
//2. Experiment by setting causeError = true for the various async calls
//   - understand the error handling







