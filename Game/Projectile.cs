using HelloMonoWorld.Engine;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

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
        base.Update();
        this.DeltaMovement = this.direction.Multiply(this.MovementSpeed);
        Entity entityCollided = this.GetCollisionsOfClass(typeof(AbstractEnemy)).FirstOrDefault();
        if (entityCollided != null)
        {
            this.OnEntityHit(entityCollided);
        }
    }

    public virtual void OnEntityHit(Entity other)
    {
        other.Hurt(this.damage, this.knockback);
        this.Discard();
    }
}