using Microsoft.Xna.Framework;
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
    public Direction attackDirection = null;

    public double health = 10d;
    public double immunityTime = 0d;

    public Rectangle Bounds;

    /// <summary>
    /// If true, Bounds will be updated each cycle
    /// </summary>
    public bool ShouldUpdateBounds { get; set; } = true;

    public Vector2 LeftHand { get; private set; } = new(40, 54);
    public Vector2 RightHand { get; private set; } = new(4, 54);

    public Entity(string id) : base(id, id)
    {
        this.UpdateBounds();
    }

    public Entity(string id, string spriteName) : base(id, spriteName)
    {

    }

    public override void Initialize()
    {

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
        if (deltaMovement != Vector2.Zero)
        {
            this.position += Vector2.Multiply(this.deltaMovement, (float)Time.DeltaTime);

            this.deltaMovement = Vector2.Zero;
        }
    }

    public void UpdateBounds()
    {
        this.Bounds = new Rectangle((int)(this.position.X - (this.texture.Width * this.origin.X)), (int)(this.position.Y - (this.texture.Height * this.origin.Y)), this.texture.Width, this.texture.Height);
    }

    public virtual bool Hurt(double damage, double immunityTime = 0.5d)
    {
        if (this.IsImmune())
            return false;

        this.health -= damage;
        this.immunityTime = immunityTime;
        if (this.IsDead())
        {
            this.MarkForRemoval();
        }
        return true;
    }

    public virtual bool IsImmune()
    {
        return this.immunityTime > 0d;
    }

    public virtual bool IsDead()
    {
        return this.health <= 0d;
    }

    public virtual List<Entity> GetCollisions()
    {
        List<Entity> list = new();
        MainGame.gameObjects.ForEach(g =>
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
        MainGame.gameObjects.ForEach(g =>
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
        if (Graphics.Debug)
        {
            var t = new Texture2D(Graphics.graphics.GraphicsDevice, 1, 1);
            t.SetData(new[] { Color.White });
            spriteBatch.Draw(t, this.Bounds, Color.FromNonPremultiplied(255, 0, 0, 64));
        }
    }
}
