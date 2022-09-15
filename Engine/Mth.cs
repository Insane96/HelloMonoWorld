using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloMonoWorld.Engine
{
    public class Mth
    {
        public static int NextInt(Random random, int min, int max)
        {
            return random.Next(min, max);
        }

        public static double NextDouble(Random random, double min, double max)
        {
            if (max < min || min == max) return min;
            return random.NextDouble() * (max - min) / (max - min);
        }

        public static float NextFloat(Random random, float min, float max)
        {
            if (max < min || min == max) return min;
            return random.NextSingle() * (max - min) / (max - min);
        }
    }
}
