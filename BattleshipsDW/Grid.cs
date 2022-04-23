using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsDW
{
    public class Grid
    {
        public int Size { get; set; }
        public char[,] Panels;
        public Fleet Ships;

        public Grid()
        {
            Size = 10;
            PopulateBoard();
        }

        public void PopulateBoard()
        {
            // todo - moze zrobic jakas glowna klase zeby te size przenosic
            Panels = new char[Player.gridSize, Player.gridSize];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Panels[i, j] = '~';
                }
            }
        }
        public void PrintBoard()
        {
            Console.WriteLine("  A B C D E F G H I J");

            for (int i = 0; i < Size; i++)
            {
                Console.Write(i);
                for (int j = 0; j < Size; j++)
                {
                    Console.ForegroundColor = Panels[i, j] switch
                    {
                        '~' => ConsoleColor.Blue,
                        'X' => ConsoleColor.Red,
                        '@' => ConsoleColor.DarkBlue,
                        '#' => ConsoleColor.DarkBlue,
                        _ => ConsoleColor.White,
                    };
                    Console.Write(" " + Panels[i, j]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }
        }







    }
}
