using HelloMonoWorld.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace HelloMonoWorld.Game;

public class Player : Entity
{
    EnergyWeapon EnergyWeapon { get; set; }

    public Player() : base("player", "stickman")
    {
        Position = new Vector2(200, Graphics.Height / 2);
        Weapon = new("magic_bullet", 1f, 250f, 50f, 2f, this);
        EnergyWeapon = new("energy_ball", 1f, 400f, 0f, 5f, this);
        OriginalColor = Color.Black;
        MaxHealth = 10;
        MovementSpeed = 222f;
    }

    public override void Update()
    {
        base.Update();
        CheckMovementInput();
        CheckAttackInput();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        MonoEngine.DrawText(Options.GetFont(), $"{Health:0.#} / {MaxHealth:0.#}", new Vector2(0, Graphics.Height), Color.DarkRed, new Vector2(0, 1f));
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

        DeltaMovement += GetRelativeMovement(inputMovement);
    }

    public override void OnDeath()
    {
        //Don't destroy the player, just hide him
        HideAndDisable();
    }

    private void CheckAttackInput()
    {
        if (AttackTime > 0d)
            return;
        var kstate = Keyboard.GetState();
        if (kstate.IsKeyDown(Keys.Right))
        {
            AttackTime = Weapon.AttackSpeed;
            Weapon.Attack();
        }

        if (kstate.IsKeyDown(Keys.Space))
        {
            AttackTime = EnergyWeapon.AttackSpeed;
            EnergyWeapon.Attack();
        }
    }
}
