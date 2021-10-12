using System;

namespace View
{
    public interface IGameView
    {
        event Action PlacingTheWall;

        event Action PlayerMove;

        int SelectedCellX { get; set; }

        int SelectedCellY { get; set; }

        int SelectedCornerX { get; set; }

        int SelectedCornerY { get; set; }

        void DisplayTheField(GameFieldState state);
    }
}
