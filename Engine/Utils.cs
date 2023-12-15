using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine;

public static class Utils
{
    /// <summary>
    /// Used to draw solid color rectangles
    /// </summary>
    public static readonly Texture2D OneByOneTexture = new(Graphics.GraphicsDeviceManager.GraphicsDevice, 1, 1);

    internal static void Init()
    {
        OneByOneTexture.SetData(new[] { Color.White });
    }

    public static Vector2 GetDirection(Vector2 from, Vector2 to)
    {
        return Vector2.Normalize(to - from);
    }

    public static bool Intersects(Vector2 lineStart, Vector2 lineEnd, Rectangle rectangle)
    {
        return Intersects(lineStart, lineEnd, rectangle.PosToVector2(), rectangle.PosToVector2().Sum(rectangle.Width, 0f))
               || Intersects(lineStart, lineEnd, rectangle.PosToVector2(), rectangle.PosToVector2().Sum(0f, rectangle.Height))
               || Intersects(lineStart, lineEnd, rectangle.PosToVector2().Sum(0, rectangle.Height), rectangle.PosToVector2().Sum(rectangle.Width, rectangle.Height))
               || Intersects(lineStart, lineEnd, rectangle.PosToVector2().Sum(rectangle.Width, 0), rectangle.PosToVector2().Sum(rectangle.Width, rectangle.Height));
    }

    public static bool Intersects(Vector2 lineStartA, Vector2 lineEndA, Vector2 lineStartB, Vector2 lineEndB)
    {
        Vector2 b = lineEndA - lineStartA;
        Vector2 d = lineEndB - lineStartB;
        float bDotDPerp = b.X * d.Y - b.Y * d.X;

        // if b dot d == 0, it means the lines are parallel so have infinite intersection points
        if (bDotDPerp == 0)
            return false;

        Vector2 c = lineStartB - lineStartA;
        float t = (c.X * d.Y - c.Y * d.X) / bDotDPerp;
        if (t is < 0 or > 1)
            return false;

        float u = (c.X * b.Y - c.Y * b.X) / bDotDPerp;
        return u is >= 0 and <= 1;
    }

    public static Vector2 RotateDirectionClockwise(Vector2 dir)
    {
        return new Vector2(dir.Y * -1f, dir.X);
    }

    public static Vector2 RotateDirectionCounterClockwise(Vector2 dir)
    {
        return new Vector2(dir.Y, dir.X * -1f);
    }

    public static Vector2 OppositeDirection(Vector2 dir)
    {
        return new Vector2(dir.Y * -1f, dir.X * -1f);
    }
}
