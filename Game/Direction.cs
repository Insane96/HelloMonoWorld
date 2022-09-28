using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloMonoWorld.Game;

public class Direction
{
    public readonly Vector2 vector;
    public readonly Keys key;

    public Direction(Vector2 vector, Keys key)
    {
        this.vector = vector;
        this.key = key;
    }

    public static readonly Direction UP = new(new Vector2(0, -1), Keys.W);
    public static readonly Direction RIGHT = new(new Vector2(1, 0), Keys.D);
    public static readonly Direction DOWN = new(new Vector2(0, 1), Keys.S);
    public static readonly Direction LEFT = new(new Vector2(-1, 0), Keys.A);

    public static readonly List<Direction> directions = new()
    {
        UP, RIGHT, DOWN, LEFT
    };

    public static readonly List<Direction> upDownDirections = new()
    {
        UP, DOWN
    };
}
