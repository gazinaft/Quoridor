﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using View;
using Model;
using Controllers;
namespace TestProgramm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            

            Application.EnableVisualStyles();
            
            Application.SetCompatibleTextRenderingDefault(false);    

            Game game = new Game();

            GameViewWinForm form = new GameViewWinForm();

            Presenter presenter = new Presenter(form, game);

            Application.Run(form);


        }
    }
}