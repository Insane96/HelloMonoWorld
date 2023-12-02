using Engine;
using HelloMonoWorld.Game.Entity;
using HelloMonoWorld.Game.Entity.Attributes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace HelloMonoWorld.Game.Projectile
{
    public class EnergyBall : BasicProjectile
    {
        public EnergyBall(AsepriteDocument aseprite, Vector2 direction, float damage, float knockback, AbstractEntity owner, float damageInterval) : base(aseprite, direction, damage, knockback, owner)
        {
            this.SetAttribute(Attributes.DamageInterval, damageInterval);
            this.AttackTime = damageInterval;
        }

        public EnergyBall(AsepriteDocument aseprite, float damage, float knockback, float damageInterval) : this(aseprite, default, damage, knockback, null, damageInterval)
        {

        }

        public override void Update()
        {
            if (this.AttackTime > 0d)
            {
                this.AttackTime -= Time.DeltaTime;
            }
            base.Update();
            if (this.AttackTime <= 0) 
                this.AttackTime = this.GetAttributeValue(Attributes.DamageInterval);
        }

        public override void OnEntityHit(AbstractEntity other)
        {
            if (!(this.AttackTime <= 0d))
                return;
            if (other.Hurt(this.Owner, this, this.GetAttributeValue(Attributes.Damage)))
            {
                Sounds.PlaySoundVariated(this.GetHitSound(), 0.25f, 0.25f);
            }
        }

        protected override SoundEffect GetHitSound()
        {
            return Sounds.WaterHit;
        }
    }
}
