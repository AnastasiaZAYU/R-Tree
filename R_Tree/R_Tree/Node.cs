using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_Tree
{
    public class Node
    {
        public Node left, right;
        public Rectangle rec;
        public Point point;

        public Node(int x0, int x1, int y0, int y1)
        {
            left = null;
            right = null;
            rec = new Rectangle(x0, x1, y0, y1);
            point = null;
        }

        public Node(int x, int y)
        {
            left = null;
            right = null;
            rec = null;
            point = new Point(x, y);
        }
    }
}
