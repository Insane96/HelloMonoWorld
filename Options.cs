using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloMonoWorld
{
    internal class Options
    {
        public static bool Debug;
        //TODO Add a class to handle one time key down and key up
        private static bool F3KeyDown = false;

        public static void TryToggleDebug(KeyboardState kState)
        {
            if (kState.IsKeyDown(Keys.F3))
            {
                if (!F3KeyDown)
                {
                    F3KeyDown = true;
                    Debug = !Debug;
                }
            }
            else
            {
                if (F3KeyDown)
                {
                    F3KeyDown = false;
                }
            }
        }
    }
}
