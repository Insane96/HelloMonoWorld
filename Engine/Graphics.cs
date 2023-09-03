﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HelloMonoWorld.Engine
{
    /// <summary>
    /// Manages the game window
    /// </summary>
    internal static class Graphics
    {
        public static int ViewportWidth => GraphicsDeviceManager.GraphicsDevice.Viewport.Width;

        public static int ViewportHeight => GraphicsDeviceManager.GraphicsDevice.Viewport.Height;

        public static int Width { get; private set; }
        public static int Height { get; private set; }

        public static float ScaledRatio => ViewportWidth / (float)Width;

        public static GraphicsDeviceManager GraphicsDeviceManager;

        public static void Init(Microsoft.Xna.Framework.Game game, int width, int height)
        {
            Width = width;
            Height = height;

            GraphicsDeviceManager = new GraphicsDeviceManager(game)
            {
                PreferredBackBufferWidth = width,
                PreferredBackBufferHeight = height,
            };
            GraphicsDeviceManager.ApplyChanges();
        }

        public static void ToggleFullscreen()
        {
            GraphicsDeviceManager.ToggleFullScreen();
            if (GraphicsDeviceManager.IsFullScreen)
            {
                GraphicsDeviceManager.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                GraphicsDeviceManager.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                GraphicsDeviceManager.HardwareModeSwitch = false;
            }
            else
            {
                GraphicsDeviceManager.PreferredBackBufferWidth = Width;
                GraphicsDeviceManager.PreferredBackBufferHeight = Height;
                GraphicsDeviceManager.HardwareModeSwitch = true;
            }
        }
    }
}
