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

        //texture variables
        private readonly Texture2D empty_block;
        private readonly Texture2D filled_block;
        private Vector2 position;

        //object variables
        private TetrisGrid grid;
        private int totalTicks;
        private Shape currentShape;
        private Random random;
        public bool gameOver;
        public int score;
        private Shape nextShape;
        private Shape savedShape;

        String specialEventName;
        double timeMulti;

        // input variables
        bool holdsLeftShift;
        bool hasSaved;

        // constructor
        public GameManager(Vector2 position, Texture2D empty_block, Texture2D filled_block)
        {
            this.position = position;
            this.empty_block = empty_block;
            this.filled_block = filled_block;
            this.random = new Random();
        }

        public void Initialize()
        {
            grid = new TetrisGrid(Settings.GridWidth, Settings.GridHeight);

            currentShape = new Shape(GenerateShape(), Settings.GridWidth);
            nextShape = new Shape(GenerateShape(), Settings.GridWidth);
            savedShape = new Shape(Shape.Shapes.I, Settings.GridWidth);

            hasSaved = false;
            specialEventName = "";
            timeMulti = 1;
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
            currentShape = nextShape;
            nextShape = new Shape(GenerateShape(), Settings.GridWidth);
            hasSaved = false;

            if (!grid.NextPosValid(currentShape, 5))
            {
                gameOver = true;
            }

        }

        public Shape.Shapes GenerateShape()
        {
            int type = random.Next(0, Enum.GetNames(typeof(Shape.Shapes)).Length);

            return (Shape.Shapes)type;
        }

        public void saveShape()
        {
            if (!hasSaved)
            {
                Shape temp = savedShape;
                Vector2 tempPos = currentShape.position;
                savedShape = currentShape;
                currentShape = temp;
                currentShape.position = tempPos;

                hasSaved = true;

            }
        }

        public void Move(Shape currentShape, bool right, int gridWidth)
        {
            if (right)
            {
                if (currentShape.position.X + currentShape.getWidth() < gridWidth && grid.NextPosValid(currentShape, 2))
                {
                    currentShape.position.X++;
                }
            }
            else
            {
                if (currentShape.position.X + currentShape.getEmptyWidth() > 0 && grid.NextPosValid(currentShape, 3))
                    currentShape.position.X--;
            }
        }

        public void MoveToGrid(int gridWidth)
        {
            while (currentShape.position.X + currentShape.getWidth() > gridWidth)
            {
                Move(currentShape, false, gridWidth);
            }
            while (currentShape.position.X + currentShape.getEmptyWidth() < 0)
            {
                Move(currentShape, true, gridWidth);
            }
        }

        public void SpecialEvent()
        {
            int currentEvent = random.Next(0, 5);

            switch (currentEvent)
            {
                case 0:
                    grid.GravSwitch(true);
                    specialEventName = "Gravity Switch to the Right!";
                    break;
                case 1:
                    grid.GravSwitch(false);
                    specialEventName = "Gravity Switch to the Left!";
                    break;
                case 2:
                    timeMulti = 2;
                    specialEventName = "Time speedup!";
                    break;
                case 3:
                    timeMulti = 0.5;
                    specialEventName = "Time slowdown!";
                    break;
                default:
                    specialEventName = "No event!";
                    break;
            }
        }

        // handles the input before we update
        public void HandleInput(GameTime gameTime, InputHelper inputHelper)
        {
            holdsLeftShift = false;

            if (inputHelper.KeyPressed(Keys.Left))
            {
                Move(currentShape, false, grid.width);
            }
            if (inputHelper.KeyPressed(Keys.Right))
            {
                Move(currentShape, true, grid.width);
            }
            if (inputHelper.KeyPressed(Keys.A))
            {
                currentShape.Rotate(false, grid.width, this);
            }
            if (inputHelper.KeyPressed(Keys.D))
            {
                currentShape.Rotate(true, grid.width, this);
            }
            if (inputHelper.KeyDown(Keys.LeftShift))
            {
                holdsLeftShift = true;
            }
            if (inputHelper.KeyDown(Keys.S))
            {
                saveShape();

            }
        }

        // runs everytime an update is needed
        public void Update(GameTime gameTime)
        {
            totalTicks++;


            if (grid.currentscore > 0)
            {
                score += grid.currentscore;
                grid.currentscore = 0;
            }

            grid.CheckRows();


            int temp1, temp2;
            if (score != 0)
            {
                    temp1 = (int)((31 - Settings.StartingDifficulty) -  Math.Floor(Math.Log10(score)));
                    temp2 = (int)(6 - Math.Log10(score) / Math.Log10(100));

            }
            else 
            {
                temp1 = 31 - Settings.StartingDifficulty;
                temp2 = 6;
            }

            if (!grid.NextPosValid(currentShape, 0) && totalTicks % 5 == 0)
            {
                NewShape();
                return;
            }

            if (!holdsLeftShift && totalTicks % (temp1 / timeMulti) == 0 && grid.NextPosValid(currentShape, 0))
            {
                currentShape.position.Y++;
            }
            if (holdsLeftShift && totalTicks % (temp2/timeMulti) == 0 && grid.NextPosValid(currentShape, 0))
            {
                currentShape.position.Y++;
            }

            if (totalTicks % 300 == 0 && Settings.SpecialEvents)
            {
                timeMulti = 1;
                specialEventName = "";
            }

            if (totalTicks % 600 == 0 && Settings.SpecialEvents)
            {
                SpecialEvent();
            }

        }

        // runs everytime we draw to the screen
        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {

            spriteBatch.Draw(filled_block, new Rectangle((int)position.X, (int)position.Y, grid.width * 32, grid.height * 32), Color.Gray);

            for (int i = 0; i < grid.width; i++)
            {
                for (int j = 0; j < grid.height; j++)
                {
                    if (!Settings.HiddenMode)
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



            }
            currentShape.Draw(position, spriteBatch, filled_block, true);

            spriteBatch.DrawString(font, $"Next piece: ", new Vector2(700, 300), Color.White);
            nextShape.Draw(new Vector2(700, 350), spriteBatch, filled_block, false);

            spriteBatch.DrawString(font, $"Saved piece: ", new Vector2(700, 500), Color.White);
            savedShape.Draw(new Vector2(700, 550), spriteBatch, filled_block, false);

            spriteBatch.DrawString(font, $"score: {score}", new Vector2(700, 100), Color.White);

            if (Settings.SpecialEvents)
            {
                spriteBatch.DrawString(font, $"time until next event: {Math.Round(((double)600 - (totalTicks % 600)) / 60)}", new Vector2(600, 750), Color.White);
                spriteBatch.DrawString(font, specialEventName, new Vector2(550, 800), Color.White);
            }
        }
    }
}
