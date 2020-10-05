using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Tetris
{
    class Shape
    {
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
        public Shape(Shapes currentShape)
        {
            this.shape = currentShape;
            this.position = new Vector2(0, 0);
            setShape(currentShape);
        }

        public void Fall()
        {
            this.position.Y++;
        }

        public void Move(bool right, int gridWidth)
        {
            if(right)
            {
                if (this.position.X + getWidth() < gridWidth)
                {
                    this.position.X++;
                }
            } else
            {
                if(this.position.X + getEmptyWidth() > 0)
                this.position.X--;
            }
        }

        // https://www.ict.social/csharp/monogame/csharp-programming-games-monogame-tetris/tetris-in-monogame-block
        // code to copy an 4x4 int array, this is necessary because c# passes array's by reference
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
        public void RotateRight(int gridWidth)
        {
            if(shape == Shapes.C)
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
                    arr[x, y] = a[y, 3- x];
                    MoveToGrid(gridWidth);

                }
            }
        }

        public void RotateLeft(int gridWidth)
        {
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
                    arr[x, y] = a[3- y, x];
                    MoveToGrid(gridWidth);
                }
            }

            
        }

        public void Draw(Vector2 border, SpriteBatch spriteBatch, Texture2D filled_block)
        {
            for (int j = 0; j < 4; j++)
                for (int i = 0; i < 4; i++)
                    if (arr[i, j] > 0)
                        spriteBatch.Draw(filled_block, new Vector2(border.X + (i + position.X) * filled_block.Width,
                        border.Y + (j + position.Y) * filled_block.Height), color);
        }

        private void MoveToGrid(int gridWidth)
        {
            while (position.X + getWidth() > gridWidth)
            {
                Move(false, gridWidth);
            }
            while (position.X + getEmptyWidth() < 0)
            {
                Move(true, gridWidth);
            }
        }

        private int getWidth()
        {
            int result = 0;
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    if(arr[i,j] > 0)
                    {
                        result = i+1;
                    }
                }

            }
            return result;
        }

        private int getEmptyWidth()
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

        private void setShape(Shapes currentShape)
        {
            switch (currentShape) {
                //set the shape variables

                case Shapes.C:
                    int[,] cblocks =
            {
                {1, 1, 0, 0 },
                {1, 1, 0, 0 },
                {0, 0, 0, 0 },
                {0, 0, 0, 0 },
            };
                    this.arr = cblocks;
                    this.color = Color.Yellow;
                    break;

                case Shapes.L:
                    int[,] lblocks =
            {
                {0, 0, 1, 0 },
                {1, 1, 1, 0 },
                {0, 0, 0, 0 },
                {0, 0, 0, 0 },
            };
                    this.arr = lblocks;
                    this.color = Color.Orange;
                    break;

                case Shapes.RL:
                    int[,] rlblocks =
            {
                {1, 0, 0, 0 },
                {1, 1, 1, 0 },
                {0, 0, 0, 0 },
                {0, 0, 0, 0 },
            };
                    this.arr = rlblocks;
                    this.color = Color.Blue;
                    break;

                case Shapes.I:
                    int[,] iblocks =
            {
                {1, 1, 1, 1 },
                {0, 0, 0, 0 },
                {0, 0, 0, 0 },
                {0, 0, 0, 0 },
            };
                    this.arr = iblocks;
                    this.color = Color.LightBlue;
                    break;

                case Shapes.Z:
                    int[,] zblocks =
            {
                {1, 1, 0, 0 },
                {0, 1, 1, 0 },
                {0, 0, 0, 0 },
                {0, 0, 0, 0 },
            };
                    this.arr = zblocks;
                    this.color = Color.Red;
                    break;

                case Shapes.S:
                    int[,] sblocks =
            {
                {0, 1, 1, 0 },
                {1, 1, 0, 0 },
                {0, 0, 0, 0 },
                {0, 0, 0, 0 },
            };
                    this.arr = sblocks;
                    this.color = Color.LightGreen;
                    break;

                case Shapes.T:
                    int[,] tblocks =
            {
                {0, 1, 0, 0 },
                {1, 1, 1, 0 },
                {0, 0, 0, 0 },
                {0, 0, 0, 0 },
            };
                    this.arr = tblocks;
                    this.color = Color.Purple;
                    break;

                default:
                    break;

        }


        }
    }
}
