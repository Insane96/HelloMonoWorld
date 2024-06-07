using System;
using Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HelloMonoWorld.Game
{
    internal class Options
    {
        private static float volume = 0.5f;
        public static float Volume { get => Mute ? 0f : volume; set => volume = value; }
        public static bool Mute { get; set; }

        public static int FontSize { get; private set; } = 1;

        public static SpriteFont GetFont() => UiText.SpriteFonts[FontSize];

        public static void IncreaseFontSize() => FontSize = Math.Clamp(FontSize + 1, 0, UiText.SpriteFonts.Length - 1);

        public static void DecreaseFontSize() => FontSize = Math.Clamp(FontSize - 1, 0, UiText.SpriteFonts.Length - 1);

        public static bool Debug;

        public static void TryToggleDebug()
        {
            if (Input.IsKeyPressed(Keys.F3))
            {
                Debug = !Debug;
            }
        }

        public static void TryToggleMute()
        {
            if (Input.IsKeyPressed(Keys.NumPad0))
            {
                Mute = !Mute;
            }
        }

        public static void TryIncreaseFontSize()
        {
            if (Input.IsKeyPressed(Keys.Add))
            {
                IncreaseFontSize();
            }
        }

        public static void TryDecreaseFontSize()
        {
            if (Input.IsKeyPressed(Keys.Subtract))
            {
                DecreaseFontSize();
            }
        }

        public static void TryFullScreen()
        {
            if (Input.IsKeyPressed(Keys.F11))
            {
                Graphics.GraphicsDeviceManager.ToggleFullScreen();
            }
        }
    }
}
