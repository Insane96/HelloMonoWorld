using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloMonoWorld.Engine
{
    public class Origins
    {
        public static readonly Vector2 TopLeft = Vector2.Zero;
        public static readonly Vector2 TopCenter = new(0.5f, 0f);
        public static readonly Vector2 TopRight = new(1f, 0f);
        public static readonly Vector2 CenterLeft = new(0f, 0.5f);
        public static readonly Vector2 Center = new(0.5f, 0.5f);
        public static readonly Vector2 CenterRight = new(1f, 0.5f);
        public static readonly Vector2 BottomLeft = new(0f, 1f);
        public static readonly Vector2 BottomCenter = new(0.5f, 1f);
        public static readonly Vector2 BottomRight = new(1f, 1f);
    }
}
