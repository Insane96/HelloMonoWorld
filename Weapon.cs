using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelloMonoWorld;

public class Weapon : GameObject
{
    public double attackSpeed;
    public Entity wielder;

    public Weapon(string id, string spriteName, double attackSpeed, Entity wielder) : base(id, spriteName)
    {
        this.attackSpeed = attackSpeed;
        this.wielder = wielder;
        this.origin = new(0, 0);
    }

    public override void Initialize()
    {
            
    }

    public override void Update()
    {

    }

    public void Attack(Vector2 direction)
    {
        this.position = this.wielder.position;
        this.Show();
        if (direction.Equals(Direction.RIGHT.vector))
        {
            this.spriteEffect = SpriteEffects.None;
        }
        else
        {
            this.spriteEffect = SpriteEffects.FlipHorizontally;
        }
    }
}
