using MonoGame.Aseprite.Sprites;

namespace TowerDefense.Entities;

public class EndingPoint : Entity
{
    public EndingPoint(AnimatedSprite sprite) : base(sprite)
    {
        this.MaxHealth = 100f;
        this.ShouldDrawHealth = true;
        this.ShouldUpdateBounds = true;
    }
}