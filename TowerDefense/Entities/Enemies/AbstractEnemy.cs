using MonoGame.Aseprite.Sprites;

namespace TowerDefense.Entities.Enemies;

public abstract class AbstractEnemy : Entity
{
    public float BaseDamage { get; private set; } = 1f;

    public AbstractEnemy(AnimatedSprite sprite) : base(sprite)
    {
        this.MaxHealth = 1;
        this.Heal(1);
        this.ShouldDrawHealth = true;
    }
}