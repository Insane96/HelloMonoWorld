using System;
using System.Collections.Generic;
using System.Linq;
using Engine;
using Microsoft.Xna.Framework;
using MonoGame.Aseprite.Sprites;
using TowerDefense.Entities.Enemies;

namespace TowerDefense.Entities.Projectiles;

public class Projectile : Entity
{
    public float BaseSpeed { get; protected set; }

    private Vector2 _direction;
    public Vector2 Direction
    {
        get => this._direction;
        set
        {
            this._direction = value;
            this.Sprite.Rotation = (float)Math.Atan2(value.Y, value.X);
        }
    }

    public Projectile(AnimatedSprite sprite, float baseSpeed) : base(sprite)
    {
        this.BaseSpeed = baseSpeed;
    }

    public override void Update()
    {
        base.Update();
        this.Move(this.Direction.Multiply((float)(this.BaseSpeed * Time.DeltaTime)));
        
        if (this.GetX() < -this.GetWidth() || this.GetX() > Graphics.Width + this.GetWidth() || this.GetY() < -this.GetHeight() || this.GetY() > Graphics.Height + this.GetHeight())
            this.MarkForRemoval();
        TryCollide();
    }

    public void TryCollide()
    {
        IEnumerable<Entity> entitiesCollided = this.GetCollisionsOfClass(typeof(AbstractEnemy));
        if (!entitiesCollided.Any()) return;
        this.MarkForRemoval();
        entitiesCollided.First().Hurt(1f);
    }
}