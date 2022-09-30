using HelloMonoWorld.Engine;
using HelloMonoWorld.Game.Projectile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Linq;

namespace HelloMonoWorld.Game.Spell;

public class BasicSpell
{
    public string Id { get; set; }
    public float Damage { get; set; }
    public float Knockback { get; set; }
    public float ProjectileSpeed { get; set; }
    public float Cooldown { get; set; }

    public BasicSpell(string id, float damage, float knockback, float projectileSpeed, float cooldown)
    {
        Id = id;
        Damage = damage;
        Knockback = knockback;
        ProjectileSpeed = projectileSpeed;
        Cooldown = cooldown;
    }

    public virtual BasicProjectile GetProjectile()
    {
        return new(this.Id, this.Damage, this.Knockback);
    }

    public virtual void Cast(Entity owner, Vector2 direction)
    {
        this.GetCastSound().Play(0.5f * Options.Volume, Mth.NextFloat(MainGame.random, -0.25f, 0.25f), 0f);
        BasicProjectile projectile = this.GetProjectile();
        projectile.Owner = owner;
        projectile.Direction = direction;
        projectile.MovementSpeed = this.ProjectileSpeed;
        projectile.Position = owner.Position;
        GameObject.Instantiate(projectile);
    }

    /*public virtual void Attack()
    {
        this.GetAttackSound().Play(0.5f * Options.Volume, Mth.NextFloat(MainGame.random, -0.25f, 0.25f), 0f);
        Projectile projectile = new("projectile", "magic_bullet", Direction.RIGHT.vector, this.Damage, this.Knockback, this.wielder)
        {
            Position = new Vector2(this.wielder.Position.X - (this.wielder.Texture.Width * this.wielder.Origin.X) + this.wielder.LeftHand.X, this.wielder.Position.Y - (this.wielder.Texture.Height * this.wielder.Origin.Y) + this.wielder.LeftHand.Y),
            MovementSpeed = projectileSpeed
        };
        GameObject.Instantiate(projectile);
    }

    public virtual void AttackNearest()
    {
        GameObject nearestEnemy = GameObject.GetUpdatableGameObjects().OrderBy(g => g.Position.X).FirstOrDefault(g => g is AbstractEnemy enemy);
        if (nearestEnemy == null)
            return;

        this.GetAttackSound().Play(0.5f * Options.Volume, Mth.NextFloat(MainGame.random, -0.25f, 0.25f), 0f);
        Projectile projectile = new("projectile", "magic_bullet", Vector2.Normalize(nearestEnemy.Position - this.wielder.Position.Sum(this.wielder.LeftHand.X)), this.damage, this.knockback, this.wielder)
        {
            Position = new Vector2(this.wielder.Position.X - (this.wielder.Texture.Width * this.wielder.Origin.X) + this.wielder.LeftHand.X, this.wielder.Position.Y - (this.wielder.Texture.Height * this.wielder.Origin.Y) + this.wielder.LeftHand.Y),
            MovementSpeed = projectileSpeed
        };
        GameObject.Instantiate(projectile);
    }*/

    private SoundEffect GetCastSound()
    {
        return Sounds.SwordSwing;
    }
}

