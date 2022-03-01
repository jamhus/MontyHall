using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        [HttpPost]
        public ActionResult<GameRes> Start(GameReq gameReq)
        {   // all the wins will be added here 
            int wins = 0;

            for (int i = 0; i < gameReq.Tries; i++)
            {
                var doors = CreateDoors();

                var roundResult = getGameRound(doors,gameReq.Change);
                wins += roundResult;

            }

            // divide the aount of wins by the amount of tries and multiply by 100 to get a full percentage number 
            decimal result = (decimal) wins / gameReq.Tries * 100;

            // round large decimal numbers to one decimal only
            result = decimal.Round(result, 1, MidpointRounding.AwayFromZero);

            var res = new GameRes()
            {
                Result = result,
                Tries = gameReq.Tries,

            };

            return res;
        }

        private int getGameRound(List<Door> doors, bool change)
        {
            //randomly get a door 
            Random randomer = new Random();
            int choosenDoorId = randomer.Next(1, 3);

            // define all the doors 
            // the door with id picked up by the code 
            var currentDoor = doors.First(door=> door.Id == choosenDoorId);

            // the door that gets opened , not winner and not current 
            var doorToOpen = doors.First(door => door.Id != currentDoor.Id && !door.winner);

            // the door that stays closed with the currentDoor
            var reminingDoor = doors.First(door => door.Id != currentDoor.Id && door.Id != choosenDoorId);
            
            // return the remining door if the user wished to switch , otherwise the current door if 
            // return 1 if the user wins and 0 if loses
            if (change)
            {
               return reminingDoor.winner ? 1 : 0;
            }

            else
            {
                return currentDoor.winner ? 1 : 0;
            }
        }

        private List<Door> CreateDoors()
        {
            var doors = new List<Door>();

            //randomly get a winner door 
            Random randomer = new Random();
            int winner = randomer.Next(1,3);

            for (int i = 1; i <= 3; i++)
            {
                var door = new Door()
                {
                    Id = i,
                    winner = i == winner ? true : false,
                };
                doors.Add(door);    
            }
            return doors;   
        }
    }
}
