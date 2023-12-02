using System;
using System.Linq;
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Aseprite.Sprites;
using TowerDefense.Entities.Enemies;
using TowerDefense.Entities.Projectiles;
using TowerDefense.Registry;

namespace TowerDefense.Entities.Towers;

public class Tower : Entity
{
    public float BaseAttackSpeed { get; protected set; }
    public float BaseAttackDamage { get; protected set; }
    public float BaseRange { get; protected set; }
    public Entity LockedOn { get; protected set; }

    public Tower(AnimatedSprite sprite, float baseAttackSpeed, float baseAttackDamage, float baseRange) : base(sprite)
    {
        this.BaseAttackSpeed = baseAttackSpeed;
        this.BaseAttackDamage = baseAttackDamage;
        this.BaseRange = baseRange;
    }

    public double Cooldown { get; protected set; }
    public override void Update()
    {
        this.Cooldown -= Time.DeltaTime;
        if (this.Cooldown <= 0d && this.LockedOn != null)
        {
            Projectile projectile = new(Sprites.GetAnimatedSprite(Sprites.Arrow, "idle"), 150f)
            {
                Position = this.Position
            };
            Vector2 dir = new(this.LockedOn.GetX() - this.GetX(), this.LockedOn.GetY() - this.GetY());
            dir.Normalize();
            projectile.Direction = dir;
            Instantiate(projectile);
            this.Cooldown = 1d;
        }
        TryLockOnEntity();
        RotateToLockedEntity();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }

    protected void TryLockOnEntity()
    {
        if (this.LockedOn == null || this.LockedOn.RemovalMark)
            this.LockedOn = (Entity)GetUpdatableGameObjects().Where(gameObject => gameObject is AbstractEnemy).MinBy(g => ((Entity)g).DistanceTo(this));
    }

    protected void RotateToLockedEntity()
    {
        if (this.LockedOn == null)
            return;
        
        var deltaX = this.LockedOn.GetX() - this.GetX();
        var deltaY = this.LockedOn.GetY() - this.GetY();
        float rad = (float)Math.Atan2(deltaY, deltaX);
        this.Sprite.Rotation = rad;
    }
}