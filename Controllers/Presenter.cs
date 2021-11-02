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

            View.PlacingTheWall += TryToPlaceTheWall;

            View.PlayerMove += MakeStep;

            Game.NotifyPlacingTheWall += PlaceTheWall;

            Game.NotifyCornerIsInvalid += WarnAboutInvalidCorner;

            Game.NotifyBotHasDecided += MakeBotStep;

            Game.NotifyAboutEnd += InformAboutEnd;

            View.DisplayPotentialWallsAndCorners(_gameFieldMapper.FromModelToView(Game));

            View.DisplayTheField(_gameFieldMapper.FromModelToView(Game));

        }

        public void PlaceTheWall() {

            View.PlaceTheWall();
            View.DisplayTheField(_gameFieldMapper.FromModelToView(Game));

        }

        public void WarnAboutInvalidCorner() {

            View.CantPlaceTheWall();
        
        }

        public void MakeBotStep() {

            View.DisplayTheField(_gameFieldMapper.FromModelToView(Game));

        }

        public void MakeStep() {

            Game.SelectedCell = Game.Board.Cells[View.SelectedCellX, View.SelectedCellY];
            
            Game.ChangeTheCell();

            View.DisplayTheField(_gameFieldMapper.FromModelToView(Game));

        }



        public void TryToPlaceTheWall() {

            Game.SelectedCorner = Game.Board.Corners[View.SelectedCornerX, View.SelectedCornerY];

            Game.WallIsHorizintal = View.SelectedWallIsHorizontal;

            Game.PlaceTheWall();

        }

        public void InformAboutEnd() {

            View.ThisIsTheEnd();
        
        }

    }
}
