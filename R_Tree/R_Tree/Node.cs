using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_Tree
{
    public class Node
    {
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Rectangle MBR { get; set; } // Minimum Bounding Rectangle
        public Point DataPoint { get; set; }

        public bool IsLeaf => Left == null;

        // Leaf constructor
        public Node(int x, int y)
        {
            DataPoint = new Point(x, y);
            MBR = new Rectangle(x, x, y, y);
        }

        // Constructor for internal node
        public Node(Node left, Node right)
        {
            Left = left;
            Right = right;
            UpdateMBR();
        }

        public void UpdateMBR()
        {
            if (Left != null && Right != null)
            {
                MBR = new Rectangle(
                    Math.Min(Left.MBR.XMin, Right.MBR.XMin),
                    Math.Max(Left.MBR.XMax, Right.MBR.XMax),
                    Math.Min(Left.MBR.YMin, Right.MBR.YMin),
                    Math.Max(Left.MBR.YMax, Right.MBR.YMax)
                    );
            }
            else if (Left != null)
            {
                MBR = Left.MBR;
            }
        }
    }
}
