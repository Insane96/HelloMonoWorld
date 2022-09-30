using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloMonoWorld.Game.Spell
{
    public static class Spells
    {
        public static BasicSpell MagicBullet = new("magic_bullet", 1f, 50f, 250f, 1f);
        public static EnergyBallSpell EnergyBall = new("energy_ball", 0.5f, 0f, 350f, 7f);
    }
}
