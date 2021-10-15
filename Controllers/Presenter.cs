using System;
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

            View.DisplayPotentialWallsAndCorners(_gameFieldMapper.FromModelToView(Game.Board));

            View.DisplayTheField(_gameFieldMapper.FromModelToView(Game.Board));

            View.PlacingTheWall += PlaceTheWall;

            View.PlayerMove += MakeStep;
        }

        public void MakeStep() {

            Game.SelectedCell = Game.Board.Cells[View.SelectedCellX, View.SelectedCellY];
            
            Game.ChangeTheCell();

            View.DisplayTheField(_gameFieldMapper.FromModelToView(Game.Board));

        }

        public void PlaceTheWall() {

            throw new NotImplementedException();

        }


    }
}
