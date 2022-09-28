using HelloMonoWorld.Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HelloMonoWorld
{
    public class EnergyBall : Projectile
    {
        double DamageInterval { get; set; } = 0.25d;

        public EnergyBall(string id, string spriteName, Vector2 direction, float damage, float knockback, Entity owner, double damageInterval) : base(id, spriteName, direction, damage, knockback, owner)
        {
            this.DamageInterval = damageInterval;
            this.AttackTime = damageInterval;
        }

        public override void Update()
        {
            if (this.AttackTime > 0d)
            {
                this.AttackTime -= Time.DeltaTime;
            }
            base.Update();
            if (this.AttackTime <= 0)
                this.AttackTime = this.DamageInterval;
        }

        public override void OnEntityHit(Entity other)
        {
            if (this.AttackTime <= 0d)
                other.Hurt(this.damage);
        }
    }
}
