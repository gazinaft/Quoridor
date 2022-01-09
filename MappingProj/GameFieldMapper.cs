using View;
using Model;
using System.Collections.Generic;

namespace MappingProj
{
    public class GameFieldMapper
    {
        public GameFieldState FromModelToView(Game model) {

            GameFieldState result = new GameFieldState {
                GridForCorners = model.Board.FormGridForObstacles(),
                GridForPlayers = model.Board.FormGridForPlayers(),
                
                Height = model.Board.Height,
                Width = model.Board.Width,
                CurrentPlayerID = model.ActivePlayer.PlayerId
            };
            result.GridForColoring = new int[model.Board.Width, model.Board.Height];

            for (int i = 0; i < model.Board.Width; i++) {
                for (int j = 0; j < model.Board.Height; j++) {
                    result.GridForColoring[i, j] = result.GridForPlayers[i, j] ? 1 : 0;
                }
            }

            var secondCell = model.SecondPlayer.CurrentCell;
            result.GridForColoring[secondCell.X, secondCell.Y] = 2;
            
            if (model.SelectedCell!=null) {

                result.SelectedCellX = model.SelectedCell.X;

                result.SelectedCellY = model.SelectedCell.Y;

            }

            if (model.SelectedCorner!=null) {

                result.SelectedCornerX = model.SelectedCorner.X;

                result.SelectedCornerY = model.SelectedCorner.Y;

            }

            result.IsJumping = model.IsJumping;

            result.TheWallIsPlaced = model.TheWallIsPlaced;

            result.TheWallIsHorisontal = model.WallIsHorizontal;
            
            List<(int, int)> playersStates = new List<(int, int)>();            
            
            model.Players.ForEach(pl => playersStates.Add((pl.PlayerId, pl.WallsCounter)));

            result._playersStates = playersStates;

            return result;
        }
    }
}
