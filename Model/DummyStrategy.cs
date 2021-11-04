using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Services;
namespace Model
{
    public class DummyStrategy : IPlayerStrategy
    {
        MoveValidationService _moveValidationService;

        int stepCounter;

        public DummyStrategy()
        {
            _moveValidationService = new MoveValidationService();

            stepCounter = 0;
        }

        public void Think(Game game)
        {
            Random r = new Random();

            if (stepCounter % 2 == 0)
            {

                int curIndexX = r.Next(game.Board.Corners.GetLength(0));

                int curIndexY = r.Next(game.Board.Corners.GetLength(0));

                int isHorisontal = r.Next(2);

                if (isHorisontal == 1)
                {

                    game.WallIsHorizontal = true;

                }
                else
                {

                    game.WallIsHorizontal = false;

                }

                game.SelectedCorner = game.Board.Corners[curIndexX, curIndexY];

                game.PlaceTheWall();

            }
            else {

                int index = r.Next(game.Board.GetAvailableMoves(game.ActivePlayer).Count);

                game.SelectedCell = game.Board.GetAvailableMoves(game.ActivePlayer)[index];

                if (game.DoDisplayStep)
                {

                    MovePlayerCommand command = new MovePlayerCommand(game.SelectedCell, game.ActivePlayer);

                    game._stepsHistory.AddLast(command);


                }

                game.ChangeTheCell();

                stepCounter++;


            }
 
        }
    }
}
