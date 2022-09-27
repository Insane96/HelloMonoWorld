using HelloMonoWorld.Engine;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace HelloMonoWorld;

public class Projectile : Entity
{
    public Vector2 direction;
    public float damage;
    public float knockback;
    public Entity owner;

    public Projectile(string id, string spriteName, Vector2 direction, float damage, float knockback, Entity owner) : base(id, spriteName)
    {
        this.direction = direction;
        this.damage = damage;
        this.knockback = knockback;
        this.owner = owner;
    }

    public override void Update()
    {
        this.DeltaMovement = this.direction.Multiply(this.MovementSpeed);
        List<Entity> entitiesCollided = this.GetCollisionsOfClass(typeof(AbstractEnemy));
        bool hasCollided = false;
        entitiesCollided.ForEach(entity => {
            entity.Hurt(this.damage, this.knockback);
            hasCollided = true;
        });
        if (hasCollided)
            this.Discard();
        base.Update();
    }
}