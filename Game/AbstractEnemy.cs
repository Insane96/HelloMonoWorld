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

namespace HelloMonoWorld.Game;

public class AbstractEnemy : Entity
{
    //TODO Use Weapon
    public float attackSpeed = 1f;
    public float attackDamage = 1f;

    public AbstractEnemy(string id, string spriteName) : base(id, spriteName)
    {
        AttackTime = attackSpeed;
        ShouldDrawHealth = true;
    }

    public override void Update()
    {
        if (Position.X <= 300)
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
        base.Update();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        spriteBatch.Draw(
            Texture,
            Position,
            null,
            Color,
            0f,
            new Vector2(Texture.Width * Origin.X, Texture.Height * Origin.Y),
            Vector2.One,
            SpriteEffect,
            0f
        );

    }
}

