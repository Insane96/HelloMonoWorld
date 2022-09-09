using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace HelloMonoWorld;

public class Player : Entity 
{
    public double attackTime = 0d;
    public Weapon weapon;

    public Player() : base("player", "stickman")
    {
        this.position = new Vector2(Graphics.screenWidth / 2, Graphics.screenHeight / 2);
        this.weapon = new("sword", "sword", 0.5d, this);
    }

    public override void Initialize()
    {
        this.color = Color.Black;
    }

    public override void Update()
    {
        CheckMovementInput();
        CheckAttackInput();
        if (this.attackTime > 0d)
        {
            this.attackTime -= Time.DeltaTime;
            if (attackTime <= 0d)
                this.weapon.Hide();
        }
        base.Update();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }

    private void CheckMovementInput()
    {
        var kstate = Keyboard.GetState();

        Vector2 inputMovement = Vector2.Zero;
        foreach (Direction direction in Direction.directions)
        {
            if (kstate.IsKeyDown(direction.key))
            {
                inputMovement += direction.vector;
            }
        }

        this.deltaMovement += GetRelativeMovement(inputMovement);
    }

    private void CheckAttackInput()
    {
        var kstate = Keyboard.GetState();
        if (kstate.IsKeyDown(Keys.Left))
        {
            this.attackTime = this.weapon.attackSpeed;
            this.attackDirection = Direction.LEFT;
            this.weapon.Attack();
        }
        else if (kstate.IsKeyDown(Keys.Right))
        {
            this.attackTime = this.weapon.attackSpeed;
            this.attackDirection = Direction.RIGHT;
            this.weapon.Attack();
        }
    }
}
