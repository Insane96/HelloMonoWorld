﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine;

/// <summary>
/// Takes care of Fonts and drawn text
/// </summary>
public record UiText
{
    public string Text { get; set; }
    public SpriteFont SpriteFont { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; } = Vector2.One;
    public float Rotation { get; set; } = 0f;
    public Color Color { get; set; }
    public Vector2 Origin { get; set; }
    public Color? ShadowColor { get; set; }
}