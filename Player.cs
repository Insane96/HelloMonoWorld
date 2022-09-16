using HelloMonoWorld.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        this.position = new Vector2(200, Graphics.ScreenHeight / 2);
        this.weapon = new("sword", "sword", 1d, 0.8d, this);
        Engine.Engine.Instantiate(this.weapon);
        this.color = Color.Black;
        this.movementSpeed = 200f;
    }

    public override void Update()
    {
        CheckMovementInput();
        CheckAttackInput();
        if (this.attackTime > 0d)
        {
            this.attackTime -= Time.DeltaTime;
            if (attackTime <= 0d)
            {
                this.weapon.HideAndDisable();
            }
        }
        base.Update();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        Engine.Engine.DrawText(spriteBatch, MainGame.font, $"{this.health:0.#} HP", this.position.Sum(0, -this.Bounds.Height / 2 - 25), Color.DarkRed, new Vector2(0.5f), Color.Black);
        base.Draw(spriteBatch);
    }

    private void CheckMovementInput()
    {
        var kstate = Keyboard.GetState();

        Vector2 inputMovement = Vector2.Zero;
        foreach (Direction direction in Direction.upDownDirections)
        {
            if (kstate.IsKeyDown(direction.key))
            {
                inputMovement += direction.vector;
            }
        }

        this.deltaMovement += GetRelativeMovement(inputMovement);
    }

    public override void OnDeath()
    {
        //Don't destroy the player, just hide him
        this.HideAndDisable();
    }

    private void CheckAttackInput()
    {
        if (this.attackTime > 0d)
            return;
        var kstate = Keyboard.GetState();
        if (kstate.IsKeyDown(Keys.Right))
        {
            this.attackTime = this.weapon.attackSpeed;
            this.attackDirection = Direction.RIGHT;
            this.weapon.Attack();
        }
    }
}
