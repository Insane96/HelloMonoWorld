using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloMonoWorld.Game.Spell
{
    public static class Spells
    {
        public static SpellDefinition MagicBullet = new("magic_bullet", 1f, 50f, 250f, 1f);
        public static SpellDefinition EnergyBall = new("energy_ball", 1f, 50f, 250f, 1f);
    }
}
