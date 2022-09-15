﻿using HelloMonoWorld.Engine;
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

    private SpriteFont debugFont;

    public MainGame()
    {
        Graphics.graphics = new(this)
        {
            PreferredBackBufferWidth = 1280,
            PreferredBackBufferHeight = 720
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

        debugFont = Content.Load<SpriteFont>("debug");
        Sounds.LoadContent(this.Content);
    }

    protected override void Update(GameTime gameTime)
    {
        Time.UpdateDeltaTime(gameTime);

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        Options.TryToggleDebug(Keyboard.GetState());

        Engine.Engine.UpdateGameObjects();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        if (Options.Debug)
        {
            _spriteBatch.DrawString(debugFont, $"player{{pos: {player.position.ToString("N1")}, deltaMov: {player.deltaMovement.ToString("N1")}, rect: {player.Bounds}, health: {player.health}, immunity: {player.immunityTime}}}{Environment.NewLine}", Vector2.One, Color.OrangeRed);
        }

        Engine.Engine.DrawGameObjects(_spriteBatch);

        _spriteBatch.End();


        base.Draw(gameTime);
    }
}