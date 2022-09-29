using HelloMonoWorld.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloMonoWorld.Game.Spell
{
    public class SpellDefinition
    {
        public string Id { get; set; }
        public float Damage { get; set; }
        public float Knockback { get; set; }
        public float ProjectileSpeed { get; set; }
        public float AttackSpeed { get; set; }

        public SpellDefinition(string id, float damage, float knockback, float projectileSpeed, float attackSpeed)
        {
            Id = id;
            Damage = damage;
            Knockback = knockback;
            ProjectileSpeed = projectileSpeed;
            AttackSpeed = attackSpeed;
        }

        public virtual void Attack()
        {
            this.GetAttackSound().Play(0.5f * Options.Volume, Mth.NextFloat(MainGame.random, -0.25f, 0.25f), 0f);
            Projectile projectile = new("projectile", "magic_bullet", Direction.RIGHT.vector, this.Damage, this.Knockback, this.wielder)
            {
                Position = new Vector2(this.wielder.Position.X - (this.wielder.Texture.Width * this.wielder.Origin.X) + this.wielder.LeftHand.X, this.wielder.Position.Y - (this.wielder.Texture.Height * this.wielder.Origin.Y) + this.wielder.LeftHand.Y),
                MovementSpeed = projectileSpeed
            };
            GameObject.Instantiate(projectile);
        }

        public virtual void AttackNearest()
        {
            GameObject nearestEnemy = GameObject.GetUpdatableGameObjects().OrderBy(g => g.Position.X).FirstOrDefault(g => g is AbstractEnemy enemy);
            if (nearestEnemy == null)
                return;

            this.GetAttackSound().Play(0.5f * Options.Volume, Mth.NextFloat(MainGame.random, -0.25f, 0.25f), 0f);
            Projectile projectile = new("projectile", "magic_bullet", Vector2.Normalize(nearestEnemy.Position - this.wielder.Position.Sum(this.wielder.LeftHand.X)), this.damage, this.knockback, this.wielder)
            {
                Position = new Vector2(this.wielder.Position.X - (this.wielder.Texture.Width * this.wielder.Origin.X) + this.wielder.LeftHand.X, this.wielder.Position.Y - (this.wielder.Texture.Height * this.wielder.Origin.Y) + this.wielder.LeftHand.Y),
                MovementSpeed = projectileSpeed
            };
            GameObject.Instantiate(projectile);
        }

        private SoundEffect GetAttackSound()
        {
            return Sounds.SwordSwing;
        }
    }
}
