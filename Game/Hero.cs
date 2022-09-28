using HelloMonoWorld.Engine;
using Microsoft.Xna.Framework;

namespace HelloMonoWorld;

public class Hero : Entity
{
    public Hero() : base("hero", "stickman")
    {
        this.Position = new Vector2(80, Graphics.Height / 2);
        this.Weapon = new("magic_bullet", 0.5f, 350f, 30f, 0.5f, this);
        this.AttackTime = this.Weapon.AttackSpeed;
        this.OriginalColor = Color.DarkViolet;
    }

    public override void Update()
    {
        base.Update();
        if (this.AttackTime <= 0d && this.Weapon != null)
        {
            this.AttackTime = this.Weapon.AttackSpeed;
            this.Weapon.AttackNearest();
        }
    }
}

