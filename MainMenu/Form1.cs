using System;
using System.Drawing;
using System.Windows.Forms;
using Controllers;
using Model;
using Model.Network;
using View;

namespace MainMenu
{
    using System.Threading;

    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void SingleplayerClick(object sender, EventArgs e)
        {
            Game game = new Game(new ABStrategy(new ABTree(2)));
            GameViewWinForm form = new GameViewWinForm(this);
            Presenter presenter = new Presenter(form, game);
            Hide();
            form.Show();
        }

        private void HotseatClick(object sender, EventArgs e)
        {
            Game game = new Game();
            GameViewWinForm form = new GameViewWinForm(this);
            Presenter presenter = new Presenter(form, game);
            Hide();
            form.Show();
        }

        private void MultiplayerClick(object sender, EventArgs e)
        {
            var ns = new NetworkStrategy(new NetworkReader());
            Thread.Sleep(3000);
            var isFirstTurn = ns.IsFirstTurn();
            // MultiplayerButton.BackColor = isFirstTurn ? Color.Black : Color.Chartreuse;

            var game = new Game(ns);
            GameViewWinForm form = new GameViewWinForm(this);
            Presenter presenter = new Presenter(form, game);
            game._stepsHistory.AddLast(new EmptyCommand());
            Hide();
            form.Show();
            if (!isFirstTurn) game.FindNextPlayer();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}