namespace Model {
    public class Corner {

        public Corner(int x, int y) {
            X = x;
            Y = y;
            Obstacles = new bool[3,3];
        }

        public Corner(Corner other) {
            X = other.X;
            Y = other.Y;
            Obstacles = new bool[3, 3];
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    Obstacles[i, j] = other.Obstacles[i, j];
                }
            }
        }

        public int X { get; }
        public int Y { get; }
        public bool[,] Obstacles;
    }
}
