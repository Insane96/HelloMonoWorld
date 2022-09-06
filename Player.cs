using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HelloMonoWorld;

public class Player : Entity 
{
    public double attackTime = 0d;
    public Weapon weapon;

    public static readonly Vector2 LeftHand = new();

    public Player() : base("player", "stickman")
    {
        this.position = new Vector2(Graphics.screenWidth / 2, Graphics.screenHeight / 2);
        this.weapon = new("sword", "sword", 0.5d, this);
        MainGame.gameObjects.Add(this.weapon);
    }

    public override void Initialize()
    {
        this.color = Color.Black;
    }

    public override void Update()
    {
        var kstate = Keyboard.GetState();

        this.deltaMovement = Vector2.Zero;
        foreach (Direction direction in Direction.directions)
        {
            if (kstate.IsKeyDown(direction.key))
            {
                this.deltaMovement += direction.vector;
            }
        }

        if (kstate.IsKeyDown(Keys.Left))
        {
            this.attackTime = this.weapon.attackSpeed;
            this.weapon.Attack(Direction.LEFT.vector);
        }
        else if (kstate.IsKeyDown(Keys.Right))
        {
            this.attackTime = this.weapon.attackSpeed;
            this.weapon.Attack(Direction.RIGHT.vector);
        }
        if (this.attackTime > 0d)
        {
            this.attackTime -= Time.DeltaTime;
        }

        base.Update();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }
}
