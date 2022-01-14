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
            Game.DoDisplayStep = true;
            _gameFieldMapper = new GameFieldMapper();

            View.PlacingTheWall += TryToPlaceTheWall;
            View.PlayerMove += MakeStep;
            View.ChangePlayer += ChangePlayers;
            View.DoUndo += DoUndo;
            Game.NotifyPlacingTheWall += PlaceTheWall;
            Game.NotifyCornerIsInvalid += WarnAboutInvalidCorner;
            Game.NotifyBotHasDecided += MakeBotStep;
            Game.NotifyAboutEnd += InformAboutEnd;

            View.DisplayPotentialWallsAndCorners(_gameFieldMapper.FromModelToView(Game));
            View.DisplayTheField(_gameFieldMapper.FromModelToView(Game));
        }

        public void ChangePlayers()
        {
            Game.ChangePlayers();
        }

        public void DoUndo()
        {
            View.DisplayTheField(_gameFieldMapper.FromModelToView(Game));
        }

        public void PlaceTheWall()
        {
            View.PlaceTheWall(_gameFieldMapper.FromModelToView(Game));
            View.DisplayTheField(_gameFieldMapper.FromModelToView(Game));
        }

        public void WarnAboutInvalidCorner()
        {
            View.CantPlaceTheWall();
        }

        public void MakeBotStep()
        {
            View.DisplayTheField(_gameFieldMapper.FromModelToView(Game));
        }

        public void MakeStep()
        {
            Game.SelectedCell = Game.Board.Cells[View.SelectedCellX, View.SelectedCellY];
            Game.MovePlayerCommand = new MovePlayerCommand(Game.SelectedCell);
            Game._stepsHistory.AddLast(Game.MovePlayerCommand);
            Game.MovePlayerCommand.Execute(Game);
            View.DisplayTheField(_gameFieldMapper.FromModelToView(Game));
        }

        public void TryToPlaceTheWall()
        {
            Game.SelectedCorner = Game.Board.Corners[View.SelectedCornerX, View.SelectedCornerY];
            Game.WallIsHorizontal = View.SelectedWallIsHorizontal;
            Game.PlaceTheWallCommand =
                new PlaceWallCommand(Game.SelectedCorner.X, Game.SelectedCorner.Y, Game.WallIsHorizontal);
            Game._stepsHistory.AddLast(Game.PlaceTheWallCommand);
            Game.PlaceTheWallCommand.Execute(Game);
            View.DisplayTheField(_gameFieldMapper.FromModelToView(Game));
        }

        public void InformAboutEnd(int id)
        {
            View.ThisIsTheEnd(id);
        }
    }
}