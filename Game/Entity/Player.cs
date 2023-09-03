using System.Linq;
using HelloMonoWorld.Engine;
using HelloMonoWorld.Game.Spell;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HelloMonoWorld.Game.Entity;

public class Player : AbstractEntity
{
    public Player() : base(Sprites.StickmanAnimatedAseprite, Direction.RIGHT.vector)
    {
        this.SetPosition(new Vector2(200, Graphics.Height / 2f));
        this.BaseSpell = new SpellInstance(Spells.MagicBullet, this);
        this.OriginalColor = Color.Black;
        this.MaxHealth = 100;
        this.MovementSpeed = 222f;
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
        Vector2 inputMovement = Direction.upDownDirections
            .Where(direction => Input.IsKeyDown(direction.key))
            .Aggregate(Vector2.Zero, (current, direction) => current + direction.vector);

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
        if (Input.IsKeyDown(Keys.Right))
        {
            BaseSpell.TryCast(AttackDirection);
        }
    }
}
