using System;
using System.Diagnostics;
using System.Linq;
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefense.Entities.Enemies;
using TowerDefense.Registry;

namespace TowerDefense.Entities;

public class LaserUlt : Entity
{
    private double _timeAlive;
    
    public LaserUlt(double timeAlive) : base(Sprites.GetAnimatedSprite(Sprites.LaserUlt, ""))
    {
        this._timeAlive = timeAlive;
        this.Origin = Origins.CenterLeft;
        this.Sprite.IsPingPong = true;
        this.Sprite.Play(2);
        this.Sprite.ScaleX = 1500f;
    }

    public override void Update()
    {
        base.Update();
        double oTimeAlive = this._timeAlive;
        this._timeAlive -= Time.DeltaTime;
        if (oTimeAlive >= 0.175d && this._timeAlive < 0.175d)
        {
            Vector2 direction = new((float)Math.Cos(this.Sprite.Rotation), (float)Math.Sin(this.Sprite.Rotation));
            direction.Normalize();
            Vector2 w = new(direction.Y - this.Position.Y, this.Position.X - direction.X);
            GetUpdatableGameObjects().OfType<AbstractEnemy>().ToList().ForEach(enemy =>
            {
                float dot1 = Vector2.Dot(new Vector2(enemy.Bounds.X - this.Position.X, enemy.Bounds.Y - this.Position.Y), w);
                float dot2 = Vector2.Dot(new Vector2(enemy.Bounds.X + enemy.Bounds.Width - this.Position.X, enemy.Bounds.Y - this.Position.Y), w);
                float dot3 = Vector2.Dot(new Vector2(enemy.Bounds.X - this.Position.X, enemy.Bounds.Y + enemy.Bounds.Height - this.Position.Y), w);
                float dot4 = Vector2.Dot(new Vector2(enemy.Bounds.X + enemy.Bounds.Width - this.Position.X, enemy.Bounds.Y + enemy.Bounds.Height - this.Position.Y), w);
                Debug.WriteLine($"{dot1} {dot2} {dot3} {dot4}");
            });
        }
        if (this._timeAlive <= 0d)
            this.MarkForRemoval();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        this.Sprite?.Draw(spriteBatch, this.Position);
        if (Options.Debug)
        {
            //spriteBatch.Draw(Utils.OneByOneTexture, this.Bounds, Color.FromNonPremultiplied(255, 0, 0, 64));
        }
    }
}