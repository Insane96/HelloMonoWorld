using System;
using System.Linq;
using Engine;
using Microsoft.Xna.Framework;
using MonoGame.Aseprite.Sprites;
using TowerDefense.Entities.Enemies;
using TowerDefense.Entities.Towers;

namespace TowerDefense.Entities.Projectiles;

public class Projectile : OwnableEntity
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

    public Projectile(AnimatedSprite sprite, Tower owner, float baseSpeed) : base(sprite, owner)
    {
        this.BaseSpeed = baseSpeed;
        this.ShouldUpdateBounds = false;
    }

    public override void Update()
    {
        base.Update();
        this.Move(this.Direction.Multiply((float)(this.BaseSpeed * Time.DeltaTime)));
        
        if (this.GetX() < -this.GetWidth() || this.GetX() > Graphics.Width + this.GetWidth() || this.GetY() < -this.GetHeight() || this.GetY() > Graphics.Height + this.GetHeight())
            this.MarkForRemoval();
        TryHit();
    }

    public void TryHit()
    {
        Vector2 direction = new((float)Math.Cos(this.Sprite.Rotation), (float)Math.Sin(this.Sprite.Rotation));
        direction.Normalize();
        foreach (var enemy in GetUpdatableGameObjects().OfType<AbstractEnemy>().ToList())
        {
            if (!Utils.Intersects(this.Position, this.Position + direction.Multiply(this.GetWidth() / 2f), enemy.Bounds)) 
                continue;
            this.MarkForRemoval();
            enemy.Hurt(1f);
            this.Owner.OnHitEnemy(enemy);
        }
    }
}