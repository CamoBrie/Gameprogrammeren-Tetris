using Helpers;
using Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

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
        static Random Random { get { return random; } }
        static Random random;

        /// <summary>
        /// The main font of the game.
        /// </summary>
        readonly SpriteFont font;

        /// <summary>
        /// The current game state.
        /// </summary>
        GameState gameState;

        /// <summary>
        /// All of our custom content
        /// </summary>
        Texture2D test;
        Effect effect;
        InputHelper InputHelper;

        /// <summary>
        /// All of our menu items
        /// </summary>
        MenuObject MainMenu;
        MenuObject Settings;
        MenuObject Credits;
        MenuObject GameOver;
        MenuObject Controls;

        public GameWorld()
        {
            // initialize our objects and set the gamestate
            random = new Random();
            InputHelper = new InputHelper();

            gameState = GameState.MainMenu;

            // load in custom content
            font = TetrisGame.ContentManager.Load<SpriteFont>("Fonts/gameFont");
            test = TetrisGame.ContentManager.Load<Texture2D>("Sprites/block");

            //Initialize the menu items
            List<MenuItems> temp = new List<MenuItems> {
                new MenuItems("Play Game", Color.White, () => { this.gameState = GameState.Running; return 0; }),
                new MenuItems("Settings", Color.White, () => { this.gameState = GameState.Settings; return 0; }),
                new MenuItems("Credits", Color.White, () => { this.gameState = GameState.Credits; return 0; }),
                new MenuItems("Controls", Color.White, () => { this.gameState = GameState.Controls; return 0; }),
                new MenuItems(" ", Color.White),
                new MenuItems("Controls in menu's: ", Color.Gray),
                new MenuItems("Arrow Keys and Enter", Color.Gray),

            };
            MainMenu = new MenuObject(temp, "Main Menu", 400);

            //Settings
            temp = new List<MenuItems>
            {
                new MenuItems("Back to Main Menu", Color.White, () => { this.gameState = GameState.MainMenu; return 0; }),
                new MenuItems("Main Menu", Color.White, () => { this.gameState = GameState.MainMenu; return 0; }),

            };
            Settings = new MenuObject(temp, "Settings", 300);

            //Credits
            temp = new List<MenuItems>
            {
                new MenuItems("Back to Main Menu", Color.White, () => { this.gameState = GameState.MainMenu; return 0; }),
                new MenuItems("Made by Brian van Meggelen and Sean Hofstra", Color.Gray),
                new MenuItems("time spent: 9999999999999999999 days", Color.Gray),


            };
            Credits = new MenuObject(temp, "Credits", 300);

            //Controls
            temp = new List<MenuItems>
            {
                new MenuItems("Back to Main Menu", Color.White, () => { this.gameState = GameState.MainMenu; return 0; }),
                new MenuItems("Left/Right arrows for moving", Color.Gray),
                new MenuItems("A & D for rotating", Color.Gray),
                new MenuItems("S for saving a shape", Color.Gray),
                new MenuItems("L. Shift for soft-dropping", Color.Gray),
                new MenuItems("Space for hard-dropping", Color.Gray),


            };
            Controls = new MenuObject(temp, "Controls", 300);

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
                case GameState.MainMenu:

                    if (inputHelper.KeyPressed(Keys.Up) && MainMenu.currentItem > 0)
                    {
                        MainMenu.currentItem--;
                    }

                    if (inputHelper.KeyPressed(Keys.Down) && MainMenu.currentItem < MainMenu.getLength() - 1)
                    {
                        MainMenu.currentItem++;
                    }

                    if (inputHelper.KeyPressed(Keys.Enter))
                    {
                        MainMenu.OnAction();
                    }

                    break;
                case GameState.Settings:

                    if (inputHelper.KeyPressed(Keys.Up) && Settings.currentItem > 0)
                    {
                        Settings.currentItem--;
                    }

                    if (inputHelper.KeyPressed(Keys.Down) && Settings.currentItem < Settings.getLength() - 1)
                    {
                        Settings.currentItem++;
                    }

                    if (inputHelper.KeyPressed(Keys.Enter))
                    {
                        Settings.OnAction();
                    }

                    break;
                case GameState.Credits:
                    if (inputHelper.KeyPressed(Keys.Enter))
                    {
                        Credits.OnAction();
                    }
                    break;

                case GameState.Controls:
                    if (inputHelper.KeyPressed(Keys.Enter))
                    {
                        Controls.OnAction();
                    }
                    break;

                case GameState.GameOver:

                    break;
                case GameState.Running:

                    break;

            }

        }

        public void Update(GameTime gameTime)
        {
            this.HandleInput(gameTime, InputHelper);
            //change what we draw based on gamestate
            switch (gameState)
            {
                case GameState.MainMenu:

                    break;
                case GameState.Settings:

                    break;
                case GameState.Credits:

                    break;
                case GameState.GameOver:

                    break;
                case GameState.Running:

                    break;

            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Change our sprite mode to use shaders
            spriteBatch.Begin();

            //change what we draw based on gamestate
            switch (gameState)
            {
                case GameState.MainMenu:
                    MainMenu.Draw(spriteBatch, font);
                    break;

                case GameState.Settings:
                    Settings.Draw(spriteBatch, font);
                    break;

                case GameState.Credits:
                    Credits.Draw(spriteBatch, font);
                    break;

                case GameState.Controls:
                    Controls.Draw(spriteBatch, font);
                    break;

                case GameState.GameOver:

                    break;
                case GameState.Running:

                    break;

            }
            spriteBatch.End();
        }

        public void Reset()
        {
        }

    }
}