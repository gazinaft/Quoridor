﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;
using Controllers;
using Model;
namespace ConsoleTestProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(new DummyStrategy());

            ConsoleView consoleView = new ConsoleView();

            Presenter presenter = new Presenter(consoleView, game);
            
        }
    }
}
