using Helpers;
using Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

//due to conflict we need to define this
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace Tetris.Tetris
{
    class GameManager
    {

        //texture variables
        private readonly Texture2D empty_block;
        private readonly Texture2D filled_block;

        //position of the grid relative to the screen
        private Vector2 position;

        //object variables
        private TetrisGrid grid;
        private readonly Random random;

        //gamestate variables
        public bool gameOver;
        public int score;

        //total ticks elapsed
        private int totalTicks;

        //special event variables
        private String specialEventName;
        private double timeMulti;
        private bool hiddenEvent;

        //shape variables
        public Shape currentShape;
        private Shape nextShape;
        private Shape savedShape;


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

        //set the initial variables, this function will be called when the game starts, not when the application starts
        public void Initialize()
        {
            //set the grid to the size defined in the settings
            grid = new TetrisGrid(Settings.GridWidth, Settings.GridHeight);

            //set the shapes
            currentShape = new Shape(GenerateShape(), Settings.GridWidth);
            nextShape = new Shape(GenerateShape(), Settings.GridWidth);

            //freebie! This could be seen as a feature, and also as a way to circumvent bugs (which one is it?). 
            savedShape = new Shape(Shape.Shapes.I, Settings.GridWidth);

            //set variables to keep track of gamestates
            hasSaved = false;
            specialEventName = "";
            timeMulti = 1;
            hiddenEvent = false;
        }

        //place a shape, set the currentshape and create a new one
        public void NewShape()
        {
            //place the current shape in the array
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (currentShape.arr[i, j] > 0)
                    {
                        if((int)Math.Floor(currentShape.position.X) + i < grid.width && (int)Math.Floor(currentShape.position.Y) + j < grid.height)
                        grid.placedTiles[(int)Math.Floor(currentShape.position.X) + i, (int)Math.Floor(currentShape.position.Y) + j] = currentShape.color;
                    }

                }
            }

            //set the next shape and create a new one.
            currentShape = nextShape;
            nextShape = new Shape(GenerateShape(), Settings.GridWidth);

            //this means that you can swap with the saved block again
            hasSaved = false;

            //check if game over
            if (!grid.NextPosValid(currentShape, 5))
            {
                gameOver = true;
            }

        }

        //it generates a shape based on the amount of shapes
        public Shape.Shapes GenerateShape()
        {
            int type = random.Next(0, Enum.GetNames(typeof(Shape.Shapes)).Length);

            return (Shape.Shapes)type;
        }

        //switch the currentshape with the saved shape
        public void SaveShape()
        {
            //can only save once per shape
            if (!hasSaved)
            {
                //temporary variables to keep track of the shape
                Shape temp = savedShape;

                //switch out the variables using a temp variable and reset shape position to top of grid
                savedShape = currentShape;
                currentShape = temp;
                currentShape.position = new Vector2((int)grid.width / 2 - 1, 0); ;

                //set it so we have saved already this shape
                hasSaved = true;

            }
        }

        //sets the special event and changes the variables accordingly
        public void SpecialEvent()
        {
            //create a random event
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
                case 4:
                    hiddenEvent = true;
                    specialEventName = "Hidden Mode!";
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

            //handle the keypresses
            if (inputHelper.KeyPressed(Keys.Left))
            {
                currentShape.Move(false, grid.width, grid.NextPosValid(currentShape, 2));
            }
            if (inputHelper.KeyPressed(Keys.Right))
            {
                currentShape.Move(true, grid.width, grid.NextPosValid(currentShape, 3));
            }
            if (inputHelper.KeyPressed(Keys.A))
            {
                currentShape.Rotate(false, grid.width);
            }
            if (inputHelper.KeyPressed(Keys.D))
            {
                currentShape.Rotate(true, grid.width);
            }
            if (inputHelper.KeyDown(Keys.LeftShift))
            {
                holdsLeftShift = true;
            }
            if (inputHelper.KeyPressed(Keys.S))
            {
                SaveShape();

            }
        }

        // runs everytime an update is needed
        public void Update(GameTime gameTime)
        {
            //add a tick to the game, I found using this easier than using the gametime variable.
            totalTicks++;

            //add the score to the total
            if (grid.currentscore > 0)
            {
                score += grid.currentscore;
                grid.currentscore = 0;
            }

            //clear any rows if possible
            grid.CheckRows();

            //create temporary variables to set the gamespeed
            int temp1, temp2;
            if (score != 0)
            {
                //has some score, so we can make the game more difficult based on it
                    temp1 = (int)((31 - Settings.StartingDifficulty) -  Math.Floor(Math.Log10(score)));
                    temp2 = (int)(6 - Math.Log10(score) / Math.Log10(100));

            }
            else 
            {
                temp1 = 31 - Settings.StartingDifficulty;
                temp2 = 6;
            }

            //check if the next position is valid and creates a new shape based on it.
            if (!grid.NextPosValid(currentShape, 0) && totalTicks % 5 == 0)
            {
                NewShape();
                return;
            }

            //normal behavior and runs based on temp variables
            if (!holdsLeftShift && totalTicks % (temp1/timeMulti) == 0 && grid.NextPosValid(currentShape, 0))
            {
                currentShape.position.Y++;
            }

            //while holding shift (quick drop), drop faster
            if (holdsLeftShift && totalTicks % (temp2/timeMulti) == 0 && grid.NextPosValid(currentShape, 0))
            {
                currentShape.position.Y++;
            }

            //reset the special event every 5 seconds
            if (totalTicks % 300 == 0 && Settings.SpecialEvents)
            {
                timeMulti = 1;
                hiddenEvent = false;
                specialEventName = "";

            }

            //every 10 seconds, create an event
            if (totalTicks % 600 == 0 && Settings.SpecialEvents)
            {
                SpecialEvent();
            }

        }

        // runs everytime we draw to the screen
        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            //draw a background for the grid
            spriteBatch.Draw(filled_block, new Rectangle((int)position.X, (int)position.Y, grid.width * 32, grid.height * 32), Color.Gray);

            //draw the grid based on filled blocks and give a color to them
            for (int i = 0; i < grid.width; i++)
            {
                for (int j = 0; j < grid.height; j++)
                {
                    if (!(Settings.HiddenMode || (hiddenEvent && j > grid.height-10)))
                    {
                        if (grid.placedTiles[i, j] != Color.White )
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

            //draw the currentshape in the grid
            currentShape.Draw(position, spriteBatch, filled_block, true);

            //draw the next piece
            spriteBatch.DrawString(font, $"Next piece: ", new Vector2(700, 300), Color.White);
            nextShape.Draw(new Vector2(700, 350), spriteBatch, filled_block, false);

            //draw the saved piece
            spriteBatch.DrawString(font, $"Saved piece: ", new Vector2(700, 500), Color.White);
            savedShape.Draw(new Vector2(700, 550), spriteBatch, filled_block, false);

            //draw the score
            spriteBatch.DrawString(font, $"score: {score}", new Vector2(700, 100), Color.White);

            //draw the special events 
            if (Settings.SpecialEvents)
            {
                spriteBatch.DrawString(font, $"time until next event: {Math.Round(((double)600 - (totalTicks % 600)) / 60)}", new Vector2(600, 750), Color.White);
                spriteBatch.DrawString(font, specialEventName, new Vector2(550, 800), Color.White);
            }
        }
    }
}
