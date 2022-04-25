using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsDW
{
    public class Game
    {
        private TurnState turn;
        private IConsole console;
        private Player1 player1;
        private Player2 player2;

        public Game()
        {
            turn = TurnState.Player1;
            Random x = new();
            if (x.Next() % 2 == 0) turn = TurnState.Player2;
            player1 = new Player1();
            player2 = new Player2();
            console = new ConsoleWrapper();
        }

        public void StartGame()
        {
            player1.Restart();
            player2.Restart();
            player1.PlaceShips(console);
            player2.PlaceShips();
            player1.PrintStatus();
            string winner = "";
            while (!player1.ShipsList.AllShipsSunk() && !player2.ShipsList.AllShipsSunk())
            {
                winner = PlayTurn();
            }
            console.WriteLine("");
            console.WriteLine($"{winner} won the game!");
            console.WriteLine("");
            console.WriteLine("Press ENTER to continue");
            console.ReadLine();
        }

        private string PlayTurn()
        {
            dynamic first = player1;
            dynamic second = player2;
            var turnSummary = "";
            for (int i = 0; i < 2; i++)
            {
                switch (turn)
                {
                    case TurnState.Player1:
                        first = player1;
                        second = player2;
                        turn = TurnState.Player2;
                        break;
                    case TurnState.Player2:
                        first = player2;
                        second = player1;
                        turn = TurnState.Player1;
                        break;
                }
                first.Fire(out XY xy, console, out string firing2);
                second.React(xy, out string message2);
                first.InterpretMessage(message2, xy);
                turnSummary = $"{turnSummary}\n{firing2}\n{message2}\n";
                if (second.AllShipsSunk()) return first.Name;
            }
            console.Clear();
            console.WriteLine("Fire!");
            player1.PrintStatus();
            console.WriteLine(turnSummary);
            return "";
        }
    }
}
