namespace Model {
    public class Corner {

        public Corner(int x, int y) {
            X = x;
            Y = y;
            Obstacles = new bool[3,3];
        }

        public int X { get; }
        public int Y { get; }
        public bool[,] Obstacles;
    }
}
