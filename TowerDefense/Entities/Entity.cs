using System;
using System.Collections.Generic;
using System.Linq;
using Engine;
using Microsoft.Xna.Framework;
using MonoGame.Aseprite.Sprites;

namespace TowerDefense.Entities;

public class Entity : GameObject
{

    public Rectangle Bounds { get; set; } = Rectangle.Empty;

    /// <summary>
    /// If true, Bounds will be updated each cycle
    /// </summary>
    public bool ShouldUpdateBounds { get; set; } = true;
    
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