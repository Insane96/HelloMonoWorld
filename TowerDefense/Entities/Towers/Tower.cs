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
    public bool IsUlting { get; protected set; }
    public double UltingTimer { get; protected set; }
    public double UltimateDuration { get; protected set; } = 2f;
    public float UltimateCharge { get; protected set; }
    public float UltimateChargeOnHit { get; set; }

    public double Cooldown { get; protected set; }
    public Entity LockedOn { get; protected set; }

    public Tower(SpriteSheet sprite) : base(Sprites.GetAnimatedSprite(sprite, "idle"))
    {
        this.BaseAttackSpeed = 1f;
        this.BaseAttackDamage = 1f;
        this.BaseRange = 200f;
        this.UltimateChargeOnHit = 0.01f;
    }

    public override void Update()
    {
        base.Update();
        this.Attack();
        this.UpdateUlting();
        this.TryLockOnEntity();
        this.RotateToLockedEntity();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        //Draw the circle and ult bar before the tower
        if (this.IsMouseOver())
        {
            spriteBatch.Draw(CreateCircleTexture((int)(this.BaseRange * 2f)), this.Position.Sum(-this.BaseRange, -this.BaseRange), Color.FromNonPremultiplied(0, 0, 0, 32));
        }

        spriteBatch.Draw(Utils.OneByOneTexture, this.Position.Sum(-7, Bounds.Height / 2f + 5), null, Color.FromNonPremultiplied(this.UltimateCharge >= 1f ? 0 : 255, 204, 102, 192), 0f, Origins.CenterLeft, new Vector2(this.UltimateCharge * 14, 3), SpriteEffects.None, 0f);
        base.Draw(spriteBatch);
    }

    private static Texture2D CreateCircleTexture(int diameter)
    {
        Texture2D texture = new(Graphics.GraphicsDeviceManager.GraphicsDevice, diameter, diameter);
        Color[] colorData = new Color[diameter * diameter];

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
        if (this.IsUlting 
            || this.UltimateCharge >= 1f)
            return;
        this.UltimateCharge += this.UltimateChargeOnHit;
    }

    protected virtual void TryLockOnEntity()
    {
        if (this.LockedOn != null && this.DistanceTo(this.LockedOn) > this.BaseRange * this.BaseRange)
            this.LockedOn = null;
        
        if (this.LockedOn == null || this.LockedOn.RemovalMark)
            this.LockedOn = (Entity)GetUpdatableGameObjects().Where(gameObject => gameObject is AbstractEnemy abstractEnemy
                                                                                  && abstractEnemy.DistanceTo(this) < this.BaseRange * this.BaseRange)
                .MinBy(gameObject => ((Entity)gameObject).DistanceTo(this));
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

    public override bool Intersects(Entity entity)
    {
        if (entity is Tower tower)
        {
            return (tower.Position - this.Position).Length() < this.GetWidth();
        }

        Vector2 vec2 = entity.Position - this.Position;
        vec2 = Vector2.Clamp(vec2, new Vector2(entity.Bounds.Left, entity.Bounds.Top), new Vector2(entity.Bounds.Right, entity.Bounds.Bottom));
        vec2 = entity.Position + vec2;
        return (vec2 - this.Position).Length() <= this.GetWidth();
    }

    public virtual void Attack()
    {
        this.Cooldown -= Time.DeltaTime;
        if (this.Cooldown <= 0d && this.LockedOn != null)
        {
            Projectile projectile = new(Sprites.GetAnimatedSprite(Sprites.Arrow, "idle"), this, 250f, this.LockedOn)
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
    }

    public virtual void UpdateUlting()
    {
        if (this.IsUlting)
        {
            this.UltingTimer -= Time.DeltaTime;
            if (this.UltingTimer <= 0f)
            {
                this.IsUlting = false;
                this.UltimateCharge = 0f;
                //Time.TimeScale = 1f;
            }
        }
    }

    public override bool IsMouseOver()
    {
        double distance = this.DistanceTo(new Vector2(Input.MouseState.X, Input.MouseState.Y));
        return base.IsMouseOver() && distance < this.GetWidth() / 2f * this.GetWidth() / 2f;
    }

    public override void OnMouseClickedOn()
    {
        if (!(this.UltimateCharge >= 1f)) 
            return;
        this.IsUlting = true;
        this.UltingTimer = this.UltimateDuration;
        this.Cooldown = 0f;
        //Time.TimeScale = 0.5f;
    }
}