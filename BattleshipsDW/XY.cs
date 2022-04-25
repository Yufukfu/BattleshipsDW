using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsDW
{
    public class XY
    {
        public int X;
        public int Y;
        public XY(int x, int y)
        {
            X = x;
            Y = y;
        }

        public string ToPosition()
        {
            var p1 = char.ToUpper((char)(Y + 65));
            var p2 = X.ToString();
            string position = p1 + p2;
            return position;
        }
    }
}
