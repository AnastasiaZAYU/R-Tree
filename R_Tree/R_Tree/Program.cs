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
                    break;

                case "print_tree":
                    break;

                case "exit":
                    break;

                case "contains":
                    break;

                case "search":
                    break;

                default:
                    throw new Exception("Unknown command. Available commands: create, insert, search, print_tree, contains, exit.");
            }
        }
    }
}
