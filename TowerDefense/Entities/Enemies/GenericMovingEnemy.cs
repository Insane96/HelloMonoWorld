using Engine;
using Microsoft.Xna.Framework;
using MonoGame.Aseprite.Sprites;

namespace TowerDefense.Entities.Enemies;

public class GenericMovingEnemy : AbstractEnemy
{
    public GenericMovingEnemy(AnimatedSprite sprite) : base(sprite)
    {
    }

    public override void Update()
    {
        base.Update();
        this.Move(new Vector2(1, 0).Multiply(20f * (float)Time.DeltaTime));
    }
}