using System;
using System.Diagnostics;
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
    public bool IsUlting { get; protected set; }
    public double UltingTimer { get; protected set; }
    public float UltimateCharge { get; protected set; }
    public float UltimateChargeOnHit { get; set; }

    public double Cooldown { get; protected set; }
    public Entity LockedOn { get; protected set; }

    public Tower(AnimatedSprite sprite, float baseAttackSpeed, float baseAttackDamage, float baseRange, float ultimateChargeOnHit) : base(sprite)
    {
        this.BaseAttackSpeed = baseAttackSpeed;
        this.BaseAttackDamage = baseAttackDamage;
        this.BaseRange = baseRange;
        this.UltimateChargeOnHit = ultimateChargeOnHit;
    }
    public override void Update()
    {
        base.Update();
        this.Cooldown -= Time.DeltaTime;
        if (this.Cooldown <= 0d && this.LockedOn != null)
        {
            Projectile projectile = new(Sprites.GetAnimatedSprite(Sprites.Arrow, "idle"), this, 250f)
            {
                Position = this.Position
            };
            Vector2 dir = new(this.LockedOn.GetX() - this.GetX(), this.LockedOn.GetY() - this.GetY());
            dir.Normalize();
            projectile.Direction = dir;
            Instantiate(projectile);
            this.Cooldown = this.BaseAttackSpeed;
            if (this.IsUlting)
                this.Cooldown /= 8f;
                
        }
        if (this.IsUlting)
        {
            this.UltingTimer -= Time.DeltaTime;
            if (this.UltingTimer <= 0f)
            {
                this.IsUlting = false;
                this.UltimateCharge = 0f;
                Time.TimeScale = 1f;
            }
        }
        TryLockOnEntity();
        RotateToLockedEntity();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (this.IsMouseOver())
        {
            spriteBatch.Draw(CreateCircleTexture((int)(this.BaseRange * 2f)), this.Position.Sum(-this.BaseRange, -this.BaseRange), Color.FromNonPremultiplied(192, 0, 0, 32));
        }
        spriteBatch.Draw(Utils.OneByOneTexture, this.Position.Sum(-7, Bounds.Height / 2f + 5), null, Color.FromNonPremultiplied(255, 204, 102, 192), 0f, Origins.CenterLeft, new Vector2(this.UltimateCharge * 14, 3), SpriteEffects.None, 0f);
        base.Draw(spriteBatch);
    }

    private static Texture2D CreateCircleTexture(int diameter)
    {
        Texture2D texture = new(Graphics.GraphicsDeviceManager.GraphicsDevice, diameter, diameter);
        Color[] colorData = new Color[diameter*diameter];

        float radius = diameter / 2f;
        float radiusSquared = radius * radius;

        for (int x = 0; x < diameter; x++)
        {
            for (int y = 0; y < diameter; y++)
            {
                int index = x * diameter + y;
                Vector2 pos = new(x - radius, y - radius);
                if (pos.LengthSquared() <= radiusSquared)
                    colorData[index] = Color.White;
                else
                    colorData[index] = Color.Transparent;
            }
        }

        texture.SetData(colorData);
        return texture;
    }

    public void OnHitEnemy(AbstractEnemy enemy)
    {
        if (this.IsUlting) 
            return;
        this.UltimateCharge += this.UltimateChargeOnHit;
        if (this.UltimateCharge >= 1f)
        {
            this.IsUlting = true;
            this.UltingTimer = 2f;
            this.Cooldown = 0f;
            Time.TimeScale = 0.5f;
        }
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