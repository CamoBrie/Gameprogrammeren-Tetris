using Helpers;
using Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Tetris.Tetris;

namespace Main
{
    /// <summary>
    /// A class for representing the game world.
    /// This contains the grid, the falling block, and everything else that the player can see/do.
    /// </summary>
    class GameWorld
    {
        /// <summary>
        /// An enum for the different game states that the game can have.
        /// </summary>
        enum GameState
        {
            MainMenu,
            Running,
            Settings,
            Credits,
            Controls,
            GameOver
        }

        /// <summary>
        /// The random-number generator of the game.
        /// </summary>
        //static Random Random { get { return random; } }
        //static Random random;

        /// <summary>
        /// The main font of the game.
        /// </summary>
        readonly SpriteFont font;
        readonly Texture2D mainMenuImage;

        /// <summary>
        /// The current game state.
        /// </summary>
        GameState gameState;

        /// <summary>
        /// All of our custom content
        /// </summary>
        readonly GameManager game;

        /// <summary>
        /// All of our menu items
        /// </summary>
        readonly MenuObject MainMenu;
        readonly MenuObject SettingsMenu;
        readonly MenuObject Credits;
        readonly MenuObject GameOver;
        readonly MenuObject Controls;

        public GameWorld()
        {
            // initialize our objects and set the gamestate


            gameState = GameState.MainMenu;

            // load in custom content
            font = TetrisGame.ContentManager.Load<SpriteFont>("Fonts/ComicSans");
            mainMenuImage = TetrisGame.ContentManager.Load<Texture2D>("Sprites/mainmenu");
            Texture2D empty_block = TetrisGame.ContentManager.Load<Texture2D>("Sprites/emptyblock");
            Texture2D filled_block = TetrisGame.ContentManager.Load<Texture2D>("Sprites/filledblock");

            // initialize the game with it base point at 20,20
            game = new GameManager(new Vector2(20, 20), empty_block, filled_block);

            //
            // Initialize the menu items
            //

            // Main Menu
            List<MenuItems> temp = new List<MenuItems> {
                new MenuItems("Play Game", Color.White, () => { this.gameState = GameState.Running; game.Initialize(); return 0; }),
                new MenuItems("Settings", Color.White, () => { this.gameState = GameState.Settings; return 0; }),
                new MenuItems("Credits", Color.White, () => { this.gameState = GameState.Credits; return 0; }),
                new MenuItems("Controls", Color.White, () => { this.gameState = GameState.Controls; return 0; }),
                new MenuItems(" ", Color.White),
                new MenuItems("Controls in menu's: ", Color.Gray),
                new MenuItems("Arrow Keys and Enter", Color.Gray),

            };
            MainMenu = new MenuObject(temp, "Main Menu", 400);

            // Settings
            temp = new List<MenuItems>
            {
                new MenuItems("Starting Difficulty", Color.Gray),
                new MenuItems("Grid Width", Color.Gray),
                new MenuItems("Grid Height", Color.Gray),
                //new MenuItems("Animations (not implemented due to time)", Color.Gray),
                new MenuItems("Special Events", Color.Gray),
                new MenuItems("Hidden Mode", Color.Gray),
                new MenuItems("Back to Main Menu", Color.White, () => { this.gameState = GameState.MainMenu; return 0; }),

            };
            SettingsMenu = new MenuObject(temp, "Settings", 300);

            // Credits
            temp = new List<MenuItems>
            {
                new MenuItems("Back to Main Menu", Color.White, () => { this.gameState = GameState.MainMenu; return 0; }),
                new MenuItems("Made by Brian van Meggelen and Sean Hofstra", Color.Gray),
                new MenuItems("time spent: 9999999999999999999 days", Color.Gray),


            };
            Credits = new MenuObject(temp, "Credits", 300);

            // Controls
            temp = new List<MenuItems>
            {
                new MenuItems("Back to Main Menu", Color.White, () => { this.gameState = GameState.MainMenu; return 0; }),
                new MenuItems("Left/Right arrows for moving", Color.Gray),
                new MenuItems("A & D for rotating", Color.Gray),
                new MenuItems("S for saving a shape", Color.Gray),
                new MenuItems("L. Shift for soft-dropping", Color.Gray),


            };
            Controls = new MenuObject(temp, "Controls", 300);

            // Game Over
            temp = new List<MenuItems>
            {
                new MenuItems("Back to Main Menu", Color.White, () => { this.gameState = GameState.MainMenu; return 0; }),
                new MenuItems(" ", Color.White),
                new MenuItems("Game Over", Color.Gray),
                new MenuItems($"Your score is: ", Color.Gray),



            };
            GameOver = new MenuObject(temp, "GameOver", 300);

            //
            // End of menu items
            //
        }
        public void HandleInput(GameTime gameTime, InputHelper inputHelper)
        {
            //Exit on escape
            if (inputHelper.KeyPressed(Keys.Escape))
            {
                System.Environment.Exit(1);
            }

            //change what we do based on gamestate
            switch (gameState)
            {
                // default
                default:
                    gameState = GameState.MainMenu;
                    break;

                // Menu - main menu
                case GameState.MainMenu:

                    if (inputHelper.KeyPressed(Keys.Up) && MainMenu.currentItem > 0)
                    {
                        MainMenu.currentItem--;
                    }

                    if (inputHelper.KeyPressed(Keys.Down) && MainMenu.currentItem < 3)
                    {
                        MainMenu.currentItem++;
                    }

                    if (inputHelper.KeyPressed(Keys.Enter))
                    {
                        MainMenu.OnAction();
                    }

                    break;

                // Menu - settings
                case GameState.Settings:

                    if (inputHelper.KeyPressed(Keys.Up) && SettingsMenu.currentItem > 0)
                    {
                        SettingsMenu.currentItem--;
                    }

                    if (inputHelper.KeyPressed(Keys.Down) && SettingsMenu.currentItem < SettingsMenu.GetLength() - 1)
                    {
                        SettingsMenu.currentItem++;
                    }

                    if (inputHelper.KeyPressed(Keys.Right))
                    {
                        Settings.ChangeSetting(true, Settings.GetSetting(SettingsMenu.currentItem));
                    }

                    if (inputHelper.KeyPressed(Keys.Left))
                    {
                        Settings.ChangeSetting(false, Settings.GetSetting(SettingsMenu.currentItem));
                    }

                    if (inputHelper.KeyPressed(Keys.Enter))
                    {
                        SettingsMenu.OnAction();
                    }

                    break;

                // Menu - credits
                case GameState.Credits:
                    if (inputHelper.KeyPressed(Keys.Enter))
                    {
                        Credits.OnAction();
                    }
                    break;

                // Menu - controls
                case GameState.Controls:
                    if (inputHelper.KeyPressed(Keys.Enter))
                    {
                        Controls.OnAction();
                    }
                    break;

                // Menu - game over
                case GameState.GameOver:
                    if (inputHelper.KeyPressed(Keys.Enter))
                    {
                        GameOver.OnAction();
                    }
                    break;

                // The game
                case GameState.Running:
                    game.HandleInput(gameTime, inputHelper);
                    break;

            }

        }

