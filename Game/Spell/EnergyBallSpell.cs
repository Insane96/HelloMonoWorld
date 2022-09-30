using HelloMonoWorld.Game.Projectile;
using HelloMonoWorld.Game.Projectile.Projectile;

namespace HelloMonoWorld.Game.Spell
{
    public class EnergyBallSpell : BasicSpell
    {
        public EnergyBallSpell(string id, float damage, float knockback, float projectileSpeed, float cooldown) : base(id, damage, knockback, projectileSpeed, cooldown)
        {
        }

        public override BasicProjectile GetProjectile()
        {
            return new EnergyBall("energy_ball", this.Damage, this.Knockback, 0.1d);
        }
    }
}
