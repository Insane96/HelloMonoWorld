using HelloMonoWorld.Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloMonoWorld.Game
{
    internal class Options
    {
        public static float Volume { get; set; } = 0f;

        public static int FontSize { get; private set; } = 1;

        public static SpriteFont GetFont() => UIText.SpriteFonts[FontSize];

        public static void IncreaseFontSize() => FontSize = Math.Clamp(FontSize + 1, 0, UIText.SpriteFonts.Length - 1);

        public static void DecreaseFontSize() => FontSize = Math.Clamp(FontSize - 1, 0, UIText.SpriteFonts.Length - 1);

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

        //TODO Add a class to handle one time key down and key up
        private static bool NumPad0KeyDown = false;

        public static void TryToggleMute(KeyboardState kState)
        {
            if (kState.IsKeyDown(Keys.NumPad0))
            {
                if (!NumPad0KeyDown)
                {
                    NumPad0KeyDown = true;
                    Volume = Math.Abs(Volume - 1);
                }
            }
            else
            {
                if (NumPad0KeyDown)
                {
                    NumPad0KeyDown = false;
                }
            }
        }

        private static bool NumPadAddDown = false;

        public static void TryIncreaseFontSize(KeyboardState kState)
        {
            if (kState.IsKeyDown(Keys.Add))
            {
                if (!NumPadAddDown)
                {
                    NumPadAddDown = true;
                    IncreaseFontSize();
                }
            }
            else
            {
                if (NumPadAddDown)
                {
                    NumPadAddDown = false;
                }
            }
        }

        private static bool NumPadMinusDown = false;

        public static void TryDecreaseFontSize(KeyboardState kState)
        {
            if (kState.IsKeyDown(Keys.Subtract))
            {
                if (!NumPadMinusDown)
                {
                    NumPadMinusDown = true;
                    DecreaseFontSize();
                }
            }
            else
            {
                if (NumPadMinusDown)
                {
                    NumPadMinusDown = false;
                }
            }
        }

        private static bool F11Down = false;

        public static void TryFullScreen(KeyboardState kState)
        {
            if (kState.IsKeyDown(Keys.F11))
            {
                if (!F11Down)
                {
                    F11Down = true;
                    Graphics.graphics.ToggleFullScreen();
                }
            }
            else
            {
                if (F11Down)
                {
                    F11Down = false;
                }
            }
        }
    }
}
