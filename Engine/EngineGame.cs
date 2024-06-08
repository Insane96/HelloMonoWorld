using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine;

/// <summary>
/// Extend this instead of <see cref="Game"/>
/// </summary>
public abstract class EngineGame : Game
{
    private SpriteBatch _spriteBatch = null!;
    private RenderTarget2D? _target;
    
    private List<UiText> StringsToDraw { get; } = [];

    protected EngineGame(int winWidth, int winHeight)
    {
        Content.RootDirectory = "Content";
        Graphics.Init(this, winWidth, winHeight);

        Utils.Init();
        Input.Game = this;
        GameObject.Game = this;
        Graphics.GraphicsDeviceManager.GraphicsProfile = GraphicsProfile.HiDef;
        Graphics.GraphicsDeviceManager.PreparingDeviceSettings += (_, args) =>
        {
            Graphics.GraphicsDeviceManager.PreferMultiSampling = true;
            var rasterizerState = new RasterizerState
            {
                MultiSampleAntiAlias = true,
            };

            GraphicsDevice.RasterizerState = rasterizerState;
            args.GraphicsDeviceInformation.PresentationParameters.MultiSampleCount = 2;
            Graphics.GraphicsDeviceManager.ApplyChanges();
        };
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _target = new RenderTarget2D(GraphicsDevice, Graphics.Width, Graphics.Height);
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Time.UpdateDeltaTime(gameTime);
        Input.Update();
        GameObject.UpdateGameObjects();
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.SetRenderTarget(this._target);
        GraphicsDevice.Clear(Color.CornflowerBlue);
        this._spriteBatch.Begin();

        /*if (Options.Debug)
        {
            EngineGame.AddText(debugFont, $"{player.GetPosition().ToString("N1")}(DeltaMov: {player.DeltaMovement.ToString("N1")}){Environment.NewLine}Bounds: {player.Bounds}{Environment.NewLine}Health: {player.Health}", Vector2.One.Sum(2, 2), Color.White, Vector2.Zero, Color.Black);
        }*/
        //EngineGame.AddText(debugFont, "Test", Vector2.One.Sum(2, 2), Color.White, Vector2.Zero, Color.Black);

        GameObject.DrawGameObjects(this._spriteBatch);

        this._spriteBatch.End();

        GraphicsDevice.SetRenderTarget(null);

        this._spriteBatch.Begin();
        this._spriteBatch.Draw(_target, new Rectangle(0, 0, Graphics.ViewportWidth, Graphics.ViewportHeight), Color.White); //TODO Force 16:9
        this.DrawStrings();
        this._spriteBatch.End();

        base.Draw(gameTime);
    }

    public void DrawStrings()
    {
        foreach (UiText text in this.StringsToDraw)
        {
            Vector2 textSize = text.SpriteFont.MeasureString(text.Text);
            Vector2 position = text.Position.Multiply(Graphics.ScaledRatio).Sum(-textSize.X * text.Origin.X, -textSize.Y * text.Origin.Y);
            if (text.ShadowColor.HasValue)
                this._spriteBatch.DrawString(text.SpriteFont, text.Text, position.Sum(1, 1), text.ShadowColor.Value, text.Rotation, Vector2.Zero, text.Scale, SpriteEffects.None, 0f);
            this._spriteBatch.DrawString(text.SpriteFont, text.Text, position, text.Color, text.Rotation, Vector2.Zero, text.Scale, SpriteEffects.None, 0f);
        }
        this.StringsToDraw.Clear();
    }

    public void AddText(SpriteFont font, string text, Vector2 position, Color color, Vector2 origin, float rotation, Vector2 scale, Color? shadowColor = null)
    {
        this.StringsToDraw.Add(new UiText
        {
            Text = text,
            SpriteFont = font,
            Position = position,
            Rotation = rotation,
            Scale = scale,
            Color = color,
            Origin = origin,
            ShadowColor = shadowColor
        });
    }
}