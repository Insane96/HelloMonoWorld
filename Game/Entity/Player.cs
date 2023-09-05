using System.Linq;
using HelloMonoWorld.Engine;
using HelloMonoWorld.Game.Spell;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HelloMonoWorld.Game.Entity;

public class Player : AbstractEntity
{
    private int Gold { get; set; }

    public Player() : base(Sprites.StickmanAnimatedAseprite, Direction.RIGHT.vector)
    {
        this.SetPosition(new Vector2(200, Graphics.Height / 2f));
        this.BaseSpell = new SpellInstance(Spells.MagicBullet, this);
        this.OriginalColor = Color.Black;
        this.SetMaxHealth(100);
        this.GetAttribute(Attributes.Attributes.MovementSpeed).BaseValue = 100f;
    }

    public override void Update()
    {
        base.Update();
        this.CheckMovementInput();
        this.UpdateAttack();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        MonoEngine.DrawText(Options.GetFont(), $"{this.Health:0.#} / {this.GetAttributeValue(Attributes.Attributes.MaxHealth):0.#}", Origins.GetScreenPosition(Origins.BottomLeft), Color.DarkRed, Origins.BottomLeft);
        MonoEngine.DrawText(Options.GetFont(), $"Gold: {this.Gold}", Origins.GetScreenPosition(Origins.BottomRight), Color.Gold, Origins.BottomRight);
        if (this.IsDead())
            MonoEngine.DrawText(Options.GetFont(), $"Game Over", Origins.GetScreenPosition(Origins.Center), Color.DarkRed, Origins.Center);
        base.Draw(spriteBatch);
    }

    private void CheckMovementInput()
    {
        Vector2 inputMovement = Direction.upDownDirections
            .Where(direction => Input.IsKeyDown(direction.key))
            .Aggregate(Vector2.Zero, (current, direction) => current + direction.vector);

        this.DeltaMovement += this.GetRelativeMovement(inputMovement);
    }

    public void AddGold(int gold)
    {
        this.Gold += gold;
    }

    public override void OnDeath()
    {
        //Don't destroy the player, just disable him and pause the game
        Time.TimeScale = 0f;
        this.Disable();
    }

    private void UpdateAttack()
    {
        this.BaseSpell.Update();
        if (Input.IsKeyDown(Keys.Right))
        {
            this.BaseSpell.TryCast(this.AttackDirection);
        }
    }
}
