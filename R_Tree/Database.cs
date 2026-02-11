using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_Tree
{
    class Database
    {
        private readonly Dictionary<string, RTree> _trees = new Dictionary<string, RTree>(StringComparer.OrdinalIgnoreCase);

        public void Add(string name, RTree tree) => _trees[name] = tree;

        public bool Exists(string name) => _trees.ContainsKey(name);

        public RTree Get(string name) => _trees.TryGetValue(name, out var tree) ? tree : null;
    }
}
