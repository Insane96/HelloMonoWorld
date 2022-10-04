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

        public static KeyboardState oldKeyBoardState = Keyboard.GetState();

        public static void TryToggleDebug(KeyboardState kState)
        {
            if (kState.IsKeyDown(Keys.F3) && !oldKeyBoardState.IsKeyDown(Keys.F3))
            {
                Debug = !Debug;
            }
        }

        public static void TryToggleMute(KeyboardState kState)
        {
            if (kState.IsKeyDown(Keys.NumPad0) && !oldKeyBoardState.IsKeyDown(Keys.NumPad0))
            {
                Volume = Math.Abs(Volume - 1);
            }
        }

        public static void TryIncreaseFontSize(KeyboardState kState)
        {
            if (kState.IsKeyDown(Keys.Add) && !oldKeyBoardState.IsKeyDown(Keys.Add))
            {
                IncreaseFontSize();
            }
        }

        public static void TryDecreaseFontSize(KeyboardState kState)
        {
            if (kState.IsKeyDown(Keys.Subtract) && !oldKeyBoardState.IsKeyDown(Keys.Subtract))
            {
                DecreaseFontSize();
            }
        }

        public static void TryFullScreen(KeyboardState kState)
        {
            if (kState.IsKeyDown(Keys.F11) && !oldKeyBoardState.IsKeyDown(Keys.F11))
            {
                Graphics.graphics.ToggleFullScreen();
            }
        }
    }
}
