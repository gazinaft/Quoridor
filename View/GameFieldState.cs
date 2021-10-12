﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View
{
    public class GameFieldState
    {
        public bool[,] GridForPlayers;

        public bool[,][,] GridForCorners;

        public int Height { get; set; }

        public int Width { get; set; }
    }
}
