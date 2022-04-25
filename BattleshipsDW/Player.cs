using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BattleshipsDW;

public abstract class Player
{
    public string Name;
    public Grid OceanGrid;
    public Grid TargetGrid;
    public Fleet ShipsList;

    public Player()
    {
        Name = "";
        OceanGrid = new Grid();
        TargetGrid = new Grid();
        ShipsList = new Fleet();
    }

    public void Restart()
    {
        OceanGrid = new Grid();
        TargetGrid = new Grid();
        ShipsList = new Fleet();
    }

    public void PrintStatus()// print board and ships left
    {
        Console.WriteLine("Enemy ships left: ");
        foreach (Ship ship in ShipsList.Ships)
        {
            if (ship.EnemySunk)
                Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"{ship.Name} ");
            Console.ForegroundColor = ConsoleColor.White;
        }
        Console.WriteLine();
        Console.WriteLine("Ocean Grid:");
        OceanGrid.PrintBoard();
        Console.WriteLine();
        Console.WriteLine("Target Grid:");
        TargetGrid.PrintBoard();
    }

    public static bool CheckInput(string position, out int x, out int y)
    {
        x = 0;
        y = 0;
        Match result = Regex.Match(position, @"\b[A-J]{1}\d{1}\b");
        if (result.Success)
        {
            x = int.Parse(position[1].ToString());
            y = char.ToUpper(position[0]) - 65;
            return true;
        }
        else return false;
    }
    

    public void React(XY xy, out string message)
    {
        foreach (Ship ship in ShipsList.Ships)
        {
            if (ship.IsHit(xy))
            {
                ship.Hits++;
                OceanGrid.Panels[xy.X, xy.Y] = 'X';
                if (ship.IsSunk())
                {
                    foreach (XY z in ship.Coordinates)
                    {
                        OceanGrid.Panels[z.X, z.Y] = '#';
                    }
                    message = $"{Name}: {ship.Name} Sunk";
                    return;
                }
                OceanGrid.Panels[xy.X, xy.Y] = 'X';
                message = $"{Name}: Hit {ship.Name}";
                return;
            }
        }
        OceanGrid.Panels[xy.X, xy.Y] = '@';
        message = $"{Name}: Miss";
    }

    public void InterpretMessage(string message, XY xy)
    {
        var result = message.Split(' ');
        if (result.Contains("Miss"))
        {
            TargetGrid.Panels[xy.X, xy.Y] = '@';
        }
        else if (result.Contains("Hit"))
        {
            TargetGrid.Panels[xy.X, xy.Y] = 'X';
        }
        else if (result.Contains("Sunk"))
        {
            TargetGrid.Panels[xy.X, xy.Y] = 'X';
            Ship sunk = ShipsList.Ships.Find(z => z.Name == result[1] && !z.EnemySunk);
            sunk.EnemySunk = true;
        }
    }
}


public class Player1 : Player
{
    public Player1()
    {
        Name = "Player1";
        OceanGrid = new Grid();
        TargetGrid = new Grid();
        ShipsList = new Fleet();
    }
    public void PlaceShips(IConsole console)
    {
        string message = "";
        console.Clear();
        console.WriteLine("Place Your Ships!");
        PrintStatus();
        while (!ShipsList.AllShipsAlive())
        {
            foreach (Ship ship in ShipsList.Ships)
            {

                if (ship.Hits == 0)
                {
                    continue;
                }
                console.WriteLine($"Placing {ship.Name}, length of this ship is {ship.Length}.");
                console.WriteLine("Choose ship orientation (h - horizontal, v - vertical):");
                string orientation = console.ReadLine();
                Alignment alignment;
                if (orientation == "h")
                {
                    alignment = Alignment.Horizontal;
                }
                else if (orientation == "v")
                {
                    alignment = Alignment.Vertical;
                }
                else
                {
                    message = "ERROR - Invalid orientation";
                    break;
                }

                console.WriteLine("Input coordinates e.g. B2 or G3 :");
                string position = console.ReadLine();
                if (CheckInput(position, out int x, out int y))
                {
                    bool shipPlaced = OceanGrid.PlaceShip(x, y, alignment, ship);
                    if (shipPlaced)
                    {
                        message = "Ship placed succesfully.";
                        console.Clear();
                        console.WriteLine("Place Your Ships!");
                        PrintStatus();
                        console.WriteLine(message);
                        continue;
                    }
                    else
                    {
                        message = "Cannot place ship in this position.";
                        break;
                    }
                }
                else
                {
                    message = "ERROR - Invalid coordinates";
                    break;
                }
            }
            console.Clear();
            console.WriteLine("Place Your Ships!");
            PrintStatus();
            console.WriteLine(message);
        }
        console.Clear();
        console.WriteLine("All ships placed succesfully. Start firing!");
    }
    public void Fire(out XY xy, IConsole console, out string firing)
    {
        bool stop = false;
        int x = 0;
        int y = 0;
        xy = new XY(x, y);
        firing = "";
        while (!stop)
        {
            console.WriteLine("Input coordinates e.g. B2 or G3 :");
            string position = console.ReadLine();
            if (CheckInput(position, out x, out y))
            {
                if (TargetGrid.Panels[x, y] == '~')
                {
                    firing = $"{Name}: {position}";
                    xy = new XY(x, y);
                    stop = true;
                }
            }
            console.WriteLine("Can't attack this coordinates.");
        }
    }
}
public class Player2 : Player
{
    public Player2()
    {
        Name = "Player2";
        OceanGrid = new Grid();
        TargetGrid = new Grid();
        ShipsList = new Fleet();
    }
    public void PlaceShips()
    {
        while (!ShipsList.AllShipsAlive())
        {
            foreach (Ship ship in ShipsList.Ships)
            {
                if (ship.Hits != 0)
                {
                    Alignment alignment = Alignment.Horizontal;
                    Random random = new();
                    if (random.Next() % 2 == 0) alignment = Alignment.Vertical;

                    // var alignment1 = (random.Next() % 2 == 0) ? Alignment.Horizontal : Alignment.Vertical;

                    int x = random.Next(10);
                    int y = random.Next(10);
                    bool shipPlaced = OceanGrid.PlaceShip(x, y, alignment, ship);
                    if (!shipPlaced) break;
                }
            }
        }
    }
    public void Fire(out XY xy, IConsole console, out string firing)
    {
        xy = new XY(0, 0);
        bool stop = false;
        firing = "";
        while (!stop)
        {
            Random random = new();
            int x = random.Next(10);
            int y = random.Next(10);
            if (TargetGrid.Panels[x, y] == '~')
            {
                xy = new XY(x, y);
                string position = xy.ToPosition();
                stop = true;
                firing = $"{Name}: {position}";
            }
        }
    }
}

