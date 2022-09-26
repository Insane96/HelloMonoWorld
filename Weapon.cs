using HelloMonoWorld.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace HelloMonoWorld;

public class Weapon
{
    public string id;
    public string spriteName;
    public float damage;
    public float knockback;
    public float projectileSpeed;
    public float attackSpeed;
    public Entity wielder;

    public Weapon(string id, string spriteName, float damage, float projectileSpeed, float knockback, float attackSpeed, Entity wielder)
    {
        this.id = id;
        this.spriteName = spriteName;
        this.damage = damage;
        this.projectileSpeed = projectileSpeed;
        this.knockback = knockback;
        this.attackSpeed = attackSpeed;
        this.wielder = wielder;
    }

    public virtual void Attack()
    {
        this.GetAttackSound().Play(0.5f * Options.Volume, Mth.NextFloat(MainGame.random, -0.25f, 0.25f), 0f);
        Projectile projectile = new("projectile", "magic_bullet", this.damage, this.knockback, this.wielder)
        {
            position = new Vector2(this.wielder.position.X - (this.wielder.texture.Width * this.wielder.origin.X) + this.wielder.LeftHand.X, this.wielder.position.Y - (this.wielder.texture.Height * this.wielder.origin.Y) + this.wielder.LeftHand.Y),
            movementSpeed = (float)this.projectileSpeed
        };
        Engine.Engine.Instantiate(projectile);
    }

    private SoundEffect GetAttackSound()
    {
        return Sounds.SwordSwing;
    }
}
