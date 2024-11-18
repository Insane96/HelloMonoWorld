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

public class LaserAbility : Entity
{
    private double _life;
    private double _lifeSpan;
    
    public LaserAbility(double lifeSpan) : base(Sprites.GetAnimatedSprite(Sprites.LaserAbility, ""))
    {
        this._lifeSpan = lifeSpan;
        this.Origin = Origins.CenterLeft;
        this.Sprite!.IsPingPong = false;
        this.Sprite.Play();
        this.Sprite.ScaleX = 1500f;
    }

    public override void Update()
    {
        base.Update();
        this._life += Time.DeltaTime;
        if (this._life >= 0.25f)
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
                    enemy.Hurt((float)(50f * Time.DeltaTime));
            });
            this.Sprite.Pause();
        }
        if (this._life >= this._lifeSpan)
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