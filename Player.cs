using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HelloMonoWorld;

internal class Player : GameObject
{
    private float movementSpeed = 50f;

    public Player() : base("player", "player")
    {
        this.position = new Vector2(Graphics.screenWidth / 2, Graphics.screenHeight / 2);
    }

    public override void Initialize()
    {
        this.color = Color.Black;
    }

    public override void Update()
    {
        var kstate = Keyboard.GetState();

        if (kstate.IsKeyDown(Keys.W))
        {
            this.position.Y -= (float)(movementSpeed * Time.DeltaTime);
        }

        if (kstate.IsKeyDown(Keys.A))
        {
            this.position.Y += (float)(movementSpeed * Time.DeltaTime);
        }

        if (kstate.IsKeyDown(Keys.S))
        {
            this.position.X -= (float)(movementSpeed * Time.DeltaTime);
        }

        if (kstate.IsKeyDown(Keys.D))
        {
            this.position.X += (float)(movementSpeed * Time.DeltaTime);
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }
}
