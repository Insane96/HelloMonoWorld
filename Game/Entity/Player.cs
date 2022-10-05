using HelloMonoWorld.Engine;
using HelloMonoWorld.Game.Spell;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace HelloMonoWorld.Game.Entity;

public class Player : AbstractEntity
{
    public Player() : base("stickman", Direction.RIGHT.vector)
    {
        Position = new Vector2(200, Graphics.Height / 2);
        BaseSpell = new(Spells.MagicBullet, this);
        OriginalColor = Color.Black;
        MaxHealth = 100;
        MovementSpeed = 222f;
    }

    public override void Update()
    {
        base.Update();
        CheckMovementInput();
        UpdateAttack();
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

    private void UpdateAttack()
    {
        BaseSpell.Update();
        var kstate = Keyboard.GetState();
        if (kstate.IsKeyDown(Keys.Right))
        {
            BaseSpell.TryCast(AttackDirection);
        }

        /*if (kstate.IsKeyDown(Keys.Space))
        {
            AttackTime = EnergyWeapon.AttackSpeed;
            EnergyWeapon.Attack();
        }*/
    }
}
