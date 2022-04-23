using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsDW
{
    public class Ship
    {
        public string Name { get; set; }
        public List<XY> Coordinates;
        public int Length { get; set; }
        public int Hits { get; set; }
        public bool EnemySunk { get; set; }
        public bool IsSunk()
        {
            return Hits >= Length;
        }
        public bool IsHit(XY xy)
        {
            foreach (XY z in Coordinates)
            {
                if (xy.X == z.X && xy.Y == z.Y) return true;
            }
            return false;
        }
    }
    public class Battleship : Ship
    {
        public Battleship()
        {
            Name = "Battleship";
            Length = 5;
            Hits = Length;
            Coordinates = new List<XY>();
            EnemySunk = false;
        }
    }
    public class Destroyer : Ship
    {
        public Destroyer()
        {
            Name = "Destroyer";
            Length = 4;
            Hits = Length;
            Coordinates = new List<XY>();
            EnemySunk = false;
        }
    }
}
