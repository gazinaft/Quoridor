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

        public DummyStrategy()
        {
            _moveValidationService = new MoveValidationService();
        }

        public void Think(Game game)
        {
            Random r = new Random();
            
            int index = r.Next(game.Board.GetAvailableMoves(game.ActivePlayer).Count);

            game.SelectedCell = game.Board.GetAvailableMoves(game.ActivePlayer)[index];
            
            game.ChangeTheCell();

        }
    }
}
