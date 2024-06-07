using Microsoft.Xna.Framework;
using MonoEngine;
using MonoGame.Aseprite.Sprites;
using MonoGame.Extended;

namespace TowerDefense.Entities.Enemies;

public class GenericMovingEnemy : AbstractEnemy
{
    public GenericMovingEnemy(AnimatedSprite sprite) : base(sprite)
    {
    }

    public override void Update()
    {
        base.Update();
        this.Move(new Vector2(MainGame.EndingPoint.Position.X - this.Position.X, MainGame.EndingPoint.Position.Y - this.Position.Y).NormalizedCopy().Multiply(this.BaseMovementSpeed * (float)Time.DeltaTime));
    }
}