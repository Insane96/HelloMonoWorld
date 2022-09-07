using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HelloMonoWorld
{
    internal static class ExtensionMethods
    {
        public static string ToString(this Vector2 vector, string format)
        {
            return $"{{X:{vector.X.ToString(format)} Y:{vector.Y.ToString(format)}}}";
        }
        
        public static Vector2 Sum(this Vector2 vector, float x, float y)
        {
            return new Vector2(vector.X + x, vector.Y + y);
        }
    }
}
