using HelloMonoWorld.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HelloMonoWorld.Game.Projectile;

public class BasicProjectile : Entity
{
    private Vector2 direction;
    public Vector2 Direction
    {
        get
        {
            return direction;
        }
        set
        {
            this.direction = value;
            this.Rotation = ((float)(Math.Asin(value.Y / value.Length())));
        }
    }
    public float Damage { get; set; }
    public float Knockback { get; set; }
    public Entity Owner { get; set; }

    public BasicProjectile(string id, Vector2 direction, float damage, float knockback, Entity owner) : base(id)
    {
        this.Direction = direction;
        this.Damage = damage;
        this.Knockback = knockback;
        this.Owner = owner;
    }

    public BasicProjectile(string id, float damage, float knockback) : this(id, default, damage, knockback, null)
    {

    }

    public override void Update()
    {
        base.Update();
        DeltaMovement = Direction.Multiply(MovementSpeed);
        IEnumerable<Entity> entitiesCollided = GetCollisionsOfClass(typeof(AbstractEnemy));

        foreach (Entity entity in entitiesCollided)
        {
            OnEntityHit(entity);
            if (this.RemovalMark)
                break;
        }
    }

    public virtual void OnEntityHit(Entity other)
    {
        other.Hurt(Damage, Knockback);
        Sounds.PlaySoundVariated(this.GetHitSound(), 0.5f, 0.25f);
        Discard();
    }

    protected virtual SoundEffect GetHitSound()
    {
        return Sounds.RockHit;
    }
}