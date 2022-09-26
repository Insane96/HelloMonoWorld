using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloMonoWorld
{
    public class Projectile : Entity
    {
        public float damage;
        public float knockback;
        public Entity owner;

        public Projectile(string id, string spriteName, float damage, float knockback, Entity owner) : base(id, spriteName)
        {
            this.damage = damage;
            this.knockback = knockback;
            this.owner = owner;
        }

        public override void Update()
        {
            this.deltaMovement = new Vector2(this.movementSpeed, 0);
            List<Entity> entitiesCollided = this.GetCollisions(this.owner);
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
}
