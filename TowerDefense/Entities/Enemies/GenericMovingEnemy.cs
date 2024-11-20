using Engine;
using Engine.ExtensionMethods;
using Microsoft.Xna.Framework;
using MonoGame.Aseprite;
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
        Vector2 target = this.GetNextTurningPoint();
        if (this.DistanceTo(target) <= 10f) 
            return;
        this.Move(new Vector2(target.X - this.Position.X, target.Y - this.Position.Y).NormalizedCopy().Multiply(this.BaseMovementSpeed * (float)Time.DeltaTime));
    }
}