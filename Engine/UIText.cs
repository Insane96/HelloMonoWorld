using FontStashSharp;
using Microsoft.Xna.Framework;

namespace Engine;

/// <summary>
/// Takes care of Fonts and drawn text
/// </summary>
public record UiText
{
    public string Text { get; init; }
    public SpriteFontBase SpriteFont { get; init; }
    public Vector2 Position { get; init; }
    public Vector2 Scale { get; init; } = Vector2.One;
    public float Rotation { get; init; } = 0f;
    public Color Color { get; init; }
    public Vector2 Origin { get; init; }
    public Color? ShadowColor { get; init; }
}