using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelloMonoWorld.Engine;
using Microsoft.Xna.Framework.Graphics;
using System.Transactions;
using MonoGame.Aseprite.Graphics;
using MonoGame.Aseprite.Documents;

namespace HelloMonoWorld.Game.Entity;

public class AbstractEnemy : AbstractEntity
{
    //TODO Use Spell
    public float attackSpeed = 2f;
    public float attackDamage = 1f;

    public AbstractEnemy(AsepriteDocument aseprite) : base(aseprite, Direction.LEFT.vector)
    {
        AttackTime = attackSpeed;
        ShouldDrawHealth = true;
    }

    public override void Update()
    {
        if (!Knockbacked)
        {
            if (this.GetX() > 300)
            {
                DeltaMovement += GetRelativeMovement(Direction.LEFT.vector);
            }
            else
            {
                DeltaMovement = Vector2.Zero;
                if (AttackTime > 0d)
                {
                    AttackTime -= Time.DeltaTime;
                }
                else
                {
                    MainGame.player.Hurt(attackDamage);
                    AttackTime = attackSpeed;
                }
            }
        }
        base.Update();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }
}

