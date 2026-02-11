using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_Tree
{
    // This implementation is a binary R-tree.
    // It uses MBR for spatial search, but limits the number of children per node to 2
    // to simplify the logic without the need for complex balancing (Split).
    public class RTree
    {
        private Node _root;

        public bool Contains(int x, int y)
        {
            return ContainsRecursive(_root, x, y);
        }

        private bool ContainsRecursive(Node node, int x, int y)
        {
            if (node == null) 
                return false;

            if (!node.MBR.Contains(x, y))
                return false;

            if (node.IsLeaf)
            {
                return node.DataPoint.X == x && node.DataPoint.Y == y;
            }
            return ContainsRecursive(node.Left, x, y) || ContainsRecursive(node.Right, x, y);
        }

        public void Insert(int x, int y)
        {
            if (_root == null)
            {
                _root = new Node(x, y);
            }
            else
            {
                if (Contains(x, y))
                    throw new Exception($"Point ({x}, {y}) already exists in the tree.");
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

        public List<Point> SearchInArea(int xMin, int yMin, int xMax, int yMax)
        {
            var results = new List<Point>();
            var searchArea = new Rectangle(x1: xMin, y1: yMin, x2: xMax, y2: yMax);
            SearchInAreaRecursive(_root, searchArea, results);
            return results;
        }

        private void SearchInAreaRecursive(Node node, Rectangle area, List<Point> results)
        {
            if (node == null || !node.MBR.Intersects(area))
                return;

            if (node.IsLeaf)
            {
                if (area.Contains(node.DataPoint.X, node.DataPoint.Y))
                    results.Add(node.DataPoint);
            }
            else
            {
                SearchInAreaRecursive(node.Left, area, results);
                SearchInAreaRecursive(node.Right, area, results);
            }
        }

        public Point? SearchNearest(int x, int y)
        {
            if (_root == null)
                return null;
            Point? bestPoint = null;
            long minDistanceSq = long.MaxValue;
            SearchNearestRecursive(_root, x, y, ref bestPoint, ref minDistanceSq);
            return bestPoint;
        }

        private void SearchNearestRecursive(Node node, int x, int y, ref Point? bestPoint, ref long minDistanceSq)
        {
            if (node == null)
                return;

            if (node.IsLeaf)
            {
                long distSq = node.DataPoint.DistanceSquared(x, y);
                if (distSq < minDistanceSq)
                {
                    minDistanceSq = distSq;
                    bestPoint = node.DataPoint;
                }
                return;
            }

            long disLeft = node.Left.MBR.MinDistanceSquared(x, y);
            long disRight = node.Right.MBR.MinDistanceSquared(x, y);

            if (disLeft < disRight)
            {
                if (disLeft < minDistanceSq)
                    SearchNearestRecursive(node.Left, x, y, ref bestPoint, ref minDistanceSq);
                if (disRight < minDistanceSq)
                    SearchNearestRecursive(node.Right, x, y, ref bestPoint, ref minDistanceSq);
            }
            else
            {
                if (disRight < minDistanceSq)
                    SearchNearestRecursive(node.Right, x, y, ref bestPoint, ref minDistanceSq);
                if (disLeft < minDistanceSq)
                    SearchNearestRecursive(node.Left, x, y, ref bestPoint, ref minDistanceSq);
            }
        }

        public void PrintTree()
        {
            if (_root == null)
            {
                Console.WriteLine("Tree is empty.");
                return;
            }
            PrintRecursive(_root, "", true);
        }

        private void PrintRecursive(Node node, string indent, bool isLast)
        {
            if (node == null) 
                return;

            Console.Write(indent);
            Console.Write(isLast ? "└─ " : "├── ");

            if (node.IsLeaf)
            {
                Console.WriteLine($"Point: {node.DataPoint}");
            }
            else
            {
                Console.WriteLine($"MBR: [{node.MBR.XMin}, {node.MBR.YMin}] - [{node.MBR.XMax}, {node.MBR.YMax}]");

                string childIndent = indent + (isLast ? "    " : "│   ");

                if (node.Left != null && node.Right != null)
                {
                    PrintRecursive(node.Left, childIndent, false);
                    PrintRecursive(node.Right, childIndent, true);
                }
                else if (node.Left != null)
                {
                    PrintRecursive(node.Left, childIndent, true);
                }
            }
        }
    }
}