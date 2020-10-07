using Microsoft.Xna.Framework;
using System;

namespace Tetris.Tetris
{
    class TetrisGrid
    {
        public int width;
        public int height;
        public Color[,] placedTiles;
        public int currentscore;

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

        public void GravSwitch(bool isRight)
        {
            Color temp;
            if (isRight)
            {
                for (int times = 0; times < 3; times++)
                {
                    for (int x = 0; x < height; x++)
                    {
                        for (int i = 0; i <= width - 1; i++)
                        {
                            for (int j = i + 1; j < width; j++)
                            {
                                if (placedTiles[i, x].PackedValue < placedTiles[j, x].PackedValue)
                                {
                                    temp = placedTiles[i, x];
                                    placedTiles[i, x] = placedTiles[j, x];
                                    placedTiles[j, x] = temp;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                for (int times = 0; times < 3; times++)
                {
                    for (int x = 0; x < height; x++)
                    {
                        for (int i = 0; i <= width - 1; i++)
                        {
                            for (int j = i + 1; j < width; j++)
                            {
                                if (placedTiles[i, x].PackedValue > placedTiles[j, x].PackedValue)
                                {
                                    temp = placedTiles[i, x];
                                    placedTiles[i, x] = placedTiles[j, x];
                                    placedTiles[j, x] = temp;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void CheckRows()
        {
            // create new empty array
            Color[,] temparr = new Color[width, height];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    temparr[j, i] = placedTiles[j, i];
                }
            }

            int linesCleared = 0;
            bool flag = false;

            for (int i = 0; i < height; i++)
            {
                int counter = 0;

                for (int j = 0; j < width; j++)
                {
                    if (placedTiles[j, i] != Color.White)
                    {
                        counter++;

                    }
                }

                if (counter == width)
                {
                    flag = true;
                    linesCleared++;

                    for (int j = 0; j < width; j++)
                    {
                        for (int x = i; x > 0; x--)
                        {
                            temparr[j, x] = placedTiles[j, x - 1];
                        }
                    }
                }
            }

            if (flag)
            {
                placedTiles = temparr;
                currentscore += 200 * (int)Math.Floor(Math.Pow(linesCleared, 2));
            }




        }

        public bool NextPosValid(Shape currentShape, int direction)
        {
            Vector2 currentpos = currentShape.position;

            switch (direction)
            {
                case 0: currentShape.position.Y++; break;
                case 1: currentShape.position.Y--; break;
                case 2: currentShape.position.X++; break;
                case 3: currentShape.position.X--; break;
                default: break;
            }



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
                            currentShape.position = currentpos;
                            return false;
                        }

                        if (placedTiles[(int)currentShape.position.X + x, (int)currentShape.position.Y + y] != Color.White)
                        {
                            currentShape.position = currentpos;
                            return false;
                        }

                    }

                }
            }
            currentShape.position = currentpos;
            return true;
        }
    }
}
