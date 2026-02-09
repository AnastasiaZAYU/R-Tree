using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_Tree
{
    public readonly struct Point
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public long DistanceSquared(int otherX, int otherY)
        {
            long dx = (long)X - otherX;
            long dy = (long)Y - otherY;
            return dx * dx + dy * dy;
        }

        public override string ToString() => $"({X}, {Y})";
    }
}
