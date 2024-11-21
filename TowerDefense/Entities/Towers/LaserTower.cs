using System;
using Engine;
using Engine.ExtensionMethods;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using TowerDefense.Registry;

namespace TowerDefense.Entities.Towers;

public class LaserTower : Tower
{
    public bool IsAimingUlt { get; protected set; }
    public bool HasUltDamaged { get; protected set; }
    public double TimeLockedOn { get; protected set; }
    public LaserTower() : base(Sprites.LaserTower)
    {
        this.BaseAttackSpeed = 0.1f;
        this.BaseAttackDamage = 0.1f;
        this.BaseRange = 350f;
        this.UltimateChargeOnHit = 0.001f;
        this.UltimateDuration = 3f;
    }

    public override void Attack()
    {
        this.Cooldown -= Time.DeltaTime;
        if (this.LockedOn == null || this.LockedOn.IsDead())
        {
            this.TimeLockedOn = 0f;
            return;
        }
        if (this.IsUlting || this.Cooldown > 0d) 
            return;

        this.TimeLockedOn += Time.DeltaTime;
        this.LockedOn.Hurt((float)(this.BaseAttackDamage * this.TimeLockedOn));
        if (this.UltimateCharge < 1f)
            this.UltimateCharge += this.UltimateChargeOnHit;
        this.Cooldown = this.BaseAttackSpeed;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        if (!this.IsUlting && this.LockedOn != null)
            spriteBatch.DrawLine(this.Position, this.LockedOn.Position, Color.White, 2f);
        if (this.IsAimingUlt)
        {
            spriteBatch.DrawLine(this.Position, Input.MouseState.Position.ToVector2().ExtendFrom(this.Position, 1500f), Color.FromNonPremultiplied(51, 204, 255, 64), 35f);
        }
    }

    public override void OnMouseClickedOn()
    {
        if (this.UltimateCharge < 1f) 
            return;
        if (!this.IsAimingUlt)
        {
            this.IsAimingUlt = true;
            //MainGame.IsAimingUlt = this;
            Time.TimeScale = 0.25f;
        }
        else
        {
            this.IsAimingUlt = false;
            Time.TimeScale = 1f;
        }
    }

    public override void OnMouseClick()
    {
        if (this.IsMouseOver())
        {
            this.OnMouseClickedOn();
        }
        else if (this.IsAimingUlt)
        {
            Time.TimeScale = 1f;
            this.LockedOn = null;
            
            var deltaX = Input.MouseState.X - this.GetX();
            var deltaY = Input.MouseState.Y - this.GetY();
            float rad = (float)Math.Atan2(deltaY, deltaX);
            LaserAbility ability = new(this.UltimateDuration)
            {
                Position = this.Position,
                Sprite =
                {
                    Rotation = rad
                }
            };
            this.IsAimingUlt = false;
            Instantiate(ability);
            this.IsUlting = true;
            this.UltingTimer = this.UltimateDuration;
        }
    }
}