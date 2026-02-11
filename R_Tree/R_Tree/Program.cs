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
            var db = new Database();
            Console.WriteLine("R-Tree Database CLI. Type 'exit' to quit. Use ';' at the end of each command.");

            while (true)
            {
                string input = ReadCommand();
                if (string.IsNullOrWhiteSpace(input))
                    continue;

                input = input.Replace(";", "").Trim();
                var parts = input.Split(new[] { ' ', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 0)
                    continue;

                string command = parts[0].ToLower();

                if (command == "exit")
                    break;

                try
                {
                    ExecuteCommand(command, parts, db);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        static string ReadCommand()
        {
            Console.Write("> ");
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                string line = Console.ReadLine();
                if (line == null)
                    return null;
                sb.AppendLine(line + " ");
                if (line.Contains(";"))
                    break;
            }
            return sb.ToString();
        }

        static void ExecuteCommand(string command, string[] parts, Database db)
        {
            switch (command)
            {
                case "create":
                    if (parts.Length < 2)
                        throw new Exception("Usage: create <tree_name>;");
                    string treeName = parts[1];
                    if (db.Exists(treeName))
                    {
                        Console.WriteLine($"Tree '{treeName}' already exists.");
                        return;
                    }
                    db.Add(treeName, new RTree());
                    Console.WriteLine($"Tree '{treeName}' created.");
                    break;

                case "insert":
                    if (parts.Length < 4)
                        throw new Exception("Usage: insert <tree_name> (x, y);");
                    treeName = parts[1];
                    var treeToInsert = db.Get(treeName);
                    if (treeToInsert == null)
                        throw new Exception($"Tree '{treeName}' not found.");

                    int x = int.Parse(parts[2]);
                    int y = int.Parse(parts[3]);
                    treeToInsert.Insert(x, y);
                    Console.WriteLine($"Inserted point ({x}, {y}) into tree '{treeName}'.");
                    break;

                case "print":
                    if (parts.Length < 2)
                        throw new Exception("Usage: print <tree_name>;");
                    treeName = parts[1];
                    var treeToPrint = db.Get(treeName);
                    if (treeToPrint == null)
                        throw new Exception($"Tree '{treeName}' not found.");

                    treeToPrint.PrintTree();
                    break;

                case "contains":
                    if (parts.Length < 4)
                        throw new Exception("Usage: contains <tree_name> (x, y);");
                    treeName = parts[1];
                    var treeToCheck = db.Get(treeName);
                    if (treeToCheck == null)
                        throw new Exception($"Tree '{treeName}' not found.");
                    
                    x = int.Parse(parts[2]);
                    y = int.Parse(parts[3]);
                    Console.WriteLine(treeToCheck.Contains(x, y) ? "Yes" : "No");
                    break;

                case "search":
                    break;

                default:
                    throw new Exception("Unknown command. Available commands: create, insert, contains, print, exit.");
            }
        }
    }
}
