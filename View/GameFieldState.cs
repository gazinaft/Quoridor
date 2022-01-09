using System.Collections.Generic;

namespace View
{
    public class GameFieldState
    {
        public bool[,] GridForPlayers;
        public int[,] GridForColoring;

        public bool[,][,] GridForCorners;

        public bool IsJumping { get; set; }

        public bool TheWallIsPlaced { get; set; }
        
        public bool TheWallIsHorisontal { get; set; }

        public int SelectedCornerX { get; set; }

        public int SelectedCornerY { get; set; }

        public int SelectedCellX { get; set; }

        public int SelectedCellY { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public List<(int, int)> _playersStates { get; set; }

        public int CurrentPlayerID { get; set; }
    }
}
