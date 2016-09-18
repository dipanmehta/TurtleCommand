using System;
using System.Configuration;
using System.IO;

namespace TurtleCommand
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool nextStep;
            var commandInterpreter = new CommandInterpreter(new CommandHandler());
            Console.WriteLine("Move Turtle");
          
            do
            {
                Console.WriteLine("SELECT INPUT SOURCE");
                Console.WriteLine("1. INSERT 1 FOR STANDARD INPUT");
                Console.WriteLine("2. INSERT 2 FOR FILE INPUT");
                Console.WriteLine("3. ENTER WITHOUT COMMAND TO EXIT");
                Console.Write("SELECT INPUT TYPE>:");
                var result = Console.ReadLine();
                nextStep = (string.IsNullOrEmpty(result));
                if (nextStep) continue;
                try
                {
                    var selection = int.Parse(result);
                    switch(selection)
                    {
                        case 1:
                            DisplayStandardMenu(commandInterpreter);
                            break;
                        case 2:
                            DisplayFileMenu(commandInterpreter);
                            break;
                        
                    }
                }
                catch (Exception )
                {
                    Console.WriteLine("Please Insert Proper Argument");
                }

            } while (!nextStep);
           
            
        }

        public static void DisplayStandardMenu(ICommandInterpreter commandInterpreter)
        {
            var nextStep = true;
            Console.WriteLine("Insert Command or Press Enter to Exit");
            Console.WriteLine("1. PLACE X,Y,NORTH");
            Console.WriteLine("2. MOVE");
            Console.WriteLine("3. LEFT");
            Console.WriteLine("4. RIGHT");
            Console.WriteLine("5. REPORT");
            Console.WriteLine("6. Enter Without Command to Move to Main Menu");
            do
            {
                try
                {
                    Console.Write("COMMAND>:");
                    var result = Console.ReadLine();
                    nextStep = (string.IsNullOrEmpty(result));
                    if (nextStep) continue;
                    commandInterpreter.Execute(result);
                }
                catch (Exception)
                {
                    Console.WriteLine("Please Provide valid arguments!");
                }

            } while (!nextStep);
        }

        public static void DisplayFileMenu(ICommandInterpreter commandInterpreter)
        {
            try
            {
                var filePath = ConfigurationManager.AppSettings.Get("FilePath");
                using (var reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Console.WriteLine("Command>:"+ line);
                        commandInterpreter.Execute(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
