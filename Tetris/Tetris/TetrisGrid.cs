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

        /// <summary>
        /// moves all the placed tiles to one side or the other, based on the isRight parameter/
        /// </summary>
        public void GravSwitch(bool isRight)
        {
            for(int row = 0; row < height; row++)
            {
                //populate empty array
                Color[] temp = new Color[width];
                for(int i = 0; i<temp.Length; i++)
                {
                    temp[i] = Color.White;
                }

                int pointer = 0;
                if(isRight)
                {
                    pointer = temp.Length - 1;
                }

                //check and move the tile to the left
                for (int col = 0; col < width; col++)
                {
                    if(placedTiles[col,row] != Color.White)
                    {
                        temp[pointer] = placedTiles[col,row];
                        pointer += isRight ? -1 : 1;
                    }
                }

                //update the new array
                for (int col = 0; col < width; col++)
                {
                    placedTiles[col, row] = temp[col];
                }
            }
        }
        /// <summary>
        /// TODO: hella ugly but it works (make it better)
        /// there is probably a better way to do this, but this is all I could come up with without copying code from the internet
        /// </summary>
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

            //set temp variables
            int linesCleared = 0;
            bool flag = false;

            //count the lines cleared
            for (int i = 0; i < height; i++)
            {
                int counter = 0;

                //count the amount of tiles in a row
                for (int j = 0; j < width; j++)
                {
                    if (placedTiles[j, i] != Color.White)
                    {
                        counter++;

                    }
                }

                //full row = line clear
                if (counter == width)
                {
                    flag = true;
                    linesCleared++;
                    
                    //move all the lines above the cleared line down 
                    for (int j = 0; j < width; j++)
                    {
                        for (int x = i; x > 0; x--)
                        {
                            temparr[j, x] = placedTiles[j, x - 1];
                        }
                    }
                }
            }

            //if a line is cleared, add to the score and replace the array
            if (flag)
            {
                placedTiles = temparr;
                currentscore += 200 * (int)Math.Floor(Math.Pow(linesCleared, 2));
            }
        }

        ///check if the next position is valid
        public bool NextPosValid(Shape currentShape, int direction)
        {
            Vector2 currentpos = currentShape.position;

            //change the next position based on the parameter
            switch (direction)
            {
                case 0: currentShape.position.Y++; break;
                case 1: currentShape.position.Y--; break;
                case 2: currentShape.position.X++; break;
                case 3: currentShape.position.X--; break;
                default: break;
            }


            //loop through all the blocks and see if it overlaps the current grid
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

                        if (placedTiles.GetLength(0) > (int)currentShape.position.X + x && placedTiles.GetLength(1) > (int)currentShape.position.Y + y) {
                            if (placedTiles[(int)currentShape.position.X + x, (int)currentShape.position.Y + y] != Color.White)
                            {
                                currentShape.position = currentpos;
                                return false;
                            }
                        }

                    }

                }
            }

            //reset the position
            currentShape.position = currentpos;
            return true;
        }
    }
}
