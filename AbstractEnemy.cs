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

namespace HelloMonoWorld;

public class AbstractEnemy : Entity
{
    public float attackSpeed = 1f;
    public float attackDamage = 1f;
    public double attackTime;

    public AbstractEnemy(string id, string spriteName) : base(id, spriteName)
    {
        this.attackTime = this.attackSpeed;
        this.ShouldDrawHealth = true;
    }

    public override void Update()
    {
        if (this.Position.X <= 300)
        {
            this.DeltaMovement = Vector2.Zero;
            if (this.attackTime > 0d)
            {
                this.attackTime -= Time.DeltaTime;
                if (this.attackTime <= 0d)
                {
                    MainGame.player.Hurt(this.attackDamage);
                    this.attackTime = this.attackSpeed;
                }
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

