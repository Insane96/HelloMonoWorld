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
    public float movementSpeed = 100f;
    public Vector2 deltaMovement = Vector2.Zero;

    private float maxHealth;

    public float MaxHealth
    {
        get { return maxHealth; }
        //If higher max health than before then heal by the difference, otherwise prevent health from begin higher than MaxHealth
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
    public double immunityTime = 0d;
    public bool knockbacked = false;
    public double knockbackResistance = 0.9d;

    public Rectangle Bounds = Rectangle.Empty;

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

    public static Texture2D healthBarTexture = new(Graphics.graphics.GraphicsDevice, 1, 1);

    public Entity(string id) : this(id, id)
    {
    }

    public Entity(string id, string spriteName) : base(id, spriteName)
    {
        //this.UpdateBounds();
        healthBarTexture.SetData(new[] { Color.White });
    }

    public override void Update()
    {
        this.Move();
        if (this.ShouldUpdateBounds)
            this.UpdateBounds();
        if (this.immunityTime > 0)
            this.immunityTime -= Time.DeltaTime;
    }

    public void Move()
    {
        if (this.deltaMovement != Vector2.Zero)
        {
            this.position += Vector2.Multiply(this.deltaMovement, (float)Time.DeltaTime);

            if (!this.knockbacked)
                this.deltaMovement = Vector2.Zero;
            else
            {
                this.deltaMovement = Vector2.Multiply(this.deltaMovement, (float)this.knockbackResistance);
                if (this.deltaMovement.Length() < 5f)
                {
                    this.knockbacked = false;
                }
            }
        }
    }

    public void Knockback(Vector2 direction, float force)
    {
        if (force == 0f)
            return;
        this.knockbacked = true;
        this.deltaMovement = Vector2.Multiply(direction, force);
    }

    public void UpdateBounds()
    {
        this.Bounds = new Rectangle((int)(this.position.X - (this.texture.Width * this.origin.X)), (int)(this.position.Y - (this.texture.Height * this.origin.Y)), this.texture.Width, this.texture.Height);
    }

    public virtual bool Hurt(float damage, float knockback = 0f)
    {
        if (this.IsImmune()
            || this.IsDead())
            return false;

        this.Health -= damage;
        //this.immunityTime = immunityTime;
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

    public virtual bool IsImmune()
    {
        return this.immunityTime > 0d;
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

    protected Vector2 GetRelativeMovement(Vector2 input)
    {
        float length = input.LengthSquared();
        if (length < 1e-4)
            return Vector2.Zero;
        return Vector2.Multiply(length > 1.0d ? Vector2.Normalize(input) : input, this.movementSpeed);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        if (Options.Debug)
        {
            var t = new Texture2D(Graphics.graphics.GraphicsDevice, 1, 1);
            t.SetData(new[] { Color.White });
            spriteBatch.Draw(t, this.Bounds, Color.FromNonPremultiplied(255, 0, 0, 64));
        }
        if (this.ShouldDrawHealth)
        {
            spriteBatch.Draw(healthBarTexture, this.position.Sum(-25, this.Bounds.Height / 2 + 25), null, Color.FromNonPremultiplied(255, 100, 100, 192), 0f, Origins.CenterLeft, new Vector2((float)(this.Health / this.MaxHealth) * 50, 10), SpriteEffects.None, 0f);
        }
    }
}
