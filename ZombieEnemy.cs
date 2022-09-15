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
            this.movementSpeed = 90f;
            this.position = new Vector2(500, 500);
            this.health = 1f;
            this.color = Color.FromNonPremultiplied(183, 138, 16, 255);
        }
    }
}
