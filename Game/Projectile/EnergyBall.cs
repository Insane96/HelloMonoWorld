using HelloMonoWorld.Engine;
using HelloMonoWorld.Game.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using MonoGame.Aseprite.Documents;

namespace HelloMonoWorld.Game.Projectile.Projectile
{
    public class EnergyBall : BasicProjectile
    {
        double DamageInterval { get; set; } = 0.2d;

        public EnergyBall(AsepriteDocument aseprite, Vector2 direction, float damage, float knockback, AbstractEntity owner, double damageInterval) : base(aseprite, direction, damage, knockback, owner)
        {
            DamageInterval = damageInterval;
            AttackTime = damageInterval;
        }

        public EnergyBall(AsepriteDocument aseprite, float damage, float knockback, double damageInterval) : this(aseprite, default, damage, knockback, null, damageInterval)
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

        public override void OnEntityHit(AbstractEntity other)
        {
            if (AttackTime <= 0d)
            {
                if (other.Hurt(this.Owner, this, this.Damage))
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
