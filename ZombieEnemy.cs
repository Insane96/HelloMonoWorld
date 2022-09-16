using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloMonoWorld
{
    public class ZombieEnemy : AbstractEnemy
    {
        public ZombieEnemy(string id, string spriteName) : base(id, spriteName)
        {
            this.movementSpeed = 50f;
            //this.position = new Vector2(500, 500);
            this.health = 10f;
            this.color = Color.FromNonPremultiplied(183, 138, 16, 255);
        }

        public override void Update()
        {
            if (!this.knockbacked)
                this.deltaMovement += GetRelativeMovement(new Vector2(MainGame.player.position.X - this.position.X, 0));
            base.Update();
        }

        public override bool Hurt(double damage, double immunityTime = 0.5)
        {
            if (base.Hurt(damage, immunityTime))
            {
                this.Knockback(Direction.RIGHT.vector, (float)damage * 50);
                return true;
            }
            return false;
        }
    }
}
