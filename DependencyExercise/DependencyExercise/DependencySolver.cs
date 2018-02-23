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
        private int necessaryFollowups = 0;
        private bool invalid = false;
        private Queue<string> mQueue = new Queue<string>();
        private Dictionary<string, string> mDictionaryDependeeDependent = new Dictionary<string, string>();

        public DependencySolver(){

        }

        /// <summary>
        /// returns a comma seperated string of package names in the order of install,
        /// such that a package's dependency will always precede that package.
        /// </summary>
        /// <param name="inputArrOfStringTuples"></param>
        /// <returns>string of packages in topological order, or null if an invalid string array was given 
        /// (dependency is cyclic or has more than one dependent per dependee)</returns>
        public string solvePackageDependencies(string[] inputArrOfStringTuples)
        {
            if (inputArrOfStringTuples.Length == 0) { return null; }
            else if (inputArrOfStringTuples.Length == 1) { return inputArrOfStringTuples[0]; }
        
            string result = "";

            //TODO: could there only be 2 elements?

            // processing the strings in the array &
            // populating queue and dict.
            solvePackageDependenciesHelper(inputArrOfStringTuples);
            if (invalid == true)
            {
                return "invalid";
            }

            string tempKey = "";
            string tempValue = "";
            
            while(mQueue.Count > 0 || necessaryFollowups > 0) {
                tempKey = mQueue.Dequeue();

                try
                {
                    if (mDictionaryDependeeDependent.ContainsKey(tempKey))
                    {
                        mDictionaryDependeeDependent.TryGetValue(tempKey, out tempValue);
                        mQueue.Enqueue(tempValue);
                        result += tempKey + ", ";
                        necessaryFollowups--;
                    }
                    else if (mQueue.Count == 0 && necessaryFollowups == 0)
                    {
                        break;
                    }
                    else
                    {
                        result += tempKey + ", ";
                    }
                }
                catch (ArgumentNullException e) { Console.WriteLine(e.ToString()); }
                
            }
            result += tempKey;
            return result;
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
            // allowing regex to capture numbers so the testing is easier (I guess one could use only single letters but there is only 26 :)
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
                            invalid = true;
                        }
                        mDictionaryDependeeDependent.Add(tempDependee, tempDependent);
                        necessaryFollowups++;
                    }

                    // TODO: What if we find at this point that this is cyclic or not based on specification?
                    // if queue is empty something is wrong or if there is more then one dependent per dependee

                    match.NextMatch();
                    Console.WriteLine("Dependent: " + tempDependent + " Dependee: " + tempDependee);
                }
            }

            // if the queue has no elements
            // in the queue it must be a circular dependence
            if (mQueue.Count == 0)
            {
                invalid = true;
            }
            return;
        }


        static void Main(string[] args)
        {

            // Quick check if regex works
            //string[] strArrGiven = new string[] { "KittenService: ", "Leetmeme: Cyberportal", "Cyberportal: Ice", "CamelCaser: KittenService", "Fraudstream: Leetmeme", "Ice: " };

            //parseArrHelper(strArrGiven);
            //Console.ReadLine();
        }
    }
}
