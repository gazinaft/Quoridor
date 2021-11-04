using View;
using Model;
using System.Collections.Generic;

namespace MappingProj
{
    public class GameFieldMapper
    {
        public GameFieldState FromModelToView(Game model) {

            GameFieldState result = new GameFieldState() { GridForCorners = model.Board.FormGridForObstacles(), GridForPlayers = model.Board.FormGridForPlayers(), Height = model.Board.Height, Width = model.Board.Width, CurrentPlayerID = model.ActivePlayer.PlayerId };

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
