namespace View
{
    public class GameFieldState
    {
        public bool[,] GridForPlayers;

        public bool[,][,] GridForCorners;

        public int Height { get; set; }

        public int Width { get; set; }
    }
}
