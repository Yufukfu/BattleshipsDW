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
    public class PlayerTests
    {
        readonly Player player = new Player1();
        [TestMethod()]
        public void CheckInputStandardTest()
        {
            //arrange
            string position = "C3";
            //act
            bool valid = Player.CheckInput(position, out int x, out int y);
            //assert
            Assert.IsTrue(x == 3 && y == 2 && valid);
        }
        [TestMethod()]
        public void CheckInputLowerCaseTest()
        {
            //arrange
            string position = "a3";
            //act
            bool valid = Player.CheckInput(position, out int x, out int y);
            //assert
            Assert.IsTrue(!valid && x == 0 && y == 0);
        }
        [TestMethod()]
        public void CheckInputOutOfBandsTest()
        {
            //arrange
            string position = "N2";
            //act
            bool valid = Player.CheckInput(position, out int x, out int y);
            //assert
            Assert.IsTrue(!valid && x == 0 && y == 0);
        }

        [TestMethod()]
        public void AllShipsAliveTest()
        {
            foreach (Ship ship in player.ShipsList.Ships)
            {
                ship.Hits = 0;
            }
            Assert.IsTrue(player.ShipsList.AllShipsAlive());
            player.ShipsList.Ships[0].Hits = 1;
            Assert.IsTrue(player.ShipsList.AllShipsAlive());
            player.ShipsList.Ships[0].Hits = player.ShipsList.Ships[0].Length;
            Assert.IsFalse(player.ShipsList.AllShipsAlive());

        }
        [TestMethod()]
        public void AllSunkTest()
        {
            foreach (Ship ship in player.ShipsList.Ships)
            {
                ship.Hits = ship.Length;
            }
            Assert.IsTrue(player.ShipsList.AllShipsSunk());
            player.ShipsList.Ships[0].Hits = 1;
            Assert.IsFalse(player.ShipsList.AllShipsSunk());
        }

        [TestMethod()]
        public void ReactMIssTest()
        {
            //arrange
            var ship = player.ShipsList.Ships[0];
            player.OceanGrid.PlaceShip(2, 2, Alignment.Horizontal, ship);
            var xy = new XY(0, 0);
            string expected = "Miss";
            //act
            player.React(xy, out string message);
            //assert
            Assert.IsTrue(message.Contains(expected));
        }

        [TestMethod()]
        public void ReactHitTest()
        {
            //arrange
            var ship = player.ShipsList.Ships[0];
            player.OceanGrid.PlaceShip(2, 2, Alignment.Horizontal, ship);
            var xy = new XY(2, 2);
            string expected = "Hit";
            //act
            player.React(xy, out string message);
            //assert
            Assert.IsTrue(message.Contains(expected));
        }

        [TestMethod()]
        public void ReactSunkTest()
        {
            //arrange
            var ship = player.ShipsList.Ships[0];
            player.OceanGrid.PlaceShip(2, 2, Alignment.Horizontal, ship);
            ship.Hits = ship.Length;
            var xy = new XY(2, 2);
            string expected = "Sunk";
            //act
            player.React(xy, out string message);
            //assert
            Assert.IsTrue(message.Contains(expected));
        }

        [TestMethod()]
        public void InterpretMessageMissTest()
        {
            //arrange
            string message = "Miss";
            var xy = new XY(5, 7);
            char expected = '@';
            //act
            player.InterpretMessage(message, xy);
            //assert
            Assert.IsTrue(player.TargetGrid.Panels[xy.X, xy.Y] == expected);
        }

        [TestMethod()]
        public void InterpretMessageHitTest()
        {
            //arrange
            string message = "Hit Destroyer";
            var xy = new XY(5, 7);
            char expected = 'X';
            //act
            player.InterpretMessage(message, xy);
            //assert
            Assert.IsTrue(player.TargetGrid.Panels[xy.X, xy.Y] == expected);
        }
        [TestMethod()]
        public void InterpretMessageSunkTest()
        {
            //arrange
            var sunkShips = player.ShipsList.Ships.Where(z => z.EnemySunk == true).ToList().Count;
            string message = "Sunk Destroyer";
            var xy = new XY(5, 7);
            char expected = 'X';
            //act
            player.InterpretMessage(message, xy);
            var sunkShipsUpdate = player.ShipsList.Ships.Where(z => z.EnemySunk == true).ToList().Count;
            sunkShips++;
            //assert
            Assert.IsTrue(player.TargetGrid.Panels[xy.X, xy.Y] == expected && sunkShips == sunkShipsUpdate);
        }

        
    }
}