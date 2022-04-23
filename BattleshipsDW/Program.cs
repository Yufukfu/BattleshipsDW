using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BattleshipsDW
{
    class Program
    {
        static void Main()
        {

            Console.WindowWidth = 60;
            Console.WindowHeight = 45;
            PrintHelp();
            bool exit = false;
            while (!exit) // main menu
            {
                string command = Console.ReadLine();
                Console.Clear();
                PrintHelp();
                Console.WriteLine("Input command:");
                Console.WriteLine();

                switch (command)
                {
                    case "s":
                        PlayGame();
                        break;
                    case "h":
                        PrintGameHelp();
                        break;
                    case "x":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("ERROR - Invalid Command");
                        Console.WriteLine("Type 'h' for help.");
                        break;
                }

            }
            Console.WriteLine("Good bye!");
        }

        private static void PlayGame() //game
        {
            Game game = new();
            game.StartGame();
        }


        private static void PrintHelp()
        {
            Console.WriteLine(
                "@@ BATTLESHIPS @@\n" +
                "\n" +
                "Main menu \n" +
                "s -- Start the game\n" +
                "h -- Print help\n" +
                "x -- Exit the program\n"
                );
        }
        private static void PrintGameHelp()
        {
            Console.Clear();
            var lines = File.ReadAllLines(@"Help.txt");
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }
    }
}