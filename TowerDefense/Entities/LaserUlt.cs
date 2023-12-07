using System.Threading;
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Aseprite;
using MonoGame.Aseprite.Sprites;
using MonoGame.Extended.Sprites;
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
            this.GetCollisionsOfClass(typeof(AbstractEnemy)).ForEach(entity =>
            {
                AbstractEnemy abstractEnemy = (AbstractEnemy)entity;
                abstractEnemy.Hurt(50f);
            });
        }
        if (this._timeAlive <= 0d)
            this.MarkForRemoval();
    }

    public override void UpdateBounds()
    {
        base.UpdateBounds();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        this.Sprite?.Draw(spriteBatch, this.Position);
        if (Options.Debug)
        {
            spriteBatch.Draw(Utils.OneByOneTexture, this.Bounds, Color.FromNonPremultiplied(255, 0, 0, 64));
        }
    }
}