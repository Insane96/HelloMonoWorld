using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TwinShooter;

public class MainGame : Game
{
    private SpriteBatch _spriteBatch;
    private RenderTarget2D? _target;

    public SpriteFont BaseFont { get; private set; }

    public MainGame()
    {
        Content.RootDirectory = "Content";
        MonoEngine.Init(this, 1280, 720);
        IsMouseVisible = false;
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
        BaseFont = Content.Load<SpriteFont>("fonts/BaseFont");
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _target = new RenderTarget2D(GraphicsDevice, Graphics.Width, Graphics.Height);
    }


    private float scale = 1f;
    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        if (Input.IsKeyPressed(Keys.PageUp))
            scale += 0.1f;
        else if (Input.IsKeyPressed(Keys.PageDown))
            scale -= 0.1f;
        MonoEngine.AddText(BaseFont, $"Hello World: {scale}", new Vector2(10, 10), Color.BlanchedAlmond, Origins.TopLeft, 0f, Vector2.One.Multiply(scale));
        base.Update(gameTime);
        MonoEngine.Update(gameTime);

    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.SetRenderTarget(_target);
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();

        /*if (Options.Debug)
        {
            MonoEngine.AddText(debugFont, $"{player.GetPosition().ToString("N1")}(DeltaMov: {player.DeltaMovement.ToString("N1")}){Environment.NewLine}Bounds: {player.Bounds}{Environment.NewLine}Health: {player.Health}", Vector2.One.Sum(2, 2), Color.White, Vector2.Zero, Color.Black);
        }*/
        //MonoEngine.AddText(debugFont, "Test", Vector2.One.Sum(2, 2), Color.White, Vector2.Zero, Color.Black);

        MonoEngine.DrawGameObjects(_spriteBatch);

        _spriteBatch.End();

        GraphicsDevice.SetRenderTarget(null);

        _spriteBatch.Begin();
        _spriteBatch.Draw(_target, new Rectangle(0, 0, Graphics.ViewportWidth, Graphics.ViewportHeight), Color.White); //TODO Force 16:9
        MonoEngine.DrawStrings(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}