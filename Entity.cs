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
    public Direction attackDirection = null;

    public Rectangle Bounds;

    public Vector2 LeftHand { get; private set; } = new(40, 54);
    public Vector2 RightHand { get; private set; } = new(4, 54);

    public Entity(string id) : base(id, id)
    {
        this.Bounds = new Rectangle((int)(this.position.X - (this.texture.Width * this.origin.X)), (int)this.position.Y, this.texture.Width, this.texture.Height);
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
        this.Bounds = new Rectangle((int)(this.position.X - (this.texture.Width * this.origin.X)), (int)this.position.Y, this.texture.Width, this.texture.Height);
    }

    public void Move()
    {
        if (deltaMovement != Vector2.Zero)
        {
            //this.deltaMovement.Normalize();
            //this.position += Vector2.Multiply(this.deltaMovement, (float)(movementSpeed * Time.DeltaTime));
            this.position += Vector2.Multiply(this.deltaMovement, (float)Time.DeltaTime);

            this.deltaMovement = Vector2.Zero;
        }
    }
}
