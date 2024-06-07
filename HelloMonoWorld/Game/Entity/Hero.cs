using System.Linq;
using Engine;
using HelloMonoWorld.Game.Spell;
using Microsoft.Xna.Framework;

namespace HelloMonoWorld.Game.Entity;

public class Hero : AbstractEntity
{
    public AbstractEntity TargetEnemy { get; set; }

    public Hero() : base(Sprites.StickmanAnimatedAseprite, Direction.RIGHT.vector)
    {
        this.SetPosition(new Vector2(80, Graphics.Height / 2));
        this.BaseSpell = new(Spells.EnergyBall, this);
        this.OriginalColor = Color.DarkViolet;
    }

    public override void Update()
    {
        base.Update();

        this.BaseSpell.Update();
        if (this.TargetEnemy == null)
        {
            this.GetTarget();
        }
        else
        {
            if (this.TargetEnemy.IsDead())
                this.TargetEnemy = null;
            else
                this.BaseSpell.TryCast(Utils.GetDirection(this.GetPosition(), this.TargetEnemy.GetPosition()));
        }
    }

    public virtual void GetTarget()
    {
        GameObject nearestEnemy = GetUpdatableGameObjects().OrderBy(g => g.GetX()).FirstOrDefault(g => g is AbstractEnemy);
        if (nearestEnemy == null)
            return;
        else
            TargetEnemy = (AbstractEnemy)nearestEnemy;
    }
}

