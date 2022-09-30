using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloMonoWorld.Game
{
    public class ZombieEnemy : AbstractEnemy
    {
        public ZombieEnemy(string id, string spriteName) : base(id, spriteName)
        {
            MovementSpeed = 50f;
            //this.position = new Vector2(500, 500);
            MaxHealth = 4f;
            OriginalColor = Color.FromNonPremultiplied(183, 138, 16, 255);
        }

        public override void Update()
        {
            if (!Knockbacked)
                DeltaMovement += GetRelativeMovement(Direction.LEFT.vector);
            base.Update();
        }

        public override bool Hurt(float damage, float knockback = 0)
        {
            if (base.Hurt(damage))
            {
                if (knockback != 0f)
                    Push(Direction.RIGHT.vector, knockback);
                return true;
            }
            return false;
        }
    }
}
