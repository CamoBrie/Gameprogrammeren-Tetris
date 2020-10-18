using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Main
{
    class TetrisGame : Game
    {
        SpriteBatch spriteBatch;
        readonly InputHelper inputHelper;
        GameWorld gameWorld;

        /// <summary>
        /// A static reference to the ContentManager object, used for loading assets.
        /// </summary>
        public static ContentManager ContentManager { get; private set; }


        /// <summary>
        /// A static reference to the width and height of the screen.
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

            // store a static reference to the content manager, so other objects can use it
            ContentManager = Content;

            // set the directory where game assets are located
            Content.RootDirectory = "Content";

            // set the desired window size
            ScreenSize = new Point(900, 900);
            graphics.PreferredBackBufferWidth = ScreenSize.X;
            graphics.PreferredBackBufferHeight = ScreenSize.Y;

            // set the monogame variables
            IsMouseVisible = true;

            // create the input helper object
            inputHelper = new InputHelper();
        }

        protected override void LoadContent()
        {
            // create the spritebatch
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // create the game world
            gameWorld = new GameWorld();

        }

        protected override void Update(GameTime gameTime)
        {
            inputHelper.Update(gameTime);
            gameWorld.HandleInput(gameTime, inputHelper);
            gameWorld.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //start with a black background
            GraphicsDevice.Clear(Color.Black);
            gameWorld.Draw(gameTime, spriteBatch);
        }
    }
}