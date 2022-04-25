using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsDW
{
    public class Grid
    {
        public int Size;
        public char[,] Panels;
        public Fleet Ships;

        public Grid()
        {
            Size = 10;
            PopulateBoard();
        }

        private void PopulateBoard()
        {
            Panels = new char[Size, Size];
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

        public bool PlaceShip(int x, int y, Alignment alignment, Ship ship)
        {
            XY position = new(x, y);
            bool PositionValid = true;
            for (int i = 0; i < ship.Length; i++)
            {
                if (x >= Size || y >= Size || Panels[x, y] != '~')
                {
                    PositionValid = false;
                    break;
                }

                if (alignment == Alignment.Horizontal)
                    y++;
                else if (alignment == Alignment.Vertical)
                    x++;
            }
            if (PositionValid)
            {
                x = position.X;
                y = position.Y;
                for (int i = 0; i < ship.Length; i++)
                {
                    Panels[x, y] = ship.Name[0];
                    ship.Coordinates.Add(new XY(x, y));
                    if (alignment == Alignment.Horizontal)
                        y++;
                    else if (alignment == Alignment.Vertical)
                        x++;
                }
                ship.Hits = 0;
            }
            return PositionValid;
        }
    }
}
