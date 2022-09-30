using Microsoft.Xna.Framework;

namespace HelloMonoWorld.Engine;

public static class Utils
{
    public static Vector2 GetDirection(Vector2 from, Vector2 to)
    {
        return Vector2.Normalize(to - from);
    }
}
