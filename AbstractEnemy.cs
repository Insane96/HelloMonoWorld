using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloMonoWorld;

public class AbstractEnemy : Entity
{
    public AbstractEnemy(string id, string spriteName) : base(id, spriteName)
    {

    }

    public override void Update()
    {
        //this.deltaMovement = GetRelativeMovement(new Vector2(MainGame.player.position.X - this.position.X, 0));
        base.Update();
    }
}

