using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

/*
 * @author: Ali Momeni 
 * Date: Feb, 22 2018
 * 
 * Package Installer Exercise supplied by Pluralsight
 */
namespace DependencyExercise
{
    public class DependencySolver
    {
        // Member variables
        private int mEdgeDependencyFollowupCount = 0;
        private bool mInvalidCircularDependencyBool = false;
        private Queue<string> mQueue = new Queue<string>();
        private Dictionary<string, string> mDictionaryDependeeDependent = new Dictionary<string, string>();

        /// <summary>
        /// returns a comma seperated string of package names in the order of install,
        /// such that a package's dependency will always precede that package.
        /// </summary>
        /// <param name="inputArrOfStringTuples"></param>
        /// <returns>string of packages in topological order, or string "invalid" if an invalid 
        /// string array was passed in (dependency is cyclic or has more than one dependent per dependee)</returns>
        public string solvePackageDependencies(string[] inputArrOfStringTuples)
        {
            if (inputArrOfStringTuples.Length == 0 || inputArrOfStringTuples == null) { return "invalid"; }
        
            HashSet<string> result = new HashSet<string>();

            // processing the strings in the array &
            // populating queue and dict.
            solvePackageDependenciesHelper(inputArrOfStringTuples);
            if (mInvalidCircularDependencyBool == true)
            {
                return "invalid";
            }

            string tempKey = "";
            string tempValue = "";
            
            // as long as there is more nodes in the queue or more edges
            // where dependencies must be checked
            while(mQueue.Count > 0 || mEdgeDependencyFollowupCount > 0) {

                if (mQueue.Count > 0)
                {
                    tempKey = mQueue.Dequeue();
                }

                try
                {
                    if (mDictionaryDependeeDependent.ContainsKey(tempKey))
                    {
                        mDictionaryDependeeDependent.TryGetValue(tempKey, out tempValue);
                        mQueue.Enqueue(tempValue);
                        result.Add(tempKey + ", ");
                        mEdgeDependencyFollowupCount--;
                    }
                    else if (mQueue.Count == 0 && mEdgeDependencyFollowupCount == 0)
                    {
                        // break out so no comma is added 
                        // once the last element is reached
                        break; 
                        
                    }
                    else
                    {
                        if (result.Contains(tempKey + ", "))
                        {
                            return "invalid";
                        } else {
                            result.Add(tempKey + ", ");
                        }
                    }
                }
                catch (ArgumentNullException e) { Console.WriteLine(e.ToString()); }
                
            }
            result.Add(tempKey);
            return string.Join("", result);
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

            String tempDependent    = "";
            String tempDependee     = "";
            
            foreach(string s in sArr) 
            {
                Match match = regex.Match(s);

                if (match.Success)
                {
                    // grabbing respective groups using regex group functionality
                    tempDependent = match.Groups["Dependent"].Value.Trim();
                    tempDependee  = match.Groups["Dependee"].Value.Trim();

                    if (tempDependee.Equals(""))
                    {
                        // if dependee is empty we know that the node is good starting node as 
                        // it doesn't have any dependents (a root node) -> add it queue (FIFO)
                        mQueue.Enqueue(tempDependent);
                    }
                    else
                    {
                        // else the node can not be a root node and the dependee of this node
                        // must be checked for further dependents
                        if (mDictionaryDependeeDependent.ContainsKey(tempDependee))
                        {
                            mInvalidCircularDependencyBool = true;
                        }
                        try
                        {
                            mDictionaryDependeeDependent.Add(tempDependee, tempDependent);
                        }
                        catch (ArgumentException)
                        {
                            mInvalidCircularDependencyBool = true;
                            return; 
                        }
                        mEdgeDependencyFollowupCount++;
                    }

                    match.NextMatch();
                }
            }

            // if the queue has no elements
            // in the queue it must be a circular dependence
            if (mQueue.Count == 0)
            {
                mInvalidCircularDependencyBool = true;
            }
            return;
        }
    }
}
