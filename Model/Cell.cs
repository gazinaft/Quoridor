namespace Model
{
    public class Cell {

        public Cell(int x, int y) {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }
        public bool HasPlayer { get; set; }

    }
}
