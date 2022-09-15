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
        base.Update();
    }
}