        public void Update(GameTime gameTime)
        {

            //change what we update based on gamestate
            switch (gameState)
            {
                // don't have to update anything
                case GameState.MainMenu:
                case GameState.Settings:
                case GameState.Credits:
                case GameState.GameOver:
                    //change the text to the score
                    GameOver.RenameItem(3, $"Your score is: {game.score}");
                    break;

                // update game
                case GameState.Running:
                    game.Update(gameTime);
                    if (game.gameOver)
                    {
                        gameState = GameState.GameOver;
                    }
                    break;

            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // start the drawing of the spritebatch
            spriteBatch.Begin();

            // change what we draw based on gamestate
            switch (gameState)
            {
                // draw the menu
                case GameState.MainMenu:
                    // draw the title screen image
                    spriteBatch.Draw(mainMenuImage, new Vector2(250, 100), Color.White);
                    MainMenu.Draw(spriteBatch, font);
                    break;

                // draw the menu
                case GameState.Settings:
                    SettingsMenu.Draw(spriteBatch, font);
                    break;

                // draw the menu
                case GameState.Credits:
                    Credits.Draw(spriteBatch, font);
                    break;

                // draw the menu
                case GameState.Controls:
                    Controls.Draw(spriteBatch, font);
                    break;

                // draw the menu
                case GameState.GameOver:
                    GameOver.Draw(spriteBatch, font);
                    break;

                // draw the game
                case GameState.Running:
                    game.Draw(spriteBatch, font);
                    break;

            }
            spriteBatch.End();
        }
    }
}