using MonoGame.Aseprite.Sprites;

namespace TowerDefense.Entities.Enemies;

public class AbstractEnemy : Entity
{
    public float BaseDamage { get; private set; }

    public AbstractEnemy(AnimatedSprite sprite, float baseDamage) : base(sprite)
    {
        BaseDamage = baseDamage;
    }
}