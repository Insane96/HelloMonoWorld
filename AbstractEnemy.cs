using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelloMonoWorld.Engine;

namespace HelloMonoWorld;

public class AbstractEnemy : Entity
{

    public double attackSpeed = 1d;
    public double attackTime;
    public double attackDamage = 1d;

    public AbstractEnemy(string id, string spriteName) : base(id, spriteName)
    {
        this.attackTime = this.attackSpeed;
    }

    public override void Update()
    {
        if (this.position.X <= 300)
        {
            this.deltaMovement = Vector2.Zero;
            if (this.attackTime > 0d)
            {
                this.attackTime -= Time.DeltaTime;
                if (this.attackTime <= 0d)
                {
                    MainGame.player.Hurt(this.attackDamage, 0d);
                    this.attackTime = this.attackSpeed;
                }
            }
        }
        base.Update();
    }
}

