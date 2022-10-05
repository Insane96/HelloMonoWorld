using HelloMonoWorld.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HelloMonoWorld.Game.Projectile.Projectile
{
    public class EnergyBall : BasicProjectile
    {
        double DamageInterval { get; set; } = 0.2d;

        public EnergyBall(string id, Vector2 direction, float damage, float knockback, Entity owner, double damageInterval) : base(id, direction, damage, knockback, owner)
        {
            DamageInterval = damageInterval;
            AttackTime = damageInterval;
        }

        public EnergyBall(string id, float damage, float knockback, double damageInterval) : this(id, default, damage, knockback, null, damageInterval)
        {

        }

        public override void Update()
        {
            if (AttackTime > 0d)
            {
                AttackTime -= Time.DeltaTime;
            }
            base.Update();
            if (AttackTime <= 0)
                AttackTime = DamageInterval;
        }

        public override void OnEntityHit(Entity other)
        {
            if (AttackTime <= 0d)
            {
                if (other.Hurt(this.Damage))
                {
                    Sounds.PlaySoundVariated(this.GetHitSound(), 0.25f, 0.25f);
                }
            }
        }

        protected override SoundEffect GetHitSound()
        {
            return Sounds.WaterHit;
        }
    }
}
