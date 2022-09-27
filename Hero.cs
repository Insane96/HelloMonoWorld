using HelloMonoWorld.Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloMonoWorld;

public class Hero : Entity
{
    public double attackTime = 0d;
    public Weapon weapon;

    public Hero() : base("hero", "stickman")
    {
        this.Position = new Vector2(80, Graphics.Height / 2);
        this.weapon = new("magic_bullet", 1f, 250f, 50f, 2f, this);
        this.attackTime = this.weapon.attackSpeed;
        this.OriginalColor = Color.DarkViolet;
    }

    public override void Update()
    {
        base.Update();
        if (this.attackTime > 0d)
        {
            this.attackTime -= Time.DeltaTime;
            if (this.attackTime <= 0d && this.weapon != null)
            {
                this.attackTime = this.weapon.attackSpeed;
                this.weapon.AttackNearest();
            }
        }
    }
}

