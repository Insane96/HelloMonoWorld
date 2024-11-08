using Engine;
using MonoGame.Aseprite;

namespace TowerDefense.Entities.Enemies;

public abstract class AbstractEnemy : Entity
{
    public float BaseDamage { get; private set; } = 1f;
    public float BaseMovementSpeed { get; private set; } = 20f;
    public double BaseAttackCooldown { get; private set; } = 1f;

    public double AttackCooldown;

    public AbstractEnemy(AnimatedSprite sprite) : base(sprite)
    {
        this.ShouldDrawHealth = true;
        this.AttackCooldown = this.BaseAttackCooldown;
    }

    public override void Update()
    {
        base.Update();
        this.AttackCooldown -= Time.DeltaTime;
        foreach (Entity entity in this.GetCollisionsOfClass(typeof(EndingPoint)))
        {
            if (this.AttackCooldown <= 0f)
            {
                EndingPoint endingPoint = (EndingPoint)entity;
                endingPoint.Hurt(this.BaseDamage);
                this.AttackCooldown = this.BaseAttackCooldown;
            }
        }
    }
}