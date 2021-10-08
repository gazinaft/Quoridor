using System;
using System.Windows.Forms;

namespace View
{
    public partial class GameViewWinForm : Form, IGameView
    {
        public GameViewWinForm()
        {
            InitializeComponent();
        }

        public int SelectedCellX { get ; set; }
        public int SelectedCellY { get; set; }
        public int SelectedCornerX { get; set; }
        public int SelectedCornerY { get; set; }

        public event Action PlacingTheWall;
        public event Action PlayerMove;

        public void DisplayTheField(GameFieldState state)
        {
            throw new NotImplementedException();
        }
    }
}
