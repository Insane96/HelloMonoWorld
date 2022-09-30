using HelloMonoWorld.Engine;
using HelloMonoWorld.Game.Spell;
using Microsoft.Xna.Framework;
using System.Linq;

namespace HelloMonoWorld.Game;

public class Hero : Entity
{
    public Entity TargetEnemy { get; set; }

    public Hero() : base("hero", "stickman", Direction.RIGHT.vector)
    {
        Position = new Vector2(80, Graphics.Height / 2);
        BaseSpell = new(Spells.EnergyBall, this);
        OriginalColor = Color.DarkViolet;
    }

    public override void Update()
    {
        base.Update();

        this.BaseSpell.Update();
        if (this.TargetEnemy == null)
        {
            GetTarget();
        }
        else
        {
            if (this.TargetEnemy.IsDead())
                this.TargetEnemy = null;
            else
                this.BaseSpell.TryCast(Utils.GetDirection(this.Position, this.TargetEnemy.Position));
        }
    }

    public virtual void GetTarget()
    {
        GameObject nearestEnemy = GameObject.GetUpdatableGameObjects().OrderBy(g => g.Position.X).FirstOrDefault(g => g is AbstractEnemy);
        if (nearestEnemy == null)
            return;
        else
            this.TargetEnemy = (AbstractEnemy) nearestEnemy;
    }
}

