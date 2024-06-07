using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEngine;

/// <summary>
/// Takes care of Fonts and drawn text
/// </summary>
public class UiText
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
            SpriteFonts[i] = contentManager.Load<SpriteFont>($"fonts/font{i}");
        }
    }
}