using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.Aseprite.Documents;
using MonoGame.Aseprite.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloMonoWorld.Game.Entity
{
    public class ZombieEnemy : AbstractEnemy
    {
        public ZombieEnemy(AsepriteDocument aseprite) : base(aseprite)
        {
            this.MovementSpeed = 50f;
            this.MaxHealth = 4f;
            this.OriginalColor = Color.FromNonPremultiplied(183, 138, 16, 255);
        }

        public override void Update()
        {
            base.Update();
        }

        public override bool Hurt(float damage, float knockback = 0)
        {
            if (base.Hurt(damage))
            {
                if (knockback != 0f)
                    this.Push(Direction.RIGHT.vector, knockback);
                return true;
            }
            return false;
        }
    }
}
