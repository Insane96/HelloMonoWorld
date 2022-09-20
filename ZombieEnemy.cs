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
            this.health = 1f;
            this.color = Color.FromNonPremultiplied(183, 138, 16, 255);
        }

        public override void Update()
        {
            if (!this.knockbacked)
                this.deltaMovement += GetRelativeMovement(Direction.LEFT.vector);
            base.Update();
        }

        public override bool Hurt(double damage, double knockback = 50d)
        {
            if (base.Hurt(damage, immunityTime))
            {
                this.Knockback(Direction.RIGHT.vector, (float)knockback);
                return true;
            }
            return false;
        }
    }
}
