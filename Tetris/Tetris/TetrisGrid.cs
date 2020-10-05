using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Tetris
{
    class TetrisGrid
    {
        public int width;
        public int height;
        Color[,] placedTiles;

        public TetrisGrid(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.placedTiles = new Color[width, height];
        }
    }
}
