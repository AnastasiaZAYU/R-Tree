using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_Tree
{
    public class RTree
    {
        private Node _root;

        public void Insert(int x, int y)
        {
            if (_root == null)
            {
                _root = new Node(x, y);
            }
            else
            {
                InsertRecursive(_root, x, y);
            }
        }

        private void InsertRecursive(Node node, int x, int y)
        {
            if (node.IsLeaf)
            {
                Point oldPoint = node.DataPoint;
                node.Left = new Node(oldPoint.X, oldPoint.Y);
                node.Right = new Node(x, y);
                node.DataPoint = default;
                node.UpdateMBR();
                return;
            }

            long leftExpansion = node.Left.MBR.ExpansionArea(x, y);
            long rightExpansion = node.Right.MBR.ExpansionArea(x, y);

            if (leftExpansion <= rightExpansion)
            {
                InsertRecursive(node.Left, x, y);
            }
            else
            {
                InsertRecursive(node.Right, x, y);
            }
            node.UpdateMBR();
        }
    }
}