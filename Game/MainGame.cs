﻿using System;
using HelloMonoWorld.Engine;
using HelloMonoWorld.Game.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HelloMonoWorld.Game;

public class MainGame : Microsoft.Xna.Framework.Game
{
    public static Player player;
    public static Hero hero;
    public static Spawner spawner;

    public static Random random = new();

    private SpriteBatch _spriteBatch;
    private RenderTarget2D target;

    public static SpriteFont debugFont;

    public MainGame()
    {
        Content.RootDirectory = "Content";
        MonoEngine.Init(this, 1280, 720);
        IsMouseVisible = false;
    }

    protected override void Initialize()
    {
        Sprites.LoadTextures(MonoEngine.ContentManager);

        player = new Player();
        GameObject.Instantiate(player);
        hero = new Hero();
        //GameObject.Instantiate(hero);
        spawner = new Spawner(2d, 3d);
        GameObject.Instantiate(spawner);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        target = new RenderTarget2D(GraphicsDevice, Graphics.Width, Graphics.Height);

        debugFont = Content.Load<SpriteFont>("fonts/debug");
        //stickmanAnim.Play("Idle");
        Sounds.LoadContent(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        if (Input.GamePadState.Buttons.Back == ButtonState.Pressed || Input.IsKeyDown(Keys.Escape))
            Exit();
        if (Input.IsKeyPressed(Keys.F7))
            Time.TimeScale = Time.TimeScale == 1f ? 3f : 1f;

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

        if (Options.Debug)
        {
            MonoEngine.DrawText(debugFont, $"{player.GetPosition().ToString("N1")}(DeltaMov: {player.DeltaMovement.ToString("N1")}){Environment.NewLine}Bounds: {player.Bounds}{Environment.NewLine}Health: {player.Health}", Vector2.One.Sum(2, 2), Color.White, Vector2.Zero, Color.Black);
        }

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