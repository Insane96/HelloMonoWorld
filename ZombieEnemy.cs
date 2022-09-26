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
            this.MaxHealth = 2f;
            this.color = Color.FromNonPremultiplied(183, 138, 16, 255);
        }

        public override void Update()
        {
            if (!this.knockbacked)
                this.deltaMovement += GetRelativeMovement(Direction.LEFT.vector);
            base.Update();
        }

        public override bool Hurt(float damage, float knockback = 0)
        {
            if (base.Hurt(damage))
            {
                if (knockback != 0f)
                    this.Knockback(Direction.RIGHT.vector, knockback);
                return true;
            }
            return false;
        }
    }
}
