using System;

namespace ErrorHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ProcessUserInput();
            }
            catch (Exception ex)
            {
                AppLog.Instance.LogException(ex);
                Console.WriteLine("Pls contact our service center, you have a virus in your computer");
            }
            finally
            {
                Console.WriteLine(AppLog.Instance.WriteToDisk());
            }
        }
        private static void ProcessUserInput()
        {
            bool quit = false;
            Console.WriteLine("Don't Press Button 3, 4, 5 or 7!");
            do
            {
                Console.WriteLine("Which button do you want to press?");
                string sButtonToPress = Console.ReadLine();

                if (sButtonToPress == "q")
                {
                    quit = true;
                }
                else
                {
                    int buttonToPress;
                    if (int.TryParse(sButtonToPress, out buttonToPress))
                    {
                        try
                        {
                            PressTheButton(buttonToPress);
                            Console.WriteLine("Indeed the button was pressed successfully");
                        }
                        catch (ExplosionException ex)
                        {
                            AppLog.Instance.LogException(ex);
                            Console.WriteLine($"{ex.Message}");
                            if (ex.Severity == ErrorSeverity.fatal)
                            {
                                Console.WriteLine($"Sorry, I cannot manage this error");
                                throw;
                            }
                            Console.WriteLine($"I could manage this error, continue to play");
                        }

                        catch (InsufficientMemoryException ex)
                        {
                            AppLog.Instance.LogException(ex);
                            Console.WriteLine($"{ex.Message} - Why cant you listen!!");
                            throw;
                        }
                        catch (Exception ex)
                        {
                            AppLog.Instance.LogException(ex);
                            Console.WriteLine($"{ex.Message} - But it is alright my friend!");
                        }
                        finally
                        {
                            Console.WriteLine("Code here will always be executed!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong input, try again");
                    }
                }
            } while (!quit);
        }
        static void PressTheButton(int buttonNr)
        {
            if (buttonNr == 3)
                throw new ExplosionException("Managable KaBoom", buttonNr, ErrorSeverity.managable);

            if (buttonNr == 4)
                throw new ExplosionException("Fatal Crash", buttonNr, ErrorSeverity.fatal);
            
            if (buttonNr == 5)
                throw new Exception("KaBoom!!");

            if (buttonNr == 7)
                throw new InsufficientMemoryException("You hopless guy!");

            Console.WriteLine($"You pressed button {buttonNr}");
        }

    }
}

//Exercise:
//1. Change the messages thrown in PressTheButton() to your own ExceptionMessage, ExplosionException inheriting from Exception.
//   Create a property in ExplosionException that of the button pressed and severity of the error (managable and fatal)
//2. Modify the code in ProcessUserInput() to catch ExplosionException and depending on severity gives an user message(manageable) or rethrow (fatal)
