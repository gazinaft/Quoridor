using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controllers;
using Model;
using View;
namespace MainMenu
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void SinglePlayerButton_Click(object sender, EventArgs e)
        {
            Game game = new Game(new ABStrategy(new ABTree(1)));

            //Game game = new Game(new DummyStrategy());

            GameViewWinForm form = new GameViewWinForm(this);

            Presenter presenter = new Presenter(form, game);

            this.Hide();

            form.Show();

        }

        private void buttonTwoPlayers_Click(object sender, EventArgs e)
        {
            Game game = new Game();

            GameViewWinForm form = new GameViewWinForm(this);

            Presenter presenter = new Presenter(form, game);

            this.Hide();

            form.Show();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
