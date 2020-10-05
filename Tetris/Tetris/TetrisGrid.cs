using Microsoft.Xna.Framework;
using System;

namespace Tetris.Tetris
{
    class TetrisGrid
    {
        public int width;
        public int height;
        public Color[,] placedTiles;

        public TetrisGrid(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.placedTiles = new Color[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    placedTiles[i, j] = Color.White;
                }
            }
        }

        public bool NextPosValid(Shape currentShape)
        {
            float currentpos = currentShape.position.Y;
            currentShape.position.Y++;

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    //x: x-value shape
                    //y: y-value shape
                    if (currentShape.arr[x, y] > 0)
                    {
                        if (currentShape.position.Y + y >= height)
                        {
                            currentShape.position.Y = currentpos;
                            return false;
                        }

                        if (placedTiles[(int)currentShape.position.X + x, (int)currentShape.position.Y + y] != Color.White)
                        {
                            currentShape.position.Y = currentpos;
                            return false;
                        }

                    }

                }
            }
            currentShape.position.Y = currentpos;
            return true;
        }
    }
}
