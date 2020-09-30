using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Helpers;

namespace Tetris
{
    class TetrisGame : Game
    {
        SpriteBatch spriteBatch;
        InputHelper inputHelper;
        GameWorld gameWorld;

        /// <summary>
        /// A static reference to the ContentManager object, used for loading assets.
        /// </summary>
        public static ContentManager ContentManager { get; private set; }


        /// <summary>
        /// A static reference to the width and height of the application.
        /// </summary>
        public static Point ScreenSize { get; private set; }

        [STAThread]
        static void Main(string[] args)
        {
            TetrisGame game = new TetrisGame();
            game.Run();
        }

        public TetrisGame()
        {
            // initialize the graphics device
            GraphicsDeviceManager graphics = new GraphicsDeviceManager(this);

            // set the graphics settings
            IsMouseVisible = true;

            // store a static reference to the content manager, so other objects can use it
            ContentManager = Content;

            // set the directory where game assets are located
            Content.RootDirectory = "Content";

            // set the desired window size
            ScreenSize = new Point(900, 900);
            graphics.PreferredBackBufferWidth = ScreenSize.X;
            graphics.PreferredBackBufferHeight = ScreenSize.Y;

            // set the window position to be in the middle of the screen
            Window.Position = new Point(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2 - graphics.PreferredBackBufferWidth / 2, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2 - graphics.PreferredBackBufferHeight / 2);

            // create the input helper object
            inputHelper = new InputHelper();
        }

        protected override void LoadContent()
        {
            // create the spritebatch
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // create and reset the game world
            gameWorld = new GameWorld();
            gameWorld.Reset();
        }

        protected override void Update(GameTime gameTime)
        {
            inputHelper.Update(gameTime);
            gameWorld.HandleInput(gameTime, inputHelper);
            gameWorld.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            gameWorld.Draw(gameTime, spriteBatch);
        }
    }
}