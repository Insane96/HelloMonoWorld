using HelloMonoWorld.Game.Projectile;
using HelloMonoWorld.Game.Projectile.Projectile;
using Microsoft.Xna.Framework.Audio;

namespace HelloMonoWorld.Game.Spell
{
    public class EnergyBallSpell : BasicSpell
    {
        public EnergyBallSpell(string id, float damage, float knockback, float projectileSpeed, float cooldown) : base(id, damage, knockback, projectileSpeed, cooldown)
        {
        }

        public override BasicProjectile GetProjectile()
        {
            return new EnergyBall("energy_ball", this.Damage, this.Knockback, 0.2d);
        }

        protected override SoundEffect GetCastSound()
        {
            return Sounds.WaterSpell;
        }
    }
}
