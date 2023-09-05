using HelloMonoWorld.Engine;
using HelloMonoWorld.Game.Entity;
using Microsoft.Xna.Framework;

namespace HelloMonoWorld.Game.Spell;

public class SpellInstance
{
    BasicSpell Spell { get; set; }
    double CurrentCooldown { get; set; }
    AbstractEntity Owner { get; set; }

    public SpellInstance(BasicSpell spell, AbstractEntity owner)
    {
        this.Spell = spell;
        this.Owner = owner;
    }

    public void Update()
    {
        if (this.CurrentCooldown > 0d)
            this.CurrentCooldown -= Time.DeltaTime;
    }

    public void TryCast(Vector2? direction)
    {
        if (this.CurrentCooldown <= 0d)
        {
            if (direction.HasValue)
                this.Spell.Cast(this.Owner, direction.Value);
            else
                this.Spell.Cast(this.Owner, this.Owner.AttackDirection.Value);
            //TODO Use the cooldown as an attribute on the player and the spell just changes the base cooldown
            this.CurrentCooldown = this.Spell.Cooldown;
        }
    }
}
