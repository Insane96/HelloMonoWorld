using HelloMonoWorld.Engine;
using HelloMonoWorld.Game.Spell;
using Microsoft.Xna.Framework;
using System.Linq;

namespace HelloMonoWorld.Game.Entity;

public class Hero : AbstractEntity
{
    public AbstractEntity TargetEnemy { get; set; }

    public Hero() : base("stickman", Direction.RIGHT.vector)
    {
        Position = new Vector2(80, Graphics.Height / 2);
        BaseSpell = new(Spells.EnergyBall, this);
        OriginalColor = Color.DarkViolet;
    }

    public override void Update()
    {
        base.Update();

        BaseSpell.Update();
        if (TargetEnemy == null)
        {
            GetTarget();
        }
        else
        {
            if (TargetEnemy.IsDead())
                TargetEnemy = null;
            else
                BaseSpell.TryCast(Utils.GetDirection(Position, TargetEnemy.Position));
        }
    }

    public virtual void GetTarget()
    {
        GameObject nearestEnemy = GetUpdatableGameObjects().OrderBy(g => g.Position.X).FirstOrDefault(g => g is AbstractEnemy);
        if (nearestEnemy == null)
            return;
        else
            TargetEnemy = (AbstractEnemy)nearestEnemy;
    }
}

