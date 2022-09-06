using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloMonoWorld;

public class Entity : GameObject
{
    public float movementSpeed = 100f;
    public Vector2 deltaMovement = Vector2.Zero;

    public Entity(string id) : base(id, id)
    {

    }

    public Entity(string id, string spriteName) : base(id, spriteName)
    {

    }

    public override void Initialize()
    {

    }

    public override void Update()
    {
        this.Move();
    }

    public void Move()
    {
        if (deltaMovement != Vector2.Zero)
            this.deltaMovement.Normalize();

        this.position += Vector2.Multiply(this.deltaMovement, (float)(movementSpeed * Time.DeltaTime));
    }
}
