using Engine;
using Microsoft.Xna.Framework;
using MonoGame.Aseprite;

namespace TowerDefense.Entities.Enemies;

public abstract class AbstractEnemy : Entity
{
    public float BaseDamage { get; private set; } = 1f;
    public float BaseMovementSpeed { get; private set; } = 20f;
    public double BaseAttackCooldown { get; private set; } = 1f;

    public double AttackCooldown;
    public int TurningPointIndex = -1;

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
            if (this.AttackCooldown > 0f) 
                continue;
            EndingPoint endingPoint = (EndingPoint)entity;
            endingPoint.Hurt(this.BaseDamage);
            this.AttackCooldown = this.BaseAttackCooldown;
        }

        if (this.TurningPointIndex == -1)
            this.TurningPointIndex++;

        if (this.TurningPointIndex <= MainGame.testMap.TurningPoints.Count - 1 && this.DistanceTo(this.GetNextTurningPoint()) < 15f)
        {
            this.TurningPointIndex++;
        }
    }

    public Vector2 GetNextTurningPoint()
    {
        if (this.TurningPointIndex == -1)
            return MainGame.testMap.StartPosition;
        if (this.TurningPointIndex > MainGame.testMap.TurningPoints.Count - 1)
            return MainGame.testMap.TowerPosition;
        return MainGame.testMap.TurningPoints[this.TurningPointIndex];
    }
}