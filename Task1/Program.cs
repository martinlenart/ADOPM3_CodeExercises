namespace Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string Message = "Martins hello";
                var t1 = Task.Run(() =>
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Console.WriteLine($"{Message}{i} from Task1");
                        Task.Delay(200);
                        if (i == 5)
                        {
                            //throw new Exception("Task1 has faulted");
                        }
                    }

                    return "Task1 has completed";
                });
                Console.WriteLine(t1.Result);

                var t2 = Task.Run(() =>
                {
                    for (int i = 0; i < 10; i++)
                    {

                        Console.WriteLine($"{Message}{i} from Task2");
                        Task.Delay(1000);
                        if (i == 5)
                        {
                            //throw new Exception("Task2 has faulted");
                        }

                    }
                    return "Task2 has completed";
                });
                Console.WriteLine(t2.Result);

                var t3 = Task.Run(() =>
                {
                    for (int i = 0; i < 15; i++)
                    {
                        Console.WriteLine($"{Message}{i} from Task3");
                        Task.Delay(500);
                        if (i == 5)
                        {
                            //throw new Exception("Task3 has faulted");
                        }
                    }
                    return "Task3 completed";
                });
                Console.WriteLine(t3.Result);
            }
            catch (Exception ex)
            {
                //Your code
            }
            finally
            {
                Console.WriteLine("Main terminated");
                Console.ReadLine();
            }
        }
    }
}

//Exercise:
//1. Write code in the exception handler to check what task faulted and write an error message containing the message from the faulted task
//2. Experiement by letting one or several of the tasks simulate an error by throwing an Exception. See that you get an error message
//3. What happens if two or more tasks fault?