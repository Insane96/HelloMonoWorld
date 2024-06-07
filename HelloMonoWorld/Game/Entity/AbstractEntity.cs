using System;
using System.Collections.Generic;
using System.Linq;
using HelloMonoWorld.Game.Entity.Attributes;
using HelloMonoWorld.Game.Spell;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoEngine;
using Attribute = HelloMonoWorld.Game.Entity.Attributes.Attribute;

namespace HelloMonoWorld.Game.Entity;

public class AbstractEntity : GameObject
{
    public List<AttributeInstance> AttributeInstances = new List<AttributeInstance>();
    public Vector2 DeltaMovement { get; set; } = Vector2.Zero;

    /*private float maxHealth;

    public float MaxHealth
    {
        get { return maxHealth; }
        
        //TODO If higher max health than before then heal by the difference, otherwise prevent health from being higher than MaxHealth
        set
        {
            float diff = value - maxHealth;
            maxHealth = value;
            if (diff > 0)
                Health += diff;
            else if (diff < 0)
                Health = Math.Min(Health, maxHealth);
        }
    }*/

    public double AttackTime { get; set; }
    public Vector2? AttackDirection { get; set; }
    public SpellInstance BaseSpell { get; set; }

    public float Health { get; set; }
    public double HitTime { get; set; }
    public bool Knockbacked { get; set; }

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
            this.SetColor(value);
        }
    }

    public AbstractEntity(AsepriteDocument aseprite) : this(aseprite, null) { }

    public AbstractEntity(AsepriteDocument aseprite, Vector2? attackDirection)
    {
        this.AttackDirection = attackDirection;
        this.SetSprite(aseprite);
        Attributes.Attributes.AttributesList.ForEach(attribute => this.AttributeInstances.Add(new AttributeInstance(attribute)));
    }

    public override void Update()
    {
        base.Update();
        Move();
        if (ShouldUpdateBounds)
            UpdateBounds();
        if (HitTime > 0)
        {
            HitTime -= Time.DeltaTime;
            if (HitTime <= 0d)
                this.SetColor(OriginalColor);
        }
        if (AttackTime > 0d)
        {
            AttackTime -= Time.DeltaTime;
        }
    }

    public void Move()
    {
        if (this.DeltaMovement != Vector2.Zero)
        {
            this.Move(Vector2.Multiply(DeltaMovement, (float)Time.DeltaTime));

            if (!Knockbacked)
                this.DeltaMovement = Vector2.Zero;
            else
            {
                this.DeltaMovement = Vector2.Multiply(this.DeltaMovement, (float)(1f - this.GetAttributeValue(Attributes.Attributes.KnockbackResistance)));
                if (this.DeltaMovement.Length() < 8f)
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
        return Vector2.Multiply(length > 1.0d ? Vector2.Normalize(input) : input, (float)this.GetAttributeValue(Attributes.Attributes.MovementSpeed));
    }

    public void Push(Vector2 direction, float force)
    {
        if (force == 0f)
            return;
        this.Knockbacked = true;
        this.DeltaMovement = Vector2.Multiply(direction, force);
    }

    public void UpdateBounds()
    {
        this.Bounds = new Rectangle((int)(this.GetX() - this.Sprite.Origin.X), (int)(this.GetY() - this.Sprite.Origin.Y), this.GetWidth(), this.GetHeight());
    }

    public virtual bool Hurt(AbstractEntity source, AbstractEntity directSource, float damage, float knockback = 0f)
    {
        if (this.IsDead())
            return false;

        this.Health -= damage;

        this.HitTime = 0.1d;
        this.SetColor(Color.Red);

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
        MainGame.player.AddGold(1);
        this.MarkForRemoval();
    }

    public virtual bool IsDead()
    {
        return this.Health <= 0f;
    }

    public virtual List<AbstractEntity> GetCollisions()
    {
        return this.GetCollisionsIgnoring();
    }

    public virtual List<AbstractEntity> GetCollisionsIgnoring(params AbstractEntity[] entitiesToIgnore)
    {
        List<AbstractEntity> list = new();
        foreach (GameObject gameObject in GetUpdatableGameObjects())
        {
            if (gameObject is not AbstractEntity entity
                    || gameObject == this
                    || entitiesToIgnore.Contains(gameObject))
                continue;

            if (entity.Enabled && this.Bounds.Intersects(entity.Bounds))
                list.Add(entity);
        }
        return list;
    }

    public virtual List<AbstractEntity> GetCollisionsIgnoringClass(params Type[] classToIgnore)
    {
        List<AbstractEntity> list = new();
        foreach (GameObject gameObject in GetUpdatableGameObjects())
        {
            if (gameObject is not AbstractEntity entity
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

    public virtual List<AbstractEntity> GetCollisionsOfClass(params Type[] clazz)
    {
        List<AbstractEntity> list = new();
        foreach (GameObject gameObject in GetUpdatableGameObjects())
        {
            if (gameObject is not AbstractEntity entity
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
            spriteBatch.Draw(Utils.OneByOneTexture, this.GetPosition().Sum(-25, Bounds.Height / 2 + 10), null, Color.FromNonPremultiplied(255, 100, 100, 192), 0f, Origins.CenterLeft, new Vector2((float)(Health / this.GetAttributeValue(Attributes.Attributes.MaxHealth)) * 50, 8), SpriteEffects.None, 0f);
        }
    }

    public void SetMaxHealth(float maxHealth)
    {
        this.Health = maxHealth;
        this.SetAttribute(Attributes.Attributes.MaxHealth, maxHealth);
    }

    public AttributeInstance GetAttribute(Attribute attribute)
    {
        return this.AttributeInstances.First(a => a.Attribute.Equals(attribute));
    }

    public void SetAttribute(Attribute attribute, float baseValue)
    {
        this.AttributeInstances.First(a => a.Attribute.Equals(attribute)).BaseValue = baseValue;
    }

    public float GetAttributeValue(Attribute attribute)
    {
        return this.AttributeInstances.First(a => a.Attribute.Equals(attribute)).Value;
    }
}
