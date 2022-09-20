using HelloMonoWorld.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace HelloMonoWorld;

public class MainGame : Game
{
    public static Player player;
    public static Spawner spawner;

    public static Random random = new();

    private SpriteBatch _spriteBatch;
    private RenderTarget2D target;

    public static SpriteFont debugFont;

    public MainGame()
    {
        Graphics.graphics = new(this)
        {
            PreferredBackBufferWidth = 1920,
            PreferredBackBufferHeight = 1080
        };
        Graphics.graphics.ApplyChanges();

        Content.RootDirectory = "Content";
        Engine.Engine.Init(this.Content);
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        player = new Player();
        Engine.Engine.Instantiate(player);
        spawner = new(2.5d, 5d);
        Engine.Engine.Instantiate(spawner);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        target = new(GraphicsDevice, Graphics.UnscaledWidth, Graphics.UnscaledHeight);

        debugFont = Content.Load<SpriteFont>("debug");
        Sounds.LoadContent(this.Content);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        Options.TryToggleDebug(Keyboard.GetState());
        Options.TryToggleMute(Keyboard.GetState());
        Options.TryIncreaseFontSize(Keyboard.GetState());
        Options.TryDecreaseFontSize(Keyboard.GetState());

        Engine.Engine.UpdateGameObjects(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.SetRenderTarget(target);
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();

        if (Options.Debug)
        {
            Engine.Engine.DrawText(debugFont, $"{player.position.ToString("N1")}(DeltaMov: {player.deltaMovement.ToString("N1")}){Environment.NewLine}Bounds: {player.Bounds}{Environment.NewLine}Health: {player.health}{Environment.NewLine}Immunity: {player.immunityTime}", Vector2.One.Sum(2, 2), Color.White, Vector2.Zero, Color.Black);
        }

        Engine.Engine.DrawGameObjects(_spriteBatch);

        _spriteBatch.End();

        GraphicsDevice.SetRenderTarget(null);

        _spriteBatch.Begin();
        //_spriteBatch.Draw(target, Vector2.Zero, Color.White);
        _spriteBatch.Draw(target, new Rectangle(0, 0, Graphics.ScreenWidth, Graphics.ScreenHeight), Color.White); //Change to force 16:9
        Engine.Engine.DrawStrings(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}