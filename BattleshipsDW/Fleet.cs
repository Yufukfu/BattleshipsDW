using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsDW
{
    public class Fleet
    {
        public List<Ship> Ships { get; set; }
        public Fleet()
        {
            Ships = new List<Ship>
            {
                new Battleship(),
                new Destroyer(),
                new Destroyer()
            };

        }
        
    }
}
