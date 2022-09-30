using HelloMonoWorld.Engine;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace HelloMonoWorld.Game.Projectile;

public class BasicProjectile : Entity
{
    public Vector2 Direction { get; set; }
    public float Damage { get; set; }
    public float Knockback { get; set; }
    public Entity Owner { get; set; }

    public BasicProjectile(string id, Vector2 direction, float damage, float knockback, Entity owner) : base(id)
    {
        this.Direction = direction;
        this.Damage = damage;
        this.Knockback = knockback;
        this.Owner = owner;
    }

    public BasicProjectile(string id, float damage, float knockback) : this(id, default, damage, knockback, null)
    {

    }

    public override void Update()
    {
        base.Update();
        DeltaMovement = Direction.Multiply(MovementSpeed);
        Entity entityCollided = GetCollisionsOfClass(typeof(AbstractEnemy)).FirstOrDefault();
        if (entityCollided != null)
        {
            OnEntityHit(entityCollided);
        }
    }

    public virtual void OnEntityHit(Entity other)
    {
        other.Hurt(Damage, Knockback);
        Discard();
    }
}