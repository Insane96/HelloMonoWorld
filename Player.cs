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

    }

    public override void Update()
    {
        var kstate = Keyboard.GetState();

        if (kstate.IsKeyDown(Keys.Up))
        {
            this.position.Y -= (float)(movementSpeed * Time.DeltaTime);
        }

        if (kstate.IsKeyDown(Keys.Down))
        {
            this.position.Y += (float)(movementSpeed * Time.DeltaTime);
        }

        if (kstate.IsKeyDown(Keys.Left))
        {
            this.position.X -= (float)(movementSpeed * Time.DeltaTime);
        }

        if (kstate.IsKeyDown(Keys.Right))
        {
            this.position.X += (float)(movementSpeed * Time.DeltaTime);
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.Draw(
            this.texture,
            this.position,
            null,
            Color.Black,
            0f,
            new Vector2(this.texture.Width / 2, this.texture.Height / 2),
            Vector2.One,
            SpriteEffects.None,
            0f
        );
        spriteBatch.End();
    }
}
