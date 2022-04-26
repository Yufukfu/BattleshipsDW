using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleshipsDW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsDW.Tests
{
    [TestClass()]
    public class GridTests
    {
        readonly Player player = new Player1();
        readonly char[,] emptyGrid = {
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
            };

        static bool CompareGrids(char[,] expected, char[,] actual)
        {
            for (int i = 0; i < expected.GetLength(0); i++)
            {
                for (int j = 0; j < expected.GetLength(1); j++)
                {
                    if (expected[i, j] != actual[i, j]) return false;
                }
            }
            return true;
        }

        [TestMethod()]
        public void PlaceShipHorizontalyTest()
        {
            //arrange
            Ship ship = new Battleship();
            int x = 0;
            int y = 0;
            var alignment = Alignment.Horizontal;
            char[,] expected = {
                {'B', 'B', 'B', 'B', 'B', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
            };

            //act
            player.OceanGrid.PlaceShip(x, y, alignment, ship);

            //assert
            Assert.IsTrue(CompareGrids(expected, player.OceanGrid.Panels));
        }

        [TestMethod()]
        public void PlaceShipVerticalyTest()
        {
            //arrange
            Ship ship = new Battleship();
            int x = 0;
            int y = 0;
            var alignment = Alignment.Vertical;
            char[,] expected = {
                {'B', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'B', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'B', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'B', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'B', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
            };

            //act
            player.OceanGrid.PlaceShip(x, y, alignment, ship);

            //assert
            Assert.IsTrue(CompareGrids(expected, player.OceanGrid.Panels));
        }

        [TestMethod()]
        public void PlaceShipOutOfBandsTest()
        {
            //arrange
            Ship ship = new Battleship();
            int x = 8;
            int y = 8;
            var alignment = Alignment.Horizontal;
            var expected = emptyGrid;
            //act
            player.OceanGrid.PlaceShip(x, y, alignment, ship);
            //assert
            Assert.IsTrue(CompareGrids(expected, player.OceanGrid.Panels));
        }

        [TestMethod()]
        public void PlaceShipsOnEachOtherTest()
        {
            //arrange
            Ship ship1 = new Battleship();
            int x1 = 3;
            int y1 = 2;
            var alignment1 = Alignment.Vertical;
            Ship ship2 = new Battleship();
            int x2 = 4;
            int y2 = 1;
            var alignment2 = Alignment.Horizontal;
            char[,] expected = {
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', 'B', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', 'B', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', 'B', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', 'B', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', 'B', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
            };
            //act
            player.OceanGrid.PlaceShip(x1, y1, alignment1, ship1);
            player.OceanGrid.PlaceShip(x2, y2, alignment2, ship2);
            //assert
            Assert.IsTrue(CompareGrids(expected, player.OceanGrid.Panels));
        }

        [TestMethod()]
        public void PlaceShipsTest()
        {
            //arrange
            var console = new ConsoleTestWrapper();
            console.LinesToRead = new List<string>
            { 
                "V",
                "D3",
                "H",
                "F4"
            };
            var player = new Player1();
            player.ShipsList.Ships.Clear();
            player.ShipsList.Ships.Add(new Battleship());
            player.ShipsList.Ships.Add(new Destroyer());
            char[,] expected = {
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', 'B', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', 'B', '~', 'D', 'D', 'D', 'D', '~'},
                {'~', '~', '~', 'B', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', 'B', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', 'B', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
            };
            //act
            player.PlaceShips(console);
            //assert
            Assert.IsTrue(CompareGrids(expected, player.OceanGrid.Panels));
        }

        [TestMethod()]
        public void PlaceShipsWrongInputTest()
        {
            //arrange
            var console = new ConsoleTestWrapper();
            console.LinesToRead = new List<string>
            {
                "V",
                "23",
                "H",
                "F4",
                "V",
                "D9",
                "V",
                "B4"
            };
            var player = new Player1();
            player.ShipsList.Ships.Clear();
            player.ShipsList.Ships.Add(new Battleship());
            player.ShipsList.Ships.Add(new Destroyer());
            char[,] expected = {
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', 'D', '~', '~', '~', 'B', 'B', 'B', 'B', 'B'},
                {'~', 'D', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', 'D', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', 'D', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
                {'~', '~', '~', '~', '~', '~', '~', '~', '~', '~'},
            };
            //act
            player.PlaceShips(console);
            //assert
            Assert.IsTrue(CompareGrids(expected, player.OceanGrid.Panels));
        }

    }
}
