using MonoGame.Aseprite.Sprites;

namespace TowerDefense.Entities.Enemies;

public class AbstractEnemy : Entity
{
    public float BaseDamage { get; private set; }

    public AbstractEnemy(AnimatedSprite sprite, float baseDamage, float health) : base(sprite)
    {
        this.BaseDamage = baseDamage;
        this.MaxHealth = health;
        this.Heal(health);
        this.ShouldDrawHealth = true;
    }
}