using System;
using System.Drawing;
using System.Windows.Forms;

namespace View
{
    public partial class GameViewWinForm : Form, IGameView
    {
        Button[,] ButtonGrid;

        Point[,] CornerGrid;

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

            CornerGrid = new Point[CurrentState.GridForCorners.GetLength(0), CurrentState.GridForCorners.GetLength(0)];

            GamePanel.Width = 2800;

            GamePanel.Height = 2800;

            double buttonSize = GamePanel.Width / state.GridForPlayers.Length;

            double wallSize = buttonSize / 5;

            GamePanel.Height = GamePanel.Width;

            for (int i = 0; i < state.Width; i++ ) {

                for (int j =0; j < state.Height; j++) {

                    ButtonGrid[i, j] = new Button();

                    ButtonGrid[i, j].Height = (int)buttonSize;

                    ButtonGrid[i, j].Width = (int)buttonSize;

                    if (CurrentState.GridForPlayers[i, j]) {

                        //ButtonGrid[i, j].Text = "PLAYER";

                        ButtonGrid[i, j].Image = Image.FromFile("C:\\Users\\Robert\\source\\repos\\Quoridor\\pictures\\fishka.jpg");

                    }

                    GamePanel.Controls.Add(ButtonGrid[i, j]);

                    ButtonGrid[i, j].Location = new Point( i * ((int)buttonSize + (int)wallSize), j* ((int)buttonSize + (int)wallSize));

                    ButtonGrid[i, j].Click += GameViewWinForm_Click;

                }

                

            }

            
            for (int i = 0; i < state.Width; i++)
            {
                for (int j = 1; j < state.Height; j++)
                {
                    

                    Button rightWall = new Button();

                    rightWall.Height = (2 * (int)buttonSize + (int)wallSize);

                    rightWall.Width = (int)wallSize;

                    rightWall.Location = new Point(ButtonGrid[i, j].Location.X - (int)wallSize, ButtonGrid[i, j].Location.Y - (int)buttonSize - (int)wallSize);

                    GamePanel.Controls.Add(rightWall);

                }

            }

            for (int i = 0; i < state.Width-1; i++)
            {
                for (int j = 1; j < state.Height; j++)
                {
                    Button higherWall = new Button();

                    higherWall.Width = (2 * (int)buttonSize + (int)wallSize);

                    higherWall.Height = (int)wallSize;

                    higherWall.Location = new Point(ButtonGrid[i, j].Location.X, ButtonGrid[i, j].Location.Y - (int)wallSize);

                    GamePanel.Controls.Add(higherWall);

                }

            }

            //throw new NotImplementedException();
        }

        private void GameViewWinForm_Click(object sender, EventArgs e)
        {
            Button selectedButton = (Button)sender;

            double wallSize = selectedButton.Width / 5;

            SelectedCellX = selectedButton.Location.X / (selectedButton.Width + (int) wallSize);

            SelectedCellY = selectedButton.Location.Y / (selectedButton.Width + (int)wallSize);

            foreach (Button b in ButtonGrid) {

                b.Dispose();
            
            }

            ChangeTheCell();

            //MessageBox.Show("Selected X: " + SelectedCellX + " Selected Y: " + SelectedCellY);

        }

        public void ChangeTheCell() {

            PlayerMove.Invoke();

        }
    }
}
