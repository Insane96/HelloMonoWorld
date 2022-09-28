using HelloMonoWorld.Engine;
using Microsoft.Xna.Framework;

namespace HelloMonoWorld.Game;

public class Hero : Entity
{
    public Hero() : base("hero", "stickman")
    {
        Position = new Vector2(80, Graphics.Height / 2);
        Weapon = new("magic_bullet", 0.5f, 350f, 30f, 0.5f, this);
        AttackTime = Weapon.AttackSpeed;
        OriginalColor = Color.DarkViolet;
    }

    public override void Update()
    {
        base.Update();
        if (AttackTime <= 0d && Weapon != null)
        {
            AttackTime = Weapon.AttackSpeed;
            Weapon.AttackNearest();
        }
    }
}

