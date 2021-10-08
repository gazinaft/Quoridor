using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;
using Model;
using MappingProj;
namespace Controllers
{
    public class Presenter
    {
        private IGameView View;

        private Game Game;

        private GameFieldMapper _gameFieldMapper;

        public Presenter(IGameView view, Game game)
        {
            View = view;

            Game = game;

            _gameFieldMapper = new GameFieldMapper();

            View.DisplayTheField(_gameFieldMapper.FromModelToView(Game.Board));

            View.PlacingTheWall += PlaceTheWall;

            View.PlayerMove += MakeStep;
        }

        public void MakeStep() {

            throw new NotImplementedException();

        }

        public void PlaceTheWall() {

            throw new NotImplementedException();

        }


    }
}
