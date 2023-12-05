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
            this.Cooldown = this.BaseAttackSpeed;
        }
        TryLockOnEntity();
        RotateToLockedEntity();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        if (Options.Debug)
        {
            spriteBatch.Draw(CreateCircleTexture((int)(this.BaseRange * 2f)), this.Position.Sum(-this.BaseRange, -this.BaseRange), Color.FromNonPremultiplied(255, 0, 0, 64));
        }
    }
    
    Texture2D CreateCircleTexture(int diameter)
    {
        Texture2D texture = new Texture2D(Graphics.GraphicsDeviceManager.GraphicsDevice, diameter, diameter);
        Color[] colorData = new Color[diameter*diameter];

        float radius = diameter / 2f;
        float radiusSquared = radius * radius;

        for (int x = 0; x < diameter; x++)
        {
            for (int y = 0; y < diameter; y++)
            {
                int index = x * diameter + y;
                Vector2 pos = new Vector2(x - radius, y - radius);
                if (pos.LengthSquared() <= radiusSquared)
                    colorData[index] = Color.White;
                else
                    colorData[index] = Color.Transparent;
            }
        }

        texture.SetData(colorData);
        return texture;
    }

    protected void TryLockOnEntity()
    {
        if (this.LockedOn == null || this.LockedOn.RemovalMark)
            this.LockedOn = (Entity)GetUpdatableGameObjects().Where(gameObject =>
            {
                return gameObject is AbstractEnemy abstractEnemy 
                       && abstractEnemy.DistanceTo(this) < this.BaseRange * this.BaseRange;
            }).MinBy(gameObject => ((Entity)gameObject).DistanceTo(this));
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