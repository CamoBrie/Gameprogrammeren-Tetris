using Helpers;
using Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace Tetris.Tetris
{
    class GameManager
    {

        private Shape CShape;
        private Shape LShape;
        private Shape RLShape;
        private Shape IShape;
        private Shape SShape;
        private Shape ZShape;
        private Shape TShape;

        //texture variables
        private readonly Texture2D empty_block;
        private readonly Texture2D filled_block;
        private Vector2 position;

        //object variables
        private TetrisGrid grid;
        private int totalTicks;
        private Shape currentShape;
        private Random random;

        // input variables
        bool holdsLeftShift;

        // constructor
        public GameManager(Vector2 position, Texture2D empty_block, Texture2D filled_block)
        {
            this.position = position;
            this.empty_block = empty_block;
            this.filled_block = filled_block;
            random = new Random();
        }

        public void Initialize()
        {
            this.grid = new TetrisGrid(Settings.GridWidth, Settings.GridHeight);

            this.currentShape = new Shape(Shape.Shapes.L, Settings.GridWidth);
        }

        public void NewShape()
        {

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (currentShape.arr[i, j] > 0)
                    {
                        grid.placedTiles[(int)Math.Floor(currentShape.position.X) + i, (int)Math.Floor(currentShape.position.Y) + j] = currentShape.color;
                    }

                }
            }
            this.currentShape = new Shape(GenerateShape(), Settings.GridWidth);
        }

        public Shape.Shapes GenerateShape()
        {
            int type = random.Next(0, Enum.GetNames(typeof(Shape.Shapes)).Length);

            return (Shape.Shapes)type;
        }

        // handles the input before we update
        public void HandleInput(GameTime gameTime, InputHelper inputHelper)
        {
            holdsLeftShift = false;

            if (inputHelper.KeyPressed(Keys.Left))
            {
                this.currentShape.Move(false, grid.width);
            }
            if (inputHelper.KeyPressed(Keys.Right))
            {
                this.currentShape.Move(true, grid.width);
            }
            if (inputHelper.KeyPressed(Keys.A))
            {
                this.currentShape.Rotate(false, grid.width);
            }
            if (inputHelper.KeyPressed(Keys.D))
            {
                this.currentShape.Rotate(true, grid.width);
            }
            if (inputHelper.KeyDown(Keys.LeftShift))
            {
                holdsLeftShift = true;
            }
        }

        // runs everytime an update is needed
        public void Update(GameTime gameTime)
        {
            this.totalTicks++;

            if (!grid.NextPosValid(this.currentShape))
            {
                NewShape();
                return;
            }

            if (!holdsLeftShift && this.totalTicks % 30 == 0)
            {
                currentShape.position.Y++;
            }
            if (holdsLeftShift && this.totalTicks % 6 == 0)
            {
                currentShape.position.Y++;
            }


        }

        // runs everytime we draw to the screen
        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            for (int i = 0; i < grid.width; i++)
            {
                for (int j = 0; j < grid.height; j++)
                {
                    if (grid.placedTiles[i, j] != Color.White)
                    {
                        spriteBatch.Draw(filled_block, Vector2.Add(position, new Vector2(i * 32, j * 32)), grid.placedTiles[i, j]);
                    }
                    else
                    {
                        spriteBatch.Draw(empty_block, Vector2.Add(position, new Vector2(i * 32, j * 32)), Color.White);
                    }
                }



            }
            currentShape.Draw(position, spriteBatch, filled_block);
        }
    }
}
