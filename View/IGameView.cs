using System;

namespace View
{
    public interface IGameView
    {
        event Action PlacingTheWall;

        event Action PlayerMove;

        bool SelectedWallIsHorizontal { get; set; }

        int SelectedCellX { get; set; }

        int SelectedCellY { get; set; }

        int SelectedCornerX { get; set; }

        int SelectedCornerY { get; set; }

        void DisplayTheField(GameFieldState state);

        void DisplayPotentialWallsAndCorners(GameFieldState state);

        void CantPlaceTheWall();

        void PlaceTheWall();
    }
}
