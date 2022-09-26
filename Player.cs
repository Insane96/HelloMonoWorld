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
        this.Position = new Vector2(200, Graphics.Height / 2);
        this.weapon = new("sword", "sword", 1f, 250f, 50f, 2f, this);
        this.OriginalColor = Color.Black;
        this.MaxHealth = 10;
        this.MovementSpeed = 200f;
    }

    public override void Update()
    {
        CheckMovementInput();
        CheckAttackInput();
        if (this.attackTime > 0d)
        {
            this.attackTime -= Time.DeltaTime;
        }
        base.Update();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        Engine.Engine.DrawText(Options.GetFont(), $"{this.Health:0.#} / {this.MaxHealth:0.#}", new Vector2(0, Graphics.Height), Color.DarkRed, new Vector2(0, 1f));
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

        this.DeltaMovement += GetRelativeMovement(inputMovement);
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
            this.weapon.Attack();
        }
    }
}
