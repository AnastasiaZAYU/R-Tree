using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_Tree
{
    public class Rectangle
    {
        public int XMin { get; private set; }
        public int YMin { get; private set; }
        public int XMax { get; private set; }
        public int YMax { get; private set; }

        public Rectangle(int x1, int x2, int y1, int y2)
        {
            XMin = Math.Min(x1, x2);
            XMax = Math.Max(x1, x2);
            YMin = Math.Min(y1, y2);
            YMax = Math.Max(y1, y2);
        }

        public long Area => (long)(XMax - XMin) * (YMax - YMin);

        public bool Contains(int x, int y)
        {
            return x >= XMin && x <= XMax && y >= YMin && y <= YMax;
        }

        public long ExpansionArea(int x, int y)
        {
            int newXMin = Math.Min(XMin, x);
            int newXMax = Math.Max(XMax, x);
            int newYMin = Math.Min(YMin, y);
            int newYMax = Math.Max(YMax, y);
            return (long)(newXMax - newXMin) * (newYMax - newYMin) - Area;
        }

        public void ExpandToInclude(int x, int y)
        {
            XMin = Math.Min(XMin, x);
            XMax = Math.Max(XMax, x);
            YMin = Math.Min(YMin, y);
            YMax = Math.Max(YMax, y);
        }

        public bool Intersects(Rectangle other)
        {
            return !(this.XMax < other.XMin || 
                this.XMin > other.XMax || 
                this.YMax < other.YMin ||
                this.YMin > other.YMax);
        }
    }
}
