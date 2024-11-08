using System;
using System.Linq;
using Engine;
using Engine.ExtensionMethods;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
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
        if (oTimeAlive >= 0.10d && this._timeAlive < 0.10d)
        {
            Vector2 direction = new((float)Math.Cos(this.Sprite.Rotation), (float)Math.Sin(this.Sprite.Rotation));
            direction.Normalize();
            float traslation = this.GetHeight() / 2f - 2f;
            Vector2 dirC = Utils.RotateDirectionClockwise(direction);
            Vector2 traslatedPosC = this.Position.Translate(dirC.X * traslation, dirC.Y * traslation);
            Vector2 dirCc = Utils.RotateDirectionCounterClockwise(direction);
            Vector2 traslatedPosCc = this.Position.Translate(dirCc.X * traslation, dirCc.Y * traslation);
            GetUpdatableGameObjects().OfType<AbstractEnemy>().ToList().ForEach(enemy =>
            {
                if (Utils.Intersects(this.Position, this.Position + direction.Multiply(this.GetWidth()), enemy.Bounds) || Utils.Intersects(traslatedPosC, traslatedPosC + direction.Multiply(this.GetWidth()), enemy.Bounds) || Utils.Intersects(traslatedPosCc, traslatedPosCc + direction.Multiply(this.GetWidth()), enemy.Bounds))
                    enemy.Hurt(50f);
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
            Vector2 direction = new((float)Math.Cos(this.Sprite.Rotation), (float)Math.Sin(this.Sprite.Rotation));
            direction.Normalize();
            float traslation = this.GetHeight() / 2f - 2f;
            Vector2 dirC = Utils.RotateDirectionClockwise(direction);
            Vector2 traslatedPosC = this.Position.Translate(dirC.X * traslation, dirC.Y * traslation);
            Vector2 dirCc = Utils.RotateDirectionCounterClockwise(direction);
            Vector2 traslatedPosCc = this.Position.Translate(dirCc.X * traslation, dirCc.Y * traslation);
            spriteBatch.DrawLine(this.Position, this.Position + direction.Multiply(this.GetWidth()), Color.FromNonPremultiplied(255, 0, 0, 64));
            spriteBatch.DrawLine(traslatedPosC, traslatedPosC + direction.Multiply(this.GetWidth()), Color.FromNonPremultiplied(255, 0, 0, 64));
            spriteBatch.DrawLine(traslatedPosCc, traslatedPosCc + direction.Multiply(this.GetWidth()), Color.FromNonPremultiplied(255, 0, 0, 64));
            //spriteBatch.Draw(Utils.OneByOneTexture, this.Bounds, Color.FromNonPremultiplied(255, 0, 0, 64));
        }
    }
}