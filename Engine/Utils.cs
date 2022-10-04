using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelloMonoWorld.Engine;

public static class Utils
{
    public static Texture2D OneByOneTexture = new(Graphics.graphics.GraphicsDevice, 1, 1);

    internal static void Init()
    {
        OneByOneTexture.SetData(new[] { Color.White });
    }

    public static Vector2 GetDirection(Vector2 from, Vector2 to)
    {
        return Vector2.Normalize(to - from);
    }
}
