using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelloMonoWorld.Engine;

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
}
