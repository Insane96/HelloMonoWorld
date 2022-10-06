using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloMonoWorld.Game.Spell
{
    public static class Spells
    {
        public static BasicSpell MagicBullet = new(1f, 50f, 250f, 2f);
        public static EnergyBallSpell EnergyBall = new(1f, 0f, 350f, 5f);
    }
}
