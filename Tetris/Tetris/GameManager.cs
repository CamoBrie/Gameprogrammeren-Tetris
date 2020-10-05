using Helpers;
using Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        // constructor
        public GameManager(Vector2 position, Texture2D empty_block, Texture2D filled_block)
        {
            this.position = position;
            this.empty_block = empty_block;
            this.filled_block = filled_block;
        }

        public void Initialize()
        {
            this.grid = new TetrisGrid(Settings.GridWidth, Settings.GridHeight);

            this.currentShape = new Shape(Shape.Shapes.L);
        }

        // handles the input before we update
        public void HandleInput(GameTime gameTime, InputHelper inputHelper)
        {
            if (inputHelper.KeyPressed(Keys.Right))
            {
                this.currentShape.Move(true, grid.width);
            }
            if (inputHelper.KeyPressed(Keys.Left))
            {
                this.currentShape.Move(false, grid.width);
            }
            if (inputHelper.KeyPressed(Keys.A))
            {
                this.currentShape.RotateLeft(grid.width);
            }
            if (inputHelper.KeyPressed(Keys.D))
            {
                this.currentShape.RotateRight(grid.width);
            }
        }

        // runs everytime an update is needed
        public void Update(GameTime gameTime)
        {
            this.totalTicks++;

            if(this.totalTicks % 30 == 0)
            {
                currentShape.Fall();
            }
        }

        // runs everytime we draw to the screen
        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            for(int i = 0; i < grid.width; i++)
            {
                for(int j = 0; j < grid.height; j++)
                {
                    spriteBatch.Draw(empty_block, Vector2.Add(position, new Vector2(i*32, j*32)), Color.White);
                }



            }
            currentShape.Draw(position, spriteBatch, filled_block);
        }
    }
}
