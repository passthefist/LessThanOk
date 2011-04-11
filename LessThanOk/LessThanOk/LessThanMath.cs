using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LessThanOk
{
    class LessThanMath
    {
        static Random rand = new Random(DateTime.UtcNow.Millisecond);

        public static int random(int min, int max)
        {
            return rand.Next(max - min) + min;
        }

        public static int min(int x, int y)
        {
            return x>y ? y : x;
        }

        public static int max(int x, int y)
        {
            return x > y ? x : y;
        }

        public static int approxDist(int x1, int y1, int x2, int y2)
        {
            //MAGIC! Linear combination of min+max funcs
            int x = Math.Abs(x1 - x2);
            int y = Math.Abs(y1 - y2);
            return (1007*min(x, y) + 441*max(x, y))/1024;
        }

        public static int approxDist(Point p1, Point p2)
        {
            return approxDist(p1.X, p1.Y, p2.X, p2.Y);
        }
    }
}
