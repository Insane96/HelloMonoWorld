using HelloMonoWorld.Engine;
using HelloMonoWorld.Game.Spell;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloMonoWorld.Game;

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
            float diff = value - maxHealth;
            maxHealth = value;
            if (diff > 0)
                Health += diff;
            else if (diff < 0)
                Health = Math.Min(Health, maxHealth);
        }
    }

    public double AttackTime { get; set; } = 0f;
    public Vector2? AttackDirection { get; set; }
    public SpellInstance BaseSpell { get; set; }

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
            Color = value;
        }
    }

    public Entity(string spriteName) : this(spriteName, null) { }

    public Entity(string spriteName, Vector2? attackDirection) : base()
    {
        this.AttackDirection = attackDirection;
        this.SetSprite(spriteName);
    }

    public override void Update()
    {
        Move();
        if (ShouldUpdateBounds)
            UpdateBounds();
        if (HitTime > 0)
        {
            HitTime -= Time.DeltaTime;
            if (HitTime <= 0d)
                Color = OriginalColor;
        }
        if (AttackTime > 0d)
        {
            AttackTime -= Time.DeltaTime;
        }
    }

    public void Move()
    {
        if (DeltaMovement != Vector2.Zero)
        {
            Position += Vector2.Multiply(DeltaMovement, (float)Time.DeltaTime);

            if (!Knockbacked)
                DeltaMovement = Vector2.Zero;
            else
            {
                DeltaMovement = Vector2.Multiply(DeltaMovement, (float)(1f - KnockbackResistance));
                if (DeltaMovement.Length() < 8f)
                {
                    Knockbacked = false;
                }
            }
        }
    }

    protected Vector2 GetRelativeMovement(Vector2 input)
    {
        float length = input.LengthSquared();
        if (length < 1e-4)
            return Vector2.Zero;
        return Vector2.Multiply(length > 1.0d ? Vector2.Normalize(input) : input, MovementSpeed);
    }

    public void Push(Vector2 direction, float force)
    {
        if (force == 0f)
            return;
        Knockbacked = true;
        DeltaMovement = Vector2.Multiply(direction, force);
    }

    public void UpdateBounds()
    {
        Bounds = new Rectangle((int)(Position.X - Texture.Width * Origin.X), (int)(Position.Y - Texture.Height * Origin.Y), Texture.Width, Texture.Height);
    }

    public virtual bool Hurt(float damage, float knockback = 0f)
    {
        if (IsDead())
            return false;

        Health -= damage;

        HitTime = 0.1d;
        Color = Color.Red;

        if (IsDead())
        {
            OnDeath();
        }
        return true;
    }

    public virtual void Kill()
    {
        Health = 0f;
        OnDeath();
    }

    public void Discard()
    {
        MarkForRemoval();
    }

    public virtual void OnDeath()
    {
        MarkForRemoval();
    }

    public virtual bool IsDead()
    {
        return Health <= 0f;
    }

    public virtual List<Entity> GetCollisions()
    {
        return GetCollisionsIgnoring();
    }

    public virtual List<Entity> GetCollisionsIgnoring(params Entity[] entitiesToIgnore)
    {
        List<Entity> list = new();
        foreach (GameObject gameObject in GetUpdatableGameObjects())
        {
            if (gameObject is not Entity entity
                    || gameObject == this
                    || entitiesToIgnore.Contains(gameObject))
                continue;

            if (entity.Enabled && Bounds.Intersects(entity.Bounds))
                list.Add(entity);
        }
        return list;
    }

    public virtual List<Entity> GetCollisionsIgnoringClass(params Type[] classToIgnore)
    {
        List<Entity> list = new();
        foreach (GameObject gameObject in GetUpdatableGameObjects())
        {
            if (gameObject is not Entity entity
                    || gameObject == this)
                continue;

            bool flag = false;
            foreach (Type type in classToIgnore)
            {
                if (type.IsAssignableFrom(gameObject.GetType()))
                    flag = true;
            }
            if (flag)
                continue;

            if (entity.Enabled && Bounds.Intersects(entity.Bounds))
                list.Add(entity);
        }
        return list;
    }

    public virtual List<Entity> GetCollisionsOfClass(params Type[] clazz)
    {
        List<Entity> list = new();
        foreach (GameObject gameObject in GetUpdatableGameObjects())
        {
            if (gameObject is not Entity entity
                    || gameObject == this)
                continue;

            foreach (Type type in clazz)
            {
                if (type.IsAssignableFrom(gameObject.GetType())
                    && entity.Enabled
                    && Bounds.Intersects(entity.Bounds))
                {
                    list.Add(entity);
                    break;
                }
            }
        }
        return list;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        if (Options.Debug)
        {
            spriteBatch.Draw(Utils.OneByOneTexture, Bounds, Color.FromNonPremultiplied(255, 0, 0, 64));
        }
        if (ShouldDrawHealth)
        {
            spriteBatch.Draw(Utils.OneByOneTexture, Position.Sum(-25, Bounds.Height / 2 + 10), null, Color.FromNonPremultiplied(255, 100, 100, 192), 0f, Origins.CenterLeft, new Vector2((float)(Health / MaxHealth) * 50, 8), SpriteEffects.None, 0f);
        }
    }
}
