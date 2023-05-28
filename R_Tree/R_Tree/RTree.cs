using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_Tree
{
    public class RTree
    {
        public Node root;

        public RTree()
        {
            root = null;
        }

        public void Insert(int x, int y)
        {
            if (root == null)
                root = new Node(x, y);
            else Insert(root, x, y);
        }

        public int Print_Tree()
        {
            if (root == null)
                return 0;
            Console.WriteLine(Key(root));
            Print(root, "");
            return 1; 
        }

        public bool Contains(int x, int y)
        {
            return Contain(root, x, y);
        }

        public int Search()
        {
            if (root == null)
                return 0;
            Search(root);
            return 1;
        }

        public int Search(int xl, int yb, int xr, int yt)
        {
            if (root == null)
                return 0;
            return Search(root, xl, yb, xr, yt, 0);
        }
        
        public int Search(int x)
        {
            if (root == null)
                return 0;
            return Search(root, x, 0);
        }
        
        public int Search(int x, int y)
        {
            if (root == null)
                return 0;
            List<string> s = null;
            s = Search(root, x, y, s);
            s.RemoveAt(0);
            foreach (string i in s)
            {
                Console.WriteLine(i);
            }
            return 1;
        }

        private List<string> Search(Node node, int x, int y, List<string> s)
        {
            if (node.rec != null)
            {
                var dl = Distance(node.left, x, y);
                Console.WriteLine(dl);
                var dr = Distance(node.right, x, y);
                Console.WriteLine(dr);
                if (s != null)
                    if (Convert.ToDouble(s[0]) < Math.Min(dl, dr))
                        return s;
                if (dl > dr)
                {
                    s = Search(node.right, x, y, s);
                    if (Convert.ToDouble(s[0]) >= dl)
                        s = Search(node.left, x, y, s);
                }
                else
                {
                    s = Search(node.left, x, y, s);
                    if (Convert.ToDouble(s[0]) >= dr)
                        s = Search(node.right, x, y, s);
                }
            }
            else
            {
                var d = Distance(node, x, y);
                if (s == null)
                    s = new List<string>() { d.ToString(), Key(node) };
                else if (Convert.ToDouble(s[0]) == d)
                    s.Add(Key(node));
                else if (d < Convert.ToDouble(s[0]))
                {
                    s.Clear();
                    s.Add(d.ToString());
                    s.Add(Key(node));
                }
            }
            return s;
        }

        private double Distance(Node node, int x, int y)
        {
            if (node.rec == null)
                return Math.Sqrt(Math.Pow(node.point.x - x, 2) + Math.Pow(node.point.y - y, 2));
            var r = node.rec;
            if (Hit(r, x, y))
                return 0;
            if (x > r.x_left & x < r.x_right)
                return Math.Min(Math.Abs(y - r.y_bottom), Math.Abs(y - r.y_top));
            if (y > r.y_bottom & y < r.y_top)
                return Math.Min(Math.Abs(x - r.x_right), Math.Abs(x - r.x_left));
            if (x < r.x_left)
            {
                if (y > r.y_top)
                    return Math.Sqrt(Math.Pow(x - r.x_left, 2) + Math.Pow(y - r.y_top, 2));
                return Math.Sqrt(Math.Pow(x - r.x_left, 2) + Math.Pow(y - r.y_bottom, 2));
            }
            if (y > r.y_top)
                return Math.Sqrt(Math.Pow(x - r.x_right, 2) + Math.Pow(y - r.y_top, 2));
            return Math.Sqrt(Math.Pow(x - r.x_right, 2) + Math.Pow(y - r.y_bottom, 2));
        }

        private int Search(Node node, int z, int k)
        {
            if (node.rec != null)
            {
                if (node.rec.x_left <= z)
                {
                    k = Search(node.left, z, k);
                    k = Search(node.right, z, k);
                }
            }
            else
            {
                var p = node.point;
                if (p.x < z)
                    Console.WriteLine("({0}, {1})", p.x, p.y);
                return 1;
            }
            return k;
        }
        
        private int Search(Node node, int xl, int yb, int xr, int yt, int k)
        {
            if (node.rec != null)
            {
                var r = node.rec;
                if (r.x_left > xr || r.x_right < xl || r.y_bottom > yt || r.y_top < yb)
                    return k;
                k = Search(node.left, xl, yb, xr, yt, k);
                k = Search(node.right, xl, yb, xr, yt, k);
            }
            else
            {
                var p = node.point;
                if (p.x >= xl & p.x <= xr & p.y >= yb & p.y <= yt)
                {
                    Console.WriteLine(Key(node));
                    return 1;
                }
            }
            return k;
        }

        private void Search(Node node)
        {
            if (node.rec != null)
            {
                Search(node.left);
                Search(node.right);
            }
            else
            {
                var p = node.point;
                Console.WriteLine("({0}, {1})", p.x, p.y);
            }
        }

        private bool Contain(Node node, int x, int y)
        {
            if (node == null) return false;
            var r = node.rec;
            if (r == null)
            {
                var p = node.point;
                if (p.x == x & p.y == y)
                    return true;
            }
            else if (Hit(r, x, y) == true) 
            {
                if (Contain(node.left, x, y) == true)
                    return true;
                if (Contain(node.right, x, y) == true)
                    return true;
            }
            return false;
        }

        private void Print(Node node, string s)
        {
            if (node == null)
                return;
            bool hasLeft = (node.left != null);
            bool hasRight = (node.right != null);
            if (!hasLeft && !hasRight)
                return;
            Console.Write(s);
            Console.Write((hasLeft && hasRight) ? "├── " : "");
            Console.Write((!hasLeft && hasRight) ? "└── " : "");
            if (hasRight)
            {
                bool ps = (hasLeft && hasRight && (node.right.right != null || node.right.left != null));
                string news = s + (ps ? "│   " : "    ");
                Console.WriteLine(Key(node.right));
                Print(node.right, news);
            }
            if (hasLeft)
            {
                Console.Write((hasRight ? s : ""));
                Console.Write("└── ");
                Console.WriteLine(Key(node.left));
                Print(node.left, s + "    ");
            }
        }

        private string Key(Node node)
        {
            if (node.rec != null)
            {
                var r = node.rec;
                return "[ (" + r.x_left.ToString() + ", " + r.y_bottom.ToString() + "), (" + r.x_right.ToString() + ", " + r.y_top.ToString() + ") ]";
            }
            var p = node.point;
            return "(" + p.x + ", " + p.y + ")";
        }

        private void Insert(Node node, int x, int y)
        {
            if (node.rec == null)
            {
                var x0 = node.point.x;
                var y0 = node.point.y;
                node.rec = new Rectangle(x0, x, y0, y);
                node.point = null;
                node.left = new Node(x0, y0);
                node.right = new Node(x, y);
                return;
            }
            if (!Hit(node.rec, x, y))
                node = IncreaseBoundaries(node, x, y);
            var nl = node.left;
            var nr = node.right;
            int sl = 0, sr = 0;
            if (nl.rec != null)
            {
                if (Hit(nl.rec, x, y))
                {
                    Insert(nl, x, y);
                    return;
                }
                else sl = Square(nl.rec, x, y);
            }
            else sl = PointsDistance(nl.point.x, nl.point.y, x, y);
            if (nr.rec != null)
            {
                if (Hit(nr.rec, x, y))
                {
                    Insert(nr, x, y);
                    return;
                }
                else sr = Square(nr.rec, x, y);
            }
            else sr = PointsDistance(nr.point.x, nr.point.y, x, y);
            if (sl <= sr)
                Insert(nl, x, y);
            else Insert(nr, x, y);
            return;
        }

        private Node IncreaseBoundaries(Node node, int x, int y)
        {
            var r = node.rec;
            if (r.x_left > x)
                r.x_left = x;
            else if (r.x_right < x)
                r.x_right = x;
            if (r.y_bottom > y)
                r.y_bottom = y;
            else if (r.y_top < y)
                r.y_top = y;
            return node;
        }

        private int PointsDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x1 - x2) * Math.Abs(y1 - y2);
        }

        private bool Hit(Rectangle r, int x, int y)
        {
            if (x >= r.x_left & x <= r.x_right & y >= r.y_bottom & y <= r.y_top)
                return true;
            return false;
        }

        private int Square(Rectangle r, int px, int py)
        {
            var rxl = r.x_left;
            var rxr = r.x_right;
            var ryb = r.y_bottom;
            var ryt = r.y_top;
            var s0 = (rxr - rxl) * (ryt - ryb);
            if (rxl > px)
                rxl = px;
            else if (rxr < px)
                rxr = px;
            if (ryb > py)
                ryb = py;
            else if (ryt < py)
                ryt = py;
            var s1 = (rxr - rxl) * (ryt - ryb);
            return s1 - s0;
        }
    }
}