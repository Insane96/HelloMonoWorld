using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloMonoWorld
{
    internal static class ExtensionMethods
    {
        public static string ToString(this Vector2 vector, string format)
        {
            return $"{{X:{vector.X.ToString(format)} Y:{vector.Y.ToString(format)}}}";
        }
    }
}
