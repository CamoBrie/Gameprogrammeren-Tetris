using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.Tetris
{
    class Shape
    {
        //list of possible shapes
        public enum Shapes
        {
            C,
            L,
            RL,
            I,
            S,
            Z,
            T
        }

        public Shapes shape { get; private set; }
        public int[,] arr { get; private set; }
        public Color color { get; private set; }

        public Vector2 position;
        public Shape(Shapes currentShape, int gridWidth)
        {
            this.shape = currentShape;
            this.position = new Vector2((int)gridWidth / 2 - 1, 0);

            //set the 2d array
            SetShape(currentShape);
        }

        // https://www.ict.social/csharp/monogame/csharp-programming-games-monogame-tetris/tetris-in-monogame-block
        // code to copy an 4x4 int array, this is necessary because c# passes arrays by reference
        private int[,] CopyTiles(int[,] tiles)
        {
            int[,] newTiles = new int[4, 4];
            for (int j = 0; j < 4; j++)
                for (int i = 0; i < 4; i++)
                    newTiles[i, j] = tiles[i, j];
            return newTiles;
        }

        // https://www.ict.social/csharp/monogame/csharp-programming-games-monogame-tetris/tetris-in-monogame-block
        // code to rotate an 4x4 int array
        public void Rotate(bool right, int gridWidth)
        {
            //we can just skip the cube shape, since it doesn't rotate
            if (shape == Shapes.C)
            {
                return;
            }

            // temp array
            int[,] a = CopyTiles(arr);
            // rotate the array by swapping coordinates, like it was a matrix
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    //rotate right or left
                    if (right)
                    {
                        arr[x, y] = a[y, 3 - x];
                    }
                    else
                    {
                        arr[x, y] = a[3 - y, x];
                    }
                }
            }
            MoveToGrid(gridWidth);
        }

        //Draw the shape, we let the subclass handle it since it has access to all the data.
        //inGame means that it is actually used in the grid, instead of just drawing it aside of the grid.
        public void Draw(Vector2 border, SpriteBatch spriteBatch, Texture2D filled_block, bool inGame)
        {
            //loop through the array and draw a block where there is one
            for (int j = 0; j < 4; j++)
                for (int i = 0; i < 4; i++)
                    if (arr[i, j] > 0)
                        if (inGame)
                        {
                            spriteBatch.Draw(filled_block, new Vector2(border.X + (i + position.X) * filled_block.Width,
                            border.Y + (j + position.Y) * filled_block.Height), color);
                        }
                        else
                        {
                            spriteBatch.Draw(filled_block, new Vector2(border.X + i * filled_block.Width,
                            border.Y + j * filled_block.Height), color);
                        }

        }

        //get the width of the block
        public int GetWidth()
        {
            int result = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (arr[i, j] > 0)
                    {
                        result = i + 1;
                    }
                }

            }
            return result;
        }

        //get the width of the empty part infront of the block
        public int GetEmptyWidth()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (arr[i, j] > 0)
                    {
                        return i;
                    }
                }

            }
            return 0;
        }

        //set the current shape and color, according to the parameter
        public void SetShape(Shapes currentShape)
        {
            switch (currentShape)
            {
                //set the shape variables

                case Shapes.C:
                    int[,] cblocks =
            {
                {0, 0, 0, 0 },
                {0, 1, 1, 0 },
                {0, 1, 1, 0 },
                {0, 0, 0, 0 },
            };
                    this.arr = cblocks;
                    this.color = Color.Yellow;
                    break;

                case Shapes.L:
                    int[,] lblocks =
            {
                {0, 0, 0, 0 },
                {0, 0, 1, 0 },
                {1, 1, 1, 0 },
                {0, 0, 0, 0 },
            };
                    this.arr = lblocks;
                    this.color = Color.Orange;
                    break;

                case Shapes.RL:
                    int[,] rlblocks =
            {
                {0, 0, 0, 0 },
                {0, 1, 0, 0 },
                {0, 1, 1, 1 },
                {0, 0, 0, 0 },
            };
                    this.arr = rlblocks;
                    this.color = Color.Blue;
                    break;

                case Shapes.I:
                    int[,] iblocks =
            {
                {0, 0, 0, 0 },
                {1, 1, 1, 1 },
                {0, 0, 0, 0 },
                {0, 0, 0, 0 },
            };
                    this.arr = iblocks;
                    this.color = Color.LightBlue;
                    break;

                case Shapes.Z:
                    int[,] zblocks =
            {
                {0, 0, 0, 0 },
                {0, 1, 1, 0 },
                {0, 0, 1, 1 },
                {0, 0, 0, 0 },
            };
                    this.arr = zblocks;
                    this.color = Color.Red;
                    break;

                case Shapes.S:
                    int[,] sblocks =
            {
                {0, 0, 0, 0 },
                {0, 1, 1, 0 },
                {1, 1, 0, 0 },
                {0, 0, 0, 0 },
            };
                    this.arr = sblocks;
                    this.color = Color.LightGreen;
                    break;

                case Shapes.T:
                    int[,] tblocks =
            {
                {0, 0, 0, 0 },
                {0, 1, 0, 0 },
                {1, 1, 1, 0 },
                {0, 0, 0, 0 },
            };
                    this.arr = tblocks;
                    this.color = Color.Purple;
                    break;

                default:
                    break;

            }


        }

        //move the shape to the right or left, and constrain it to the grid.
        //AllowedMove is there to check if the move is actually allowed, as a double-check.
        public void Move(bool right, int gridWidth, bool AllowedMove)
        {
            //if going to the right
            if (right)
            {
                if (position.X + GetWidth() < gridWidth && AllowedMove)
                {
                    position.X++;
                }
            }
            //if going to the left
            else
            {
                if (position.X + GetEmptyWidth() > 0 && AllowedMove)
                    position.X--;
            }
        }

        //force the shape to the grid, sometimes when we rotate we move outside of the playable area, so we just force it inside
        public void MoveToGrid(int gridWidth)
        {
            while (position.X + GetWidth() > gridWidth)
            {
                Move(false, gridWidth, true);
            }
            while (position.X + GetEmptyWidth() < 0)
            {
                Move(true, gridWidth, true);
            }
        }
    }
}
