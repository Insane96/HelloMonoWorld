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
    }

    public override void Initialize()
    {
        this.Hide();
    }

    public override void Update()
    {
        FollowWielder();
    }

    public void Attack()
    {
        this.position = this.wielder.position;
        this.Show();
        if (this.wielder.attackDirection.Equals(Direction.RIGHT))
        {
            this.origin = new(0f, 0.5f);
            this.spriteEffect = SpriteEffects.None;
        }
        else
        {
            this.origin = new(1f, 0.5f);
            this.spriteEffect = SpriteEffects.FlipHorizontally;
        }
    }

    private void FollowWielder()
    {
        Vector2 weaponPos = this.wielder.attackDirection == Direction.RIGHT ? this.wielder.LeftHand : this.wielder.RightHand;
        this.position = new Vector2(this.wielder.position.X - (this.wielder.texture.Width * this.wielder.origin.X) + weaponPos.X, this.wielder.position.Y - (this.wielder.texture.Height * this.wielder.origin.Y) + weaponPos.Y);
    }
}
