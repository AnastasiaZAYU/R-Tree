using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_Tree
{
    public class Rectangle
    {
        public int x_left, x_right, y_bottom, y_top;

        public Rectangle(int x1, int x2, int y1, int y2)
        {
            x_left = Math.Min(x1, x2);
            x_right = Math.Max(x1, x2);
            y_bottom = Math.Min(y1, y2);
            y_top = Math.Max(y1, y2);
        }
    }
}
