using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace R_Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            RTree tree = null;
            var link = new Database();
            string[] c = { "create", "insert", "print_tree", "exit", "contains", "search" };
            int z = 0;
            while (z == 0)
            {
                string a = "";
                int ll;
                string ai;
                do
                {
                    a = a + " ";
                    ai = Console.ReadLine();
                    for (int j = 0; j < ai.Length; j++)
                    {
                        if (ai[j] == ',')
                        {
                            a = a + " " + ai[j] + " ";
                        }
                        else if (ai[j] == '(')
                            a = a + ai[j] + " ";
                        else if (ai[j] == ')')
                            a = a + " " + ai[j];
                        else
                            a = a + ai[j];
                    }
                    ll = a.IndexOf(";");
                    if (ll != -1)
                        a = a.Remove(ll);
                } while (ll == -1);
                a = a.TrimStart(' ');
                a = a.TrimEnd(' ');
                a = Regex.Replace(a, "\\s+", " ");
                string[] w = a.Split(' ');
                for (int j = 0; j < w.Length; j++)
                {
                    if (j != 1)
                        w[j] = w[j].ToLower();
                }
                int i;
                for (i = 0; i < c.Length; i++)
                {
                    int h = a.IndexOf(c[i]);
                    if (h != -1)
                    {
                        break;
                    }
                }
                switch (i)
                {
                    case 0:
                        Regex b1 = new Regex(@"^[cC][rR][eE][aA][tT][eE][\s][a-zA-Z][\w]*$");
                        if (b1.IsMatch(a))
                        {
                            if (link.Contains(w[1]) == 1)
                            {
                                Console.Write("The name of data structure is already been used\n");
                                break;
                            }
                            tree = new RTree();
                            link.Add(w[1], tree);
                            Console.Write("Set {0} has been created\n", w[1]);
                        }
                        else
                        {
                            Console.Write("Incorrect command syntax\n");

                        }; break;
                    case 1:
                        Regex b2 = new Regex(@"^[iI][nN][sS][eE][rR][tT][\s][a-zA-Z][\w]*[\s][(][\s][-]{0,1}[\d]{1,}[\s][,][\s][-]{0,1}[\d]{1,}[\s][)]$");
                        if (b2.IsMatch(a))
                        {
                            if (link.Contains(w[1]) == 0)
                            {
                                Console.Write("Your tree doesn`t exist\n");
                                break;
                            }
                            tree = link.Link(w[1]);
                            int t1 = Convert.ToInt32(w[3]);
                            int t2 = Convert.ToInt32(w[5]);
                            tree.Insert(t1, t2);
                            Console.Write("Point ({0}, {1}) has been added to {2}\n", t1, t2, w[1]);
                        }
                        else
                        {
                            Console.Write("Incorrect command syntax\n");

                        }; break;
                    case 2:
                        Regex b3 = new Regex(@"^[pP][rR][iI][nN][tT][_][tT][rR][eE][eE][\s][a-zA-Z][\w]*$");
                        if (b3.IsMatch(a))
                        {
                            if (link.Contains(w[1]) == 0)
                            {
                                Console.Write("Your tree doesn`t exist\n");
                                break;
                            }
                            tree = link.Link(w[1]);
                            if (tree.Print_Tree() == 0)
                            {
                                Console.Write("Your tree is empty\n");
                            }
                        }
                        else
                        {
                            Console.Write("Incorrect command syntax\n");

                        }; break;
                    case 3:
                        Regex b4 = new Regex(@"^[eE][xX][iI][tT]$");
                        if (b4.IsMatch(a))
                        {
                            z = 1;
                        }; break;
                    default:
                        Console.Write("Command not recognized\n");
                        break;
                    case 4:
                        Regex b5 = new Regex(@"^[cC][oO][nN][tT][aA][iI][nN][sS][\s][a-zA-Z][\w]*[\s][(][\s][-]{0,1}[\d]{1,}[\s][,][\s][-]{0,1}[\d]{1,}[\s][)]$");
                        if (b5.IsMatch(a))
                        {
                            if (link.Contains(w[1]) == 0)
                            {
                                Console.Write("Your tree doesn`t exist\n");
                                break;
                            }
                            tree = link.Link(w[1]);
                            int t1 = Convert.ToInt32(w[3]);
                            int t2 = Convert.ToInt32(w[5]);
                            if (tree.Contains(t1, t2) == true)
                            {
                                Console.Write("Match!\n");
                            }
                            else
                            {
                                Console.Write("No Match!\n");
                            }
                        }
                        else
                        {
                            Console.Write("Incorrect command syntax\n");

                        }; break;
                    case 5:
                        Regex b6 = new Regex(@"^[sS][eE][aA][rR][cC][hH][\s][a-zA-Z][\w]*$");
                        Regex b7 = new Regex(@"^[sS][eE][aA][rR][cC][hH][\s][a-zA-Z][\w]*[\s][wW][hH][eE][rR][eE][\s][iI][nN][sS][iI][dD][eE][\s][(][\s][-]{0,1}[\d]{1,}[\s][,][\s][-]{0,1}[\d]{1,}[\s][)][\s][,][\s][(][\s][-]{0,1}[\d]{1,}[\s][,][\s][-]{0,1}[\d]{1,}[\s][)]$");
                        Regex b8 = new Regex(@"^[sS][eE][aA][rR][cC][hH][\s][a-zA-Z][\w]*[\s][wW][hH][eE][rR][eE][\s][lL][eE][fF][tT][_][oO][fF][\s][-]{0,1}[\d]{1,}$");
                        Regex b9 = new Regex(@"^[sS][eE][aA][rR][cC][hH][\s][a-zA-Z][\w]*[\s][wW][hH][eE][rR][eE][\s][nN][nN][\s][(][\s][-]{0,1}[\d]{1,}[\s][,][\s][-]{0,1}[\d]{1,}[\s][)]$");
                        {
                            if (link.Contains(w[1]) == 0)
                            {
                                Console.Write("Your tree doesn`t exist\n");
                                break;
                            }
                            tree = link.Link(w[1]);
                            if (b6.IsMatch(a))
                            {
                                if (tree.Search() == 0)
                                {
                                    Console.Write("Your tree is empty\n");
                                }
                            }
                            else if (b7.IsMatch(a))
                            {
                                int t1 = Convert.ToInt32(w[5]);
                                int t2 = Convert.ToInt32(w[7]);
                                int t3 = Convert.ToInt32(w[11]);
                                int t4 = Convert.ToInt32(w[13]);
                                if (tree.Search(t1, t2, t3, t4) == 0)
                                {
                                    Console.Write("There is no points in this rectangle\n");
                                }
                            }
                            else if (b8.IsMatch(a))
                            {
                                int t1 = Convert.ToInt32(w[4]);
                                if (tree.Search(t1) == 0)
                                {
                                    Console.Write("There is no points in this interval\n");
                                }
                            }
                            else if (b9.IsMatch(a))
                            {
                                int t1 = Convert.ToInt32(w[5]);
                                int t2 = Convert.ToInt32(w[7]);
                                if (tree.Search(t1, t2) == 0)
                                {
                                    Console.Write("Your tree is empty\n");
                                }
                            }
                            else
                            {
                                Console.Write("Incorrect command syntax\n");

                            }
                        }; break;
                }
            }
        }
    }
}
