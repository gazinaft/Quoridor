using System;
using System.Drawing;
using System.Windows.Forms;

namespace View
{
    public partial class GameViewWinForm : Form, IGameView
    {
        Button[,] ButtonGrid;

        public GameViewWinForm()
        {
            InitializeComponent();

            
        }

        public GameViewWinForm(GameFieldState gameFieldState)
        {
            InitializeComponent();

            CurrentState = gameFieldState;

            ButtonGrid = new Button[CurrentState.Height, CurrentState.Width];
        }

        public GameFieldState CurrentState { get; set; }

        public int SelectedCellX { get ; set; }
        public int SelectedCellY { get; set; }
        public int SelectedCornerX { get; set; }
        public int SelectedCornerY { get; set; }

        public event Action PlacingTheWall;
        public event Action PlayerMove;

        public void DisplayTheField(GameFieldState state)
        {
            CurrentState = state;

            ButtonGrid = new Button[CurrentState.Height, CurrentState.Width];

            GamePanel.Width = 2800;

            GamePanel.Height = 2800;

            double buttonSize = GamePanel.Width / state.GridForPlayers.Length;

            GamePanel.Height = GamePanel.Width;

            for (int i = 0; i < state.Width; i++ ) {

                for (int j =0; j < state.Height; j++) {

                    ButtonGrid[i, j] = new Button();

                    ButtonGrid[i, j].Height = (int)buttonSize;

                    ButtonGrid[i, j].Width = (int)buttonSize;

                    if (CurrentState.GridForPlayers[i, j]) {

                        ButtonGrid[i, j].Text = "PLAYER";

                    }

                    GamePanel.Controls.Add(ButtonGrid[i, j]);

                    ButtonGrid[i, j].Location = new Point( i * (int)buttonSize, j* (int)buttonSize);


                    //ButtonGrid[i, j].Text = "КНОПКА";
                }
            
            }

            //throw new NotImplementedException();
        }
    }
}
