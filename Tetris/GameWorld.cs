using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

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
        GameOver
    }

    /// <summary>
    /// The random-number generator of the game.
    /// </summary>
    public static Random Random { get { return random; } }
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
    /// The main grid of the game.
    /// </summary>
    TetrisGrid grid;

    /// <summary>
    /// All of our custom content
    /// </summary>
    Texture2D test;

    public GameWorld()
    {
        // initialize our objects and set the gamestate
        random = new Random();
        grid = new TetrisGrid(Vector2.Zero);
        gameState = GameState.MainMenu;

        // load in custom content
        font = TetrisGame.ContentManager.Load<SpriteFont>("gameFont");
        test = TetrisGame.ContentManager.Load<Texture2D>("block");

        
    }

    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
    }

    public void Update(GameTime gameTime)
    {
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        switch (this.gameState)
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
        spriteBatch.Draw(test, new Vector2(10, 10), Color.White);
        grid.Draw(gameTime, spriteBatch);
        spriteBatch.End();
    }

    public void Reset()
    {
    }

}
