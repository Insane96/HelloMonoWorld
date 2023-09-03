using System;
using System.Collections.Generic;
using HelloMonoWorld.Engine;
using HelloMonoWorld.Game.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using MonoGame.Aseprite.Documents;

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
            this.SetRotation((float)Math.Asin(value.Y / value.Length()));
        }
    }
    public float Damage { get; set; }
    public float Knockback { get; set; }
    public AbstractEntity Owner { get; set; }

    public BasicProjectile(AsepriteDocument aseprite, Vector2 direction, float damage, float knockback, AbstractEntity owner) : base(aseprite)
    {
        this.Direction = direction;
        this.Damage = damage;
        this.Knockback = knockback;
        this.Owner = owner;
    }

    public BasicProjectile(AsepriteDocument aseprite, float damage, float knockback) : this(aseprite, default, damage, knockback, null) { }

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
        other.Hurt(this.Owner, this, Damage, Knockback);
        Sounds.PlaySoundVariated(this.GetHitSound(), 0.5f, 0.25f);
        Discard();
    }

    protected virtual SoundEffect GetHitSound()
    {
        return Sounds.RockHit;
    }
}