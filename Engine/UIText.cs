using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HelloMonoWorld.Engine
{
    public class UIText
    {
        public string Text { get; set; }
        public SpriteFont SpriteFont { get; set; }
        public Vector2 Position { get; set; }
        public Color Color { get; set; }
        public Vector2 Origin { get; set; }
        public Color? ShadowColor { get; set; }

        public static readonly SpriteFont[] SpriteFonts = new SpriteFont[5];

        public static void Init(ContentManager contentManager)
        {
            for (int i = 0; i < SpriteFonts.Length; i++)
            {
                SpriteFonts[i] = contentManager.Load<SpriteFont>($"font{i}");
            }
        }
    }
}
