using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloMonoWorld;

public class Enemy : Entity
{
    public Enemy(string id, string spriteName) : base(id, spriteName)
    {
        this.movementSpeed = 90f;
        this.position = new Vector2(500, 500);
        this.health = 1f;
    }

    public override void Initialize()
    {
        base.Initialize();
        this.color = Color.OrangeRed;
    }

    public override void Update()
    {
        //this.deltaMovement = GetRelativeMovement(new Vector2(MainGame.player.position.X - this.position.X, MainGame.player.position.Y - this.position.Y));
        base.Update();
    }
}
