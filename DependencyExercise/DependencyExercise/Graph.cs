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
            if(this.mAdjacencyList.TryGetValue(v, out listV)){ }
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
            if(this.mAdjacencyList.TryGetValue(w, out listW)){ }
            if (listW == null)
            {
                this.mAdjacencyList[w] = new LinkedList<string>();
            }
        }


        /// <summary>
        /// Performs a topological sort
        /// </summary>
        public void topologicalSort()
        {

        }

        /// <summary>
        /// Helper function for topological sort
        /// </summary>
        /// <param name="v"></param>
        /// <param name="visited"></param>
        /// <param name="stack"></param>
        private void topologicalSortUtil(string v,  Dictionary<string, bool> visited, Stack<string> stack) 
        {

        }

    }



}
