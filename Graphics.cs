using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HelloMonoWorld
{
    internal static class Graphics
    {
        public static int ScreenWidth { get => graphics.GraphicsDevice.Viewport.Width; }

        public static int ScreenHeight { get => graphics.GraphicsDevice.Viewport.Height; }

        public static int UnscaledWidth { get => 1280; }
        public static int UnscaledHeight { get => 720; }

        public static float ScaledRatio { get => (float)ScreenWidth / (float)UnscaledWidth; }

        public static GraphicsDeviceManager graphics;
    }
}
