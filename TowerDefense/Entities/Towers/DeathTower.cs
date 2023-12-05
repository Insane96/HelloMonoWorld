using System.Collections.Generic;
using System.Linq;
using Engine;
using Microsoft.Xna.Framework.Graphics;
using TowerDefense.Entities.Enemies;
using TowerDefense.Registry;

namespace TowerDefense.Entities.Towers;

public class DeathTower : Tower
{
    public DeathTower() : base(Sprites.GetAnimatedSprite(Sprites.DeathTower, "idle"), 2f, 2f, 125f, 0.01f)
    {
        this.UltimateDuration = 1f;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }

    public override void Attack()
    {
        if (this.IsUlting)
        {
            
        }
        else
        {
            this.Cooldown -= Time.DeltaTime;
            if (this.Cooldown <= 0d && this.LockedOn != null)
            {
                DeathPool deathPool = new(this)
                {
                    Position = this.LockedOn.Position
                };
                Instantiate(deathPool);
                this.Cooldown = this.BaseAttackSpeed;
            }
        }
    }

    public override void UpdateUlting()
    {
        if (this.IsUlting)
        {
            this.UltingTimer -= Time.DeltaTime;
            if (this.UltingTimer <= 0f)
            {
                IEnumerable<AbstractEnemy> abstractEnemies = GetUpdatableGameObjects().Where(gameObject => gameObject is AbstractEnemy).Cast<AbstractEnemy>();
                foreach (AbstractEnemy abstractEnemy in abstractEnemies)
                {
                    abstractEnemy.Hurt(20f);
                }
                this.IsUlting = false;
                this.UltimateCharge = 0f;
                Time.TimeScale = 1f;
            }
        }
    }
}