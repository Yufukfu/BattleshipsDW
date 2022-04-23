using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsDW
{
    public class Game
    {

        public GameState State { get; set; }
        public TurnState Turn { get; set; }

        public Player1 player1;
        public Player2 player2;

        public Game()
        {
            State = GameState.End;
            Turn = TurnState.Player1;
            Random x = new();
            if (x.Next() % 2 == 0) Turn = TurnState.Player2;
            player1 = new Player1();
            player2 = new Player2();
        }
        public void StartGame()
        {
            State = GameState.Start;
            player1.Restart();
            player2.Restart();
            player1.PlaceShips();
            player2.PlaceShips();
            player1.PrintStatus();
            string winner = "";
            while (!player1.AllShipsSunk() && !player2.AllShipsSunk())
            {
                winner = PlayTurn();
            }
            Console.WriteLine();
            Console.WriteLine($"{winner} won the game!");
            Console.WriteLine();
            Console.WriteLine("Press ENTER to continue");
            Console.ReadLine();
        }

        public string PlayTurn()
        {
            XY xy;
            var turnSummary = "";
            for (int i = 0; i < 2; i++)
            {
                switch (Turn)
                {
                    case TurnState.Player1:
                        player1.Fire(out xy, out string firing1);
                        player2.React(xy, out string message1);
                        player1.InterpretMessage(message1, xy);
                        turnSummary = $"{turnSummary}\n{firing1}\n{message1}\n";
                        if (player2.AllShipsSunk()) return player1.Name;
                        Turn = TurnState.Player2;
                        break;
                    case TurnState.Player2:
                        player2.Fire(out xy, out string firing2);
                        player1.React(xy, out string message2);
                        player2.InterpretMessage(message2, xy);
                        turnSummary = $"{turnSummary}\n{firing2}\n{message2}\n";
                        if (player1.AllShipsSunk()) return player2.Name;
                        Turn = TurnState.Player1;
                        break;
                }
            }
            Console.Clear();
            Console.WriteLine("Fire!");
            player1.PrintStatus();
            Console.WriteLine(turnSummary);

            return "";
        }
    }
}
