using System;
using System.Collections.Generic;
using HelloMonoWorld.Game.Entity;
using HelloMonoWorld.Game.Entity.Attributes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using MonoEngine;

namespace HelloMonoWorld.Game.Projectile;

public class BasicProjectile : AbstractEntity
{
    private Vector2 _direction;
    public Vector2 Direction
    {
        get => this._direction;
        set
        {
            this._direction = value;
            this.SetRotation((float)Math.Asin(value.Y / value.Length()));
        }
    }
    public AbstractEntity Owner { get; set; }

    public BasicProjectile(AsepriteDocument aseprite, Vector2 direction, float damage, float knockback, AbstractEntity owner) : base(aseprite)
    {
        this.Direction = direction;
        this.GetAttribute(Attributes.Damage).BaseValue = damage;
        this.GetAttribute(Attributes.Knockback).BaseValue = knockback;
        this.Owner = owner;
    }

    public BasicProjectile(AsepriteDocument aseprite, float damage, float knockback) : this(aseprite, default, damage, knockback, null) { }

    public override void Update()
    {
        base.Update();
        this.DeltaMovement = this.Direction.Multiply(this.GetAttributeValue(Attributes.MovementSpeed));
        if (this.GetPosition().X > Graphics.Width + this.GetWidth() + 10f)
            this.Discard();
        IEnumerable<AbstractEntity> entitiesCollided = this.GetCollisionsOfClass(typeof(AbstractEnemy));

        foreach (AbstractEntity entity in entitiesCollided)
        {
            this.OnEntityHit(entity);
            if (this.RemovalMark)
                break;
        }
    }

    public virtual void OnEntityHit(AbstractEntity other)
    {
        other.Hurt(this.Owner, this, this.GetAttributeValue(Attributes.Damage), this.GetAttributeValue(Attributes.Knockback));
        Sounds.PlaySoundVariated(this.GetHitSound(), 0.5f, 0.25f);
        this.Discard();
    }

    protected virtual SoundEffect GetHitSound()
    {
        return Sounds.RockHit;
    }
}