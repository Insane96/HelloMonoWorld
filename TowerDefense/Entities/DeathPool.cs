using MonoEngine;
using TowerDefense.Entities.Enemies;
using TowerDefense.Entities.Towers;
using TowerDefense.Registry;

namespace TowerDefense.Entities;

public class DeathPool : OwnableEntity
{
    public double TimeToDamage { get; protected set; } = 0.6d;
    
    public DeathPool(Tower owner) : base(Sprites.GetAnimatedSprite(Sprites.DeathPool, "idle"), owner)
    {
        
    }

    public override void Update()
    {
        base.Update();
        this.TimeToDamage -= Time.DeltaTime;
        if (this.TimeToDamage > 0f) 
            return;
        foreach (var entity in this.GetCollisionsOfClass(typeof(AbstractEnemy)))
        {
            var abstractEnemy = (AbstractEnemy)entity;
            abstractEnemy.Hurt(this.Owner.BaseAttackDamage);
            this.Owner.OnHitEnemy(abstractEnemy);
        }

        this.MarkForRemoval();
    }
}