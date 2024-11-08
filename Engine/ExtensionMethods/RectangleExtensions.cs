using Microsoft.Xna.Framework;

namespace Engine.ExtensionMethods;

public static class RectangleExtensions
{
    public static Vector2 PosToVector2(this Rectangle rectangle)
    {
        return new Vector2(rectangle.X, rectangle.Y);
    }
}