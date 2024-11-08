using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TowerDefense.Entities;
using TowerDefense.Entities.Towers;
using TowerDefense.Registry;

namespace TowerDefense;

public class MainGame : EngineGame
{
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

    public MainGame() : base(1280, 720)
    {
        this.IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        Sprites.LoadTextures(this.Content);
        
        base.Initialize();
        GameObject.Instantiate(StartingPoint);
        GameObject.Instantiate(EndingPoint);
    }

    protected override void LoadContent()
    {
        base.LoadContent();

        debugFont = Content.Load<SpriteFont>("fonts/debug");
    }

    protected override void Update(GameTime gameTime)
    {
        Options.TryToggleDebug();
        Options.TryToggleMute();
        //Options.TryIncreaseFontSize();
        //Options.TryDecreaseFontSize();
        Options.TryFullScreen();

        base.Update(gameTime);

        Time.TimeScale = Input.IsKeyDown(Keys.F10) ? 3f : 1f;
        
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
        base.Draw(gameTime);
    }
}
