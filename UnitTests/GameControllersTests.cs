using API.Controllers;
using API.Models;
using NUnit.Framework;

namespace UnitTests
{
    public class GameControllersTests
    {
    
        [Test]
        public void CreateDoors_CreatesThreeDoors()
        {
            // arrange
            var gameController = new GameController();

            // act
            var listOfDoors = gameController.CreateDoors();

            int expectedLength = 3;

            // assert
            Assert.That(listOfDoors.Count(), Is.EqualTo(expectedLength));
        }

        [Test]
        public void CreateDoors_CreatesThreeDoors_OnlyOneWinner()
        {
            // arrange
            var gameController = new GameController();

            // act
            var listOfDoors = gameController.CreateDoors();

            var winnerDoors = listOfDoors.Where(door => door.winner);

            var looserDoors = listOfDoors.Where(door => !door.winner);

            // assert
            Assert.That(winnerDoors.Count(), Is.EqualTo(1));
            Assert.That(looserDoors.Count(), Is.EqualTo(2));
        }


        [Test]
        public void getGameRound_Works()
        {
            // arrange
            var gameController = new GameController();

            // act
            var listOfDoors = gameController.CreateDoors();
            int gameResult = gameController.getGameRound(listOfDoors,true);

            // assert
            Assert.That(gameResult, Is.AnyOf(0,1));
       
        }

        [Test]
        public void defineDoors_Works_NeverOpensWinnerDoor()
        {
            // arrange
            var gameController = new GameController();

            // act
            var listOfDoors = gameController.CreateDoors();
            var doors = gameController.defineDoors(listOfDoors);

            // assert
            Assert.That(doors.DoorToOpen.winner, Is.EqualTo(false));
        }

        [Test]
        public void Start_Works_ReturnsResults()
        {
            // arrange
            var gameController = new GameController();
            var gameReq = new GameReq
            {
                Tries = 2,
                Change = true
            };

            // act
            var result = gameController.Start(gameReq).Value;

            // assert
            Assert.That(result.Result, Is.AnyOf(0,50,100));
        }
    }
}