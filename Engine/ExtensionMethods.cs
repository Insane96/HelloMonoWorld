using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace Engine;

public static class ExtensionMethods
{
    public static string ToString(this Vector2 vector, string format)
    {
        return $"{{X:{vector.X.ToString(format)} Y:{vector.Y.ToString(format)}}}";
    }

    public static Vector2 Sum(this Vector2 vector, float x, float y)
    {
        return new Vector2(vector.X + x, vector.Y + y);
    }

    public static Vector2 Sum(this Vector2 vector, float f)
    {
        return new Vector2(vector.X + f, vector.Y + f);
    }

    public static Vector2 Multiply(this Vector2 vector, float x, float y)
    {
        return new Vector2(vector.X * x, vector.Y * y);
    }

    public static Vector2 Multiply(this Vector2 vector, float f)
    {
        return new Vector2(vector.X * f, vector.Y * f);
    }

    public static Vector2 ExtendFrom(this Vector2 a, Vector2 b, float extension)
    {
        double len = Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
        return new Vector2((float)(a.X + (a.X - b.X) / len * extension), (float)(a.Y + (a.Y - b.Y) / len * extension));
    }
}