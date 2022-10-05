﻿using HelloMonoWorld.Engine;
using HelloMonoWorld.Game.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Aseprite.Documents;
using MonoGame.Aseprite.Graphics;
using System;

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

    public static AsepriteDocument stickmanAnimated;
    public static AnimatedSprite stickmanAnim;

    public MainGame()
    {
        Content.RootDirectory = "Content";
        MonoEngine.Init(this, 1280, 720);
        IsMouseVisible = false;
    }

    protected override void Initialize()
    {
        player = new Player();
        GameObject.Instantiate(player);
        hero = new Hero();
        GameObject.Instantiate(hero);
        spawner = new(2d, 3d);
        GameObject.Instantiate(spawner);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        target = new(GraphicsDevice, Graphics.Width, Graphics.Height);

        debugFont = Content.Load<SpriteFont>("fonts/debug");
        stickmanAnimated = Content.Load<AsepriteDocument>("sprites/stickman_anim");
        stickmanAnim = new AnimatedSprite(stickmanAnimated);
        //stickmanAnim.Play("Idle");
        Sounds.LoadContent(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        if (Keyboard.GetState().IsKeyDown(Keys.F7))
            Time.TimeScale = 3f;

        Options.TryToggleDebug(Keyboard.GetState());
        Options.TryToggleMute(Keyboard.GetState());
        Options.TryIncreaseFontSize(Keyboard.GetState());
        Options.TryDecreaseFontSize(Keyboard.GetState());
        Options.TryFullScreen(Keyboard.GetState());
        Options.oldKeyBoardState = Keyboard.GetState();

        MonoEngine.UpdateGameObjects(gameTime);

        stickmanAnim.Update((float)Time.DeltaTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.SetRenderTarget(target);
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();

        if (Options.Debug)
        {
            MonoEngine.DrawText(debugFont, $"{player.Position.ToString("N1")}(DeltaMov: {player.DeltaMovement.ToString("N1")}){Environment.NewLine}Bounds: {player.Bounds}{Environment.NewLine}Health: {player.Health}", Vector2.One.Sum(2, 2), Color.White, Vector2.Zero, Color.Black);
        }

        MonoEngine.DrawGameObjects(_spriteBatch);
        stickmanAnim.Render(_spriteBatch);

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