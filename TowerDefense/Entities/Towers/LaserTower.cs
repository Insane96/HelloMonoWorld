using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using TowerDefense.Registry;

namespace TowerDefense.Entities.Towers;

public class LaserTower : Tower
{
    public LaserTower() : base(Sprites.GetAnimatedSprite(Sprites.LaserTower, "idle"), 0.1f, 0.1f, 400f, 0.001f)
    {
    }

    public override void Attack()
    {
        this.Cooldown -= Time.DeltaTime;
        if (this.IsUlting || this.Cooldown > 0d || this.LockedOn == null) 
            return;
        /*Projectile projectile = new(Sprites.GetAnimatedSprite(Sprites.Arrow, "idle"), this, 250f)
            {
                Position = this.Position
            };
            Vector2 dir = new(this.LockedOn.GetX() - this.GetX(), this.LockedOn.GetY() - this.GetY());
            dir.Normalize();
            projectile.Direction = dir;
            Instantiate(projectile);
            this.Cooldown = this.BaseAttackSpeed;
            if (this.IsUlting)
                this.Cooldown /= 8f;*/
            
        this.LockedOn.Hurt(this.BaseAttackDamage);
        this.UltimateCharge += this.UltimateChargeOnHit;
        this.Cooldown = this.BaseAttackSpeed;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        if (this.LockedOn != null)
            spriteBatch.DrawLine(this.Position, this.LockedOn.Position, Color.White, 2f);
    }

    public override void UpdateUlting()
    {
        base.UpdateUlting();
    }
}