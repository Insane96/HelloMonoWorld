using HelloMonoWorld.Engine;
using HelloMonoWorld.Game.Entity;
using HelloMonoWorld.Game.Projectile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Linq;

namespace HelloMonoWorld.Game.Spell;

public class BasicSpell
{
    public float Damage { get; set; }
    public float Knockback { get; set; }
    public float ProjectileSpeed { get; set; }
    public float Cooldown { get; set; }

    public BasicSpell(float damage, float knockback, float projectileSpeed, float cooldown)
    {
        Damage = damage;
        Knockback = knockback;
        ProjectileSpeed = projectileSpeed;
        Cooldown = cooldown;
    }

    public virtual BasicProjectile GetProjectile()
    {
        return new(Sprites.MagicBulletTexture, this.Damage, this.Knockback);
    }

    public virtual void Cast(AbstractEntity owner, Vector2 direction)
    {
        Sounds.PlaySoundVariated(this.GetCastSound(), 0.5f, 0.25f);
        BasicProjectile projectile = this.GetProjectile();
        projectile.Owner = owner;
        projectile.Direction = direction;
        projectile.MovementSpeed = this.ProjectileSpeed;
        projectile.SetPosition(owner.GetPosition());
        GameObject.Instantiate(projectile);
    }

    protected virtual SoundEffect GetCastSound()
    {
        return Sounds.RockSpell;
    }
}

