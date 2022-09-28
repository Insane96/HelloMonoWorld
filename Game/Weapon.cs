using HelloMonoWorld.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.Linq;

namespace HelloMonoWorld.Game;

public class Weapon
{
    private double AttackTime { get; set; }
    public Entity Wielder;

    public Weapon(SpellDefinition spell, Entity wielder)
    {
        this.wielder = wielder;
    }

    private SoundEffect GetAttackSound()
    {
        return Sounds.SwordSwing;
    }
}
