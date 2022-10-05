using HelloMonoWorld.Engine;
using HelloMonoWorld.Game.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HelloMonoWorld.Game.Projectile;

public class BasicProjectile : AbstractEntity
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
    public AbstractEntity Owner { get; set; }

    public BasicProjectile(string spriteName, Vector2 direction, float damage, float knockback, AbstractEntity owner) : base(spriteName)
    {
        this.Direction = direction;
        this.Damage = damage;
        this.Knockback = knockback;
        this.Owner = owner;
    }

    public BasicProjectile(string spriteName, float damage, float knockback) : this(spriteName, default, damage, knockback, null)
    {

    }

    public override void Update()
    {
        base.Update();
        DeltaMovement = Direction.Multiply(MovementSpeed);
        IEnumerable<AbstractEntity> entitiesCollided = GetCollisionsOfClass(typeof(AbstractEnemy));

        foreach (AbstractEntity entity in entitiesCollided)
        {
            OnEntityHit(entity);
            if (this.RemovalMark)
                break;
        }
    }

    public virtual void OnEntityHit(AbstractEntity other)
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