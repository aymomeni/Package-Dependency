using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DependencyExercise
{
    /// <summary>
    /// Graph Class based on https://www.geeksforgeeks.org/topological-sorting/ java graph class
    /// This class is rewritten from java into C#
    /// </summary>
    public class Graph
    {

        // Member variables
        private Dictionary<string, LinkedList<string>> mAdjacencyList;
        private Dictionary<string, bool> mVisited;

        Graph()
        {
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
        public void topologicalSort(string[] inputArrOfStringTuples)
        {
            // parse string and fill edges
            solvePackageDependenciesHelper(inputArrOfStringTuples);


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


        /// <summary>
        /// Helper method that uses regex to parse string array into tuples. Performs
        /// circular dependency checks, and checks if a node has more then one dependent.
        /// Also populates the member queue and dictionary for further processing.
        /// </summary>
        /// <param name="sArr"></param>
        /// <returns></returns>
        private void solvePackageDependenciesHelper(string[] sArr)
        {
            // allowing regex to capture numbers so the testing is easier
            Regex regex = new Regex("(?<Dependent>[A-Za-z0-9]+)\\:\\s*(?<Dependee>[A-Za-z0-9]*)");

            String tempDependent = "";
            String tempDependee = "";


            foreach (string s in sArr)
            {
                Match match = regex.Match(s);

                if (match.Success)
                {
                    // grabbing respective groups using regex group functionality
                    tempDependent = match.Groups["Dependent"].Value.Trim();
                    tempDependee = match.Groups["Dependee"].Value.Trim();

                    if (tempDependee.Equals(""))
                    {
                        continue;
                    }
                    else
                    {
                        this.addEdge(tempDependee, tempDependent);
                    }

                    match.NextMatch();
                }
            }

            return;
        }




        static void Main(string[] args)
        {
            // Create a graph given in the above diagram
            Graph g = new Graph();
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

            g.topologicalSort(new string[] {"E: A", "F: B", "B: C", "C: D", "A: G", "G: D", "D: "});
            Console.ReadLine();
        }

    }


}
