﻿using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TowerDefense.Entities.Enemies;
using TowerDefense.Entities.Towers;
using TowerDefense.Registry;

namespace TowerDefense;

public class MainGame : Game
{
    private SpriteBatch _spriteBatch;
    private RenderTarget2D target;

    public static SpriteFont debugFont;

    public MainGame()
    {
        Content.RootDirectory = "Content";
        MonoEngine.Init(this, 1280, 720);
        this.IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        Sprites.LoadTextures(MonoEngine.ContentManager);
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        target = new RenderTarget2D(GraphicsDevice, Graphics.Width, Graphics.Height);

        debugFont = Content.Load<SpriteFont>("fonts/debug");
    }

    protected override void Update(GameTime gameTime)
    {
        if (Input.GamePadState.Buttons.Back == ButtonState.Pressed || Input.IsKeyDown(Keys.Escape))
            Exit();
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        if (Input.IsLeftClickPressed())
            GameObject.Instantiate(new Tower(Sprites.GetAnimatedSprite(Sprites.CrossbowTower, "idle"), 1f, 1f, 200f)
            {
                Position = new Vector2(Input.MouseState.X, Input.MouseState.Y)
            });
        
        if (Input.IsRightClickPressed())
            GameObject.Instantiate(new AbstractEnemy(Sprites.GetAnimatedSprite(Sprites.Zombie, "idle"), 1f)
            {
                Position = new Vector2(Input.MouseState.X, Input.MouseState.Y)
            });

        Options.TryToggleDebug();
        Options.TryToggleMute();
        Options.TryIncreaseFontSize();
        Options.TryDecreaseFontSize();
        Options.TryFullScreen();

        MonoEngine.Update(gameTime);
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.SetRenderTarget(target);
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();

        /*if (Options.Debug)
        {
            MonoEngine.DrawText(debugFont, $"{player.GetPosition().ToString("N1")}(DeltaMov: {player.DeltaMovement.ToString("N1")}){Environment.NewLine}Bounds: {player.Bounds}{Environment.NewLine}Health: {player.Health}", Vector2.One.Sum(2, 2), Color.White, Vector2.Zero, Color.Black);
        }*/
        //MonoEngine.DrawText(debugFont, "Test", Vector2.One.Sum(2, 2), Color.White, Vector2.Zero, Color.Black);

        MonoEngine.DrawGameObjects(_spriteBatch);

        _spriteBatch.End();

        GraphicsDevice.SetRenderTarget(null);

        _spriteBatch.Begin();
        //_spriteBatch.Draw(target, Vector2.Zero, Color.White);
        _spriteBatch.Draw(target, new Rectangle(0, 0, Graphics.ViewportWidth, Graphics.ViewportHeight), Color.White); //TODO Force 16:9
        MonoEngine.DrawStrings(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}