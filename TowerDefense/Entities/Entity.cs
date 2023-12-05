using System;
using System.Collections.Generic;
using System.Linq;
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Aseprite.Sprites;

namespace TowerDefense.Entities;

public class Entity : GameObject
{

    public Rectangle Bounds { get; set; } = Rectangle.Empty;

    /// <summary>
    /// If true, Bounds will be updated each cycle
    /// </summary>
    public bool ShouldUpdateBounds { get; set; } = true;


    public float Health { get; set; }

    private float _maxHealth;
    public float MaxHealth
    {
        get => _maxHealth;
        set
        {
            float diff = value - this._maxHealth;
            this._maxHealth = value;
            this.Health += diff;
        }
    }

    /// <summary>
    /// If true, health bar will be rendered at the bottom of the sprite
    /// </summary>
    public bool ShouldDrawHealth { get; set; } = false;
    
    public Entity(AnimatedSprite sprite)
    {
        this.SetSprite(sprite);
    }

    public override void Update()
    {
        base.Update();
        if (ShouldUpdateBounds)
            UpdateBounds();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        if (Options.Debug)
        {
            spriteBatch.Draw(Utils.OneByOneTexture, Bounds, Color.FromNonPremultiplied(255, 0, 0, 64));
        }
        if (this.ShouldDrawHealth)
        {
            spriteBatch.Draw(Utils.OneByOneTexture, this.Position.Sum(-25, Bounds.Height / 2f + 10), null, Color.FromNonPremultiplied(255, 100, 100, 192), 0f, Origins.CenterLeft, new Vector2(this.Health / this.MaxHealth * 50, 5), SpriteEffects.None, 0f);
        }
    }

    public void Heal(float amount)
    {
        this.Health = Math.Min(this.Health + amount, this.MaxHealth);
    }

    public void Hurt(float amount)
    {
        this.Health -= amount;
        if (this.Health <= 0f)
            this.MarkForRemoval();
    }

    public double DistanceTo(GameObject entity)
    {
        return Math.Pow(entity.GetX() - this.GetX(), 2) + Math.Pow(entity.GetY() - this.GetY(), 2);
    }

    public double DistanceToSqrt(GameObject entity)
    {
        return Math.Sqrt(DistanceTo(entity));
    }
    
    public void UpdateBounds()
    {
        this.Bounds = new Rectangle((int)(this.GetX() - this.Sprite.Origin.X), (int)(this.GetY() - this.Sprite.Origin.Y), this.GetWidth(), this.GetHeight());
    }

    public virtual List<Entity> GetCollisions()
    {
        return this.GetCollisionsIgnoring();
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

            if (entity.Enabled && this.Bounds.Intersects(entity.Bounds))
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
                if (type.IsInstanceOfType(gameObject))
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
                || gameObject == this
                || !entity.Enabled
                || !Bounds.Intersects(entity.Bounds))
                continue;

            if (clazz.Any(type => type.IsInstanceOfType(gameObject)))
                list.Add(entity);
        }
        return list;
    }
}