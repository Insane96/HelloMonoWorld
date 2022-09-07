using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace HelloMonoWorld;

public class MainGame : Game
{
    public static Player player;
    public static Enemy enemy;

    public static List<GameObject> gameObjects = new();

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private SpriteFont debugFont;

    public MainGame()
    {
        _graphics = new(this)
        {
            PreferredBackBufferWidth = 1280,
            PreferredBackBufferHeight = 720
        };
        _graphics.ApplyChanges();

        Graphics.screenWidth = _graphics.GraphicsDevice.Viewport.Width;
        Graphics.screenHeight = _graphics.GraphicsDevice.Viewport.Height;

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        player = new Player();
        gameObjects.Add(player);
        gameObjects.Add(player.weapon);
        enemy = new("test_enemy", "stickman");
        gameObjects.Add(enemy);

        base.Initialize();

        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.Initialize();
        }
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.LoadContent(this.Content);
        }
        debugFont = Content.Load<SpriteFont>("debug");
    }

    protected override void Update(GameTime gameTime)
    {
        Time.UpdateDeltaTime(gameTime);

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.Enabled)
                gameObject.Update();
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.DrawString(debugFont, $"player{{pos: {player.position.ToString("N1")}, deltaMov: {player.deltaMovement.ToString("N1")}, rect: {player.texture.Bounds}}}{Environment.NewLine}" +
            $"enemy{{pos: {enemy.position.ToString("N1")}, deltaMov: {enemy.deltaMovement.ToString("N1")}}}", Vector2.One, Color.OrangeRed);

        foreach(GameObject gameObject in gameObjects)
        {
            if (gameObject.Visible)
                gameObject.Draw(_spriteBatch);
        }

        _spriteBatch.End();


        base.Draw(gameTime);
    }
}