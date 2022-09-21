﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HelloMonoWorld.Engine
{
    internal static class Graphics
    {
        public static int ViewportWidth { get => graphics.GraphicsDevice.Viewport.Width; }

        public static int ViewportHeight { get => graphics.GraphicsDevice.Viewport.Height; }

        public static int Width { get; private set; }
        public static int Height { get; private set; }

        public static float ScaledRatio { get => ViewportWidth / (float)Width; }

        public static GraphicsDeviceManager graphics;

        public static void Init(Game game, int width, int height, int viewportWidth, int viewportHeight)
        {
            Width = width;
            Height = height;

            graphics = new(game)
            {
                PreferredBackBufferWidth = viewportWidth,
                PreferredBackBufferHeight = viewportHeight,
            };
            graphics.ApplyChanges();
        }
    }
}
