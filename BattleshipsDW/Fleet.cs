using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsDW
{
    public class Fleet
    {
        public List<Ship> Ships;
        public Fleet()
        {
            Ships = new List<Ship>
            {
                new Battleship(),
                new Destroyer(),
                new Destroyer()
            };

        }
        public bool AllShipsAlive()
        {
            // return Ships.All(ship => !ship.IsSunk());
            // var xdd = Ships.Where(z => z.Hits > 0).Select(y => y.Name);

            foreach (Ship ship in Ships)
            {
                if (ship.IsSunk() == true) return false;
            }
            return true;
        }

        public bool AllShipsSunk()
        {
            foreach (Ship ship in Ships)
            {
                if (ship.IsSunk() == false) return false;
            }
            return true;
        }
    }
}
