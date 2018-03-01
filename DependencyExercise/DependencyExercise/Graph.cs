using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyExercise
{
    /// <summary>
    /// Graph Class based on https://www.geeksforgeeks.org/topological-sorting/ java graph class
    /// This class is rewritten from java into C#
    /// </summary>
    class Graph
    {
        private int mNumberOfVertices; // represents the number of vertices
        private Dictionary<string, LinkedList<string>> mAdjacencyList;
        private Dictionary<string, bool> mVisited;

        Graph(int v)
        {
            this.mNumberOfVertices = v;
            this.mAdjacencyList = new Dictionary<string, LinkedList<string>>();
            this.mVisited = new Dictionary<string, bool>();
        }

        /// <summary>
        /// This adds an edge between vertex v and w into the member data structures
        /// </summary>
        /// <param name="v"></param>
        /// <param name="w"></param>
        public void addEdge(string v, string w)
        {
            LinkedList<string> listV;
            if (this.mAdjacencyList.TryGetValue(v, out listV)) { }
            if (listV == null)
            {
                this.mAdjacencyList[v] = new LinkedList<string>();
                this.mAdjacencyList[v].AddLast(w); // adding edge to w into adj list at v
            }
            else // list already exists
            {
                this.mAdjacencyList[v].AddLast(w);
            }

            LinkedList<string> listW;
            if (this.mAdjacencyList.TryGetValue(w, out listW)) { }
            if (listW == null)
            {
                this.mAdjacencyList[w] = new LinkedList<string>();
            }

            // setting visited to false
            this.mVisited[v] = false;
            this.mVisited[w] = false;
        }


        /// <summary>
        /// Performs a topological sort (function that spawns the recursive
        /// topological sort util function)
        /// </summary>
        public void topologicalSort()
        {
            // stack used to keep track of the output elements
            Stack<string> stack = new Stack<string>();

            foreach (string s in this.mVisited.Keys.ToArray<string>())
            {
                // if it's not visited call util function
                if (!this.mVisited[s])
                {
                    this.topologicalSortUtil(s, this.mVisited, stack);
                }
            }

            while (stack.Count > 0)
            {
                Console.Write(stack.Pop() + " "); // after rec this outputs topologically ordered nodes
            }
        }

        /// <summary>
        /// Helper function for topological sort
        /// </summary>
        /// <param name="v">represents the node</param>
        /// <param name="visited">hashmap that maps nodes to boolean values</param>
        /// <param name="stack">keeps track of the topologically sorted output</param>
        private void topologicalSortUtil(string v, Dictionary<string, bool> visited, Stack<string> stack)
        {
            visited[v] = true;

            LinkedList<string> list = this.mAdjacencyList[v];
            foreach (string s in list)
            {
                if (!visited[s])
                {
                    this.topologicalSortUtil(s, visited, stack);
                } // else it should be a circular dependency
            }
            stack.Push(v);
        }

        static void Main(string[] args)
        {
            // Create a graph given in the above diagram
            Graph g = new Graph(6);
            /*
            g.addEdge("5", "2");
            g.addEdge("5", "0");
            g.addEdge("4", "0");
            g.addEdge("4", "1");
            g.addEdge("2", "3");
            g.addEdge("3", "1");
            */

            /*
            g.addEdge("C", "B");
            g.addEdge("C", "A");
            */

            /*
            g.addEdge("A", "E");
            g.addEdge("B", "F");
            g.addEdge("C", "B");
            g.addEdge("D", "C");
            g.addEdge("D", "G");
            */

            Console.WriteLine("Following is a Topological " +
                              "sort of the given graph");
            g.topologicalSort();
            Console.ReadLine();
        }

    }


}
