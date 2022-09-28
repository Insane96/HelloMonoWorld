using HelloMonoWorld.Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HelloMonoWorld.Game
{
    public class EnergyBall : Projectile
    {
        double DamageInterval { get; set; } = 0.25d;

        public EnergyBall(string id, string spriteName, Vector2 direction, float damage, float knockback, Entity owner, double damageInterval) : base(id, spriteName, direction, damage, knockback, owner)
        {
            DamageInterval = damageInterval;
            AttackTime = damageInterval;
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
                if (other.Hurt(damage))
                {
                    Sounds.Hit.Play(0.5f * Options.Volume, Mth.NextFloat(MainGame.random, -0.25f, 0.25f), 0f);
                }
            }
        }
    }
}
