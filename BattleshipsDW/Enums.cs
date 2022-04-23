using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsDW
{
    public enum GameState
    {
        Start,
        PlacingShips,
        Firing,
        End
    }

    public enum TurnState
    {
        Player1,
        Player2
    }

    public enum Alignment
    {
        Vertical,
        Horizontal
    }

}