using System.Collections.Generic;

namespace View
{
    public class GameFieldState
    {
        public bool[,] GridForPlayers;

        public bool[,][,] GridForCorners;

        public int Height { get; set; }

        public int Width { get; set; }

        public List<(int, int)> _playersStates { get; set; }

        public int CurrentPlayerID { get; set; }
    }
}
