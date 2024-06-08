using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TowerDefense.Entities;
using TowerDefense.Entities.Towers;
using TowerDefense.Registry;

namespace TowerDefense;

public class MainGame : Game
{
    private SpriteBatch? _spriteBatch;
    private RenderTarget2D? _target;

    public static SpriteFont debugFont;

    public static bool IsBuildingTower = true;

    public static StartingPoint StartingPoint = new()
    {
        Position = new Vector2(-50, -50)
    };

    public static EndingPoint EndingPoint = new(Sprites.GetAnimatedSprite(Sprites.DeathTower, "idle"))
    {
        Position = new Vector2(1000, 500)
    };

    public MainGame()
    {
        Content.RootDirectory = "Content";
        EngineGame.Init(this, 1280, 720);
        this.IsMouseVisible = true;
        Input.Game = this;
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
        Sprites.LoadTextures(EngineGame.ContentManager);
        
        base.Initialize();
        GameObject.Instantiate(StartingPoint);
        GameObject.Instantiate(EndingPoint);
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _target = new RenderTarget2D(GraphicsDevice, Graphics.Width, Graphics.Height);

        debugFont = Content.Load<SpriteFont>("fonts/debug");
    }

    protected override void Update(GameTime gameTime)
    {
        Options.TryToggleDebug();
        Options.TryToggleMute();
        Options.TryIncreaseFontSize();
        Options.TryDecreaseFontSize();
        Options.TryFullScreen();

        EngineGame.Update(gameTime);

        if (Input.IsKeyDown(Keys.F10))
            Time.TimeScale = 3f;
        else
            Time.TimeScale = 1f;
        
        if (Input.GamePadState.Buttons.Back == ButtonState.Pressed || Input.IsKeyDown(Keys.Escape))
            Exit();
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        if (IsBuildingTower && Input.IsLeftClickPressed())
        {
            Tower tower = !Input.IsKeyDown(Keys.LeftShift) ? new Tower(Sprites.CrossbowTower) : new LaserTower();

            tower.Position = new Vector2(Input.MouseState.X, Input.MouseState.Y);
            tower.UpdateBounds();
            if (tower.GetCollisionsOfClass(typeof(Tower)).Count == 0)
                GameObject.Instantiate(tower);
        }
        
        if (Input.IsRightClickPressed())
        {
            /*if (!Input.IsKeyDown(Keys.LeftShift))
                GameObject.Instantiate(new AbstractEnemy(Sprites.GetAnimatedSprite(Sprites.Zombie, "idle"), 1f, 5f)
                {
                    Position = new Vector2(Input.MouseState.X, Input.MouseState.Y)
                });
            else 
                GameObject.Instantiate(new AbstractEnemy(Sprites.GetAnimatedSprite(Sprites.Zombie, "idle"), 1f, 100f)
                {
                    Position = new Vector2(Input.MouseState.X, Input.MouseState.Y)
                });*/
            //AbstractEnemy abstractEnemy = EnemiesRegistry.CreateFromId("zombie");
        }

        if (Input.IsKeyPressed(Keys.B))
            IsBuildingTower = !IsBuildingTower;
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.SetRenderTarget(_target);
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();

        /*if (Options.Debug)
        {
            EngineGame.DrawText(debugFont, $"{player.GetPosition().ToString("N1")}(DeltaMov: {player.DeltaMovement.ToString("N1")}){Environment.NewLine}Bounds: {player.Bounds}{Environment.NewLine}Health: {player.Health}", Vector2.One.Sum(2, 2), Color.White, Vector2.Zero, Color.Black);
        }*/
        //EngineGame.DrawText(debugFont, "Test", Vector2.One.Sum(2, 2), Color.White, Vector2.Zero, Color.Black);

        EngineGame.DrawGameObjects(_spriteBatch);

        _spriteBatch.End();

        GraphicsDevice.SetRenderTarget(null);

        _spriteBatch.Begin();
        //_spriteBatch.Draw(target, Vector2.Zero, Color.White);
        _spriteBatch.Draw(_target, new Rectangle(0, 0, Graphics.ViewportWidth, Graphics.ViewportHeight), Color.White); //TODO Force 16:9
        EngineGame.DrawStrings(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
