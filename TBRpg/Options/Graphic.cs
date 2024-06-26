﻿using Engine;
using Microsoft.Xna.Framework.Input;

namespace TBRpg.Options;

public class Graphic
{
    private const float MinScale = 0.5f;
    private const float MaxScale = 1.2f;
    public static float TextScale { get; private set; } = 1f;

    public static void IncreaseScale()
    {
        TextScale += 0.1f;
        if (TextScale > MaxScale) TextScale = MaxScale;
    }
    
    public static void DecreaseScale()
    {
        TextScale -= 0.1f;
        if (TextScale < MinScale) TextScale = MinScale;
    }

    public static void TryChangeScale()
    {
        if (Input.IsKeyPressed(Keys.PageUp))
            IncreaseScale();
        else if (Input.IsKeyPressed(Keys.PageDown))
            DecreaseScale();
    }
}