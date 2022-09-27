using HelloMonoWorld.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloMonoWorld;

public class Entity : GameObject
{
    public float MovementSpeed { get; set; } = 100f;
    public Vector2 DeltaMovement { get; set; } = Vector2.Zero;

    private float maxHealth;

    public float MaxHealth
    {
        get { return maxHealth; }
        //If higher max health than before then heal by the difference, otherwise prevent health from being higher than MaxHealth
        set
        {
            float diff = value - this.maxHealth;
            maxHealth = value;
            if (diff > 0)
                this.Health += diff;
            else if (diff < 0)
                this.Health = Math.Min(this.Health, this.maxHealth);
        }
    }

    public float Health { get; set; } = 0f;
    public double HitTime { get; set; } = 0d;
    public bool Knockbacked { get; set; } = false;
    public double KnockbackResistance { get; set; } = 0.1d;

    public Rectangle Bounds { get; set; } = Rectangle.Empty;

    /// <summary>
    /// If true, Bounds will be updated each cycle
    /// </summary>
    public bool ShouldUpdateBounds { get; set; } = true;

    /// <summary>
    /// If true, health bar will be rendered at the bottom of the sprite
    /// </summary>
    public bool ShouldDrawHealth { get; set; } = false;

    public Vector2 LeftHand { get; private set; } = new(40, 54);
    public Vector2 RightHand { get; private set; } = new(4, 54);

    private Color originalColor;

    public Color OriginalColor
    {
        get { return originalColor; }
        set
        {
            originalColor = value;
            this.Color = value;
        }
    }

    public static Texture2D OneByOneTexture = new(Graphics.graphics.GraphicsDevice, 1, 1);

    public Entity(string id) : this(id, id)
    {

    }

    public Entity(string id, string spriteName) : base(id, spriteName)
    {
        OneByOneTexture.SetData(new[] { Color.White });
    }

    public override void Update()
    {
        this.Move();
        if (this.ShouldUpdateBounds)
            this.UpdateBounds();
        if (this.HitTime > 0)
        {
            this.HitTime -= Time.DeltaTime;
            if (this.HitTime <= 0d)
                this.Color = this.OriginalColor;
        }
    }

    public void Move()
    {
        if (this.DeltaMovement != Vector2.Zero)
        {
            this.Position += Vector2.Multiply(this.DeltaMovement, (float)Time.DeltaTime);

            if (!this.Knockbacked)
                this.DeltaMovement = Vector2.Zero;
            else
            {
                this.DeltaMovement = Vector2.Multiply(this.DeltaMovement, (float)(1f - this.KnockbackResistance));
                if (this.DeltaMovement.Length() < 8f)
                {
                    this.Knockbacked = false;
                }
            }
        }
    }

    protected Vector2 GetRelativeMovement(Vector2 input)
    {
        float length = input.LengthSquared();
        if (length < 1e-4)
            return Vector2.Zero;
        return Vector2.Multiply(length > 1.0d ? Vector2.Normalize(input) : input, this.MovementSpeed);
    }

    public void Knockback(Vector2 direction, float force)
    {
        if (force == 0f)
            return;
        this.Knockbacked = true;
        this.DeltaMovement = Vector2.Multiply(direction, force);
    }

    public void UpdateBounds()
    {
        this.Bounds = new Rectangle((int)(this.Position.X - (this.Texture.Width * this.Origin.X)), (int)(this.Position.Y - (this.Texture.Height * this.Origin.Y)), this.Texture.Width, this.Texture.Height);
    }

    public virtual bool Hurt(float damage, float knockback = 0f)
    {
        if (this.IsDead())
            return false;

        this.Health -= damage;
        
        this.HitTime = 0.1d;
        this.Color = Color.Red;

        if (this.IsDead())
        {
            this.OnDeath();
        }
        return true;
    }

    public virtual void Kill()
    {
        this.Health = 0f;
        this.OnDeath();
    }

    public void Discard()
    {
        this.MarkForRemoval();
    }

    public virtual void OnDeath()
    {
        this.MarkForRemoval();
    }

    public virtual bool IsDead()
    {
        return this.Health <= 0f;
    }

    public virtual List<Entity> GetCollisions()
    {
        List<Entity> list = new();
        Engine.Engine.gameObjects.ForEach(g =>
        {
            if (g is not Entity entity
                    || g == this)
                return;

            if (entity.Enabled && this.Bounds.Intersects(entity.Bounds))
                list.Add(entity);
        });
        return list;
    }

    public virtual List<Entity> GetCollisions(params Entity[] toIgnore)
    {
        List<Entity> list = new();
        Engine.Engine.gameObjects.ForEach(g =>
        {
            if (g is not Entity entity
                    || g == this
                    || toIgnore.Contains(g))
                return;

            if (entity.Enabled && this.Bounds.Intersects(entity.Bounds))
                list.Add(entity);
        });
        return list;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        if (Options.Debug)
        {
            spriteBatch.Draw(OneByOneTexture, this.Bounds, Color.FromNonPremultiplied(255, 0, 0, 64));
        }
        if (this.ShouldDrawHealth)
        {
            spriteBatch.Draw(OneByOneTexture, this.Position.Sum(-25, this.Bounds.Height / 2 + 10), null, Color.FromNonPremultiplied(255, 100, 100, 192), 0f, Origins.CenterLeft, new Vector2((float)(this.Health / this.MaxHealth) * 50, 8), SpriteEffects.None, 0f);
        }
    }
}
