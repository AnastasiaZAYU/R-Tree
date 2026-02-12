# R-Tree Spatial Database

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Language: C#](https://img.shields.io/badge/Language-C%23-blue.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)

A C# implementation of a spatial database using a **Binary R-Tree** indexing structure. This project allows you to create spatial indices, insert 2D points, and perform efficient spatial queries (range search and nearest neighbor search) via a Command Line Interface (CLI).

## ⚡ Features
- **Binary R-Tree Implementation:** Uses a simplified R-Tree structure where every node has exactly 2 children, utilizing Minimum Bounding Rectangles (MBR) for spatial grouping.
- **Spatial Operations:**
  - **Insert:** Efficiently adds points while minimizing MBR expansion.
  - **Range Search:** Finds all points within a specific rectangular area.
  - **Nearest Neighbor (k-NN):** Finds the closest point to a given coordinate using a branch-and-bound algorithm.
  - **Exact Match:** Checks if a specific point exists.
- **CLI Interface:** Interactive console environment to manage multiple trees and execute commands.
- **Visualization:** Text-based tree printing to debug and understand the tree structure.

## 📁 Project Structure
```
├──R_Tree/
│   ├── Properties/       # Assembly metadata
│   ├── Database.cs       # In-memory storage for managing multiple R-Trees by name
│   ├── Node.cs           # Represents a node in the tree (holds MBR and DataPoint)
│   ├── Point.cs          # Simple struct for 2D coordinates
│   ├── Program.cs        # Main entry point and CLI command parser
│   ├── Rectangle.cs      # Geometric primitives and helper methods (Area, Intersection, Distance)
│   └── RTree.cs          # Core tree logic (Insert, Search, Print).
├── .gitignore            # Standard .NET ignore rules
├── LICENSE
├── README.md
└── R_Tree.sln            # Visual Studio Solution
```

## 🚀 How to run
1. **Prerequisites**: Ensure you have [Visual Studio](https://visualstudio.microsoft.com/) installed with the **.NET desktop development** workload.
2. **Clone the repository:**
```bash
git clone https://github.com/AnastasiaZAYU/rtree-spatial-database.git
```
3. Open `R_Tree.sln` in **Visual Studio**.
4. Build and run the project (F5).

## 📖 Usage Guide
Once the application starts, you can enter commands into the console.
> **Note:** All commands must end with a semicolon (;).

### **Available Commands**
| Command | Description | Example |
| :--- | :--- | :--- |
| `create` | Creates a new named R-Tree | `create map;` |
| `insert` | Inserts a point (x, y) into a tree | `insert map (10, 20);` |
| `contains` | Checks if a point exists in the tree | `contains map (10, 20);` |
| `search` | Finds points in a rectangle defined by (xMin, yMin) and (xMax, yMax) | `search map (0,0) (50,50);` |
| `nearest` | Finds the single closest point to the given coordinates | `nearest map (12, 18);` |
| `print` | Visualizes the tree structure in the console | `print map;` |
| `exit` | Closes the application | `exit;` |

### Example Session
```console
> create myMap;
Tree 'myMap' created.
> insert myMap (3, 5);
Inserted point (3, 5) into tree 'myMap'.
> insert myMap (10, 10);
Inserted point (10, 10) into tree 'myMap'.
> insert myMap (5, 8);
Inserted point (5, 8) into tree 'myMap'.
> contains myMap (5, 8);
Yes
> contains myMap (8, 8);
No
> search myMap (5, 5) (10, 10);
Found 2 point(s):
(5, 8)
(10, 10)
> nearest myMap (5, 5);
Nearest point to (5, 5) is (3, 5).
> print myMap;
└─ MBR: [3, 5] - [10, 10]
    ├── MBR: [3, 5] - [5, 8]
    │   ├── Point: (3, 5)
    │   └─ Point: (5, 8)
    └─ Point: (10, 10)
> exit;
```

## 🧠 Technical Implementation
This project implements a variation of the R-Tree optimized for simplicity:
1. **Node Structure** (`Node.cs`): Each internal node contains references to a Left and Right child (Binary) and an MBR that encompasses all points beneath it.
2. **Insertion Logic:** When inserting a new point, the algorithm traverses the tree and chooses the branch that requires the **least enlargement** of its area to include the new point.
3. **Search Logic:**
   - **Range Search:** Recursively visits nodes only if their MBR intersects with the search area.
   - **Nearest Neighbor:** Uses a recursive approach that calculates the minimum distance from the query point to the MBRs. It prunes branches that are farther away than the closest point found so far.

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👩‍💻 Author

**Anastasiia Zatsarenko**  
Institute of Physics and Technology (IPT), Igor Sikorsky KPI, 2023.
