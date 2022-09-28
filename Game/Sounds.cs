using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace HelloMonoWorld
{
    public static class Sounds
    {
        public static SoundEffect SwordSwing { get; private set; }
        public static SoundEffect Hit { get; private set; }
        public static SoundEffect HitPunch { get; private set; }

        public static void LoadContent(ContentManager contentManager)
        {
            SwordSwing = contentManager.Load<SoundEffect>("sounds/swing");
            Hit = contentManager.Load<SoundEffect>("sounds/hit");
            HitPunch = contentManager.Load<SoundEffect>("sounds/hit_punch");
        }
    }
}
