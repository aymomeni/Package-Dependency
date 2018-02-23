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
    class DependencySolver
    {
        // Member variables
        // dictionary ? and queue?
        private Queue<string> mQueue = new Queue<string>();
        private Dictionary<string, string> mDictionaryDependeeDependent = new Dictionary<string, string>();

        /// <summary>
        /// returns a comma seperated string of package names in the order of install,
        /// such that a package's dependency will always precede that package.
        /// </summary>
        /// <param name="inputArrOfStringTuples"></param>
        /// <returns>string of packages in topological order, or null if an invalid string array was given 
        /// (dependency is cyclic or has more than one dependent per dependee)</returns>
        public static string solvePackageDependencies(string[] inputArrOfStringTuples)
        {

            return null;
        }

        /// <summary>
        /// Helper method that uses regex to parse string array into tuples
        /// </summary>
        /// <param name="sArr"></param>
        /// <returns></returns>
        private static void parseArrHelper(string[] sArr)
        {
            Regex regex = new Regex("(?<Dependent>[A-Za-z0-9]+)\\:\\s*(?<Dependee>[A-Za-z0-9]*)");

            String tempDependent    = "";
            String tempDependee     = "";
            
            foreach(string s in sArr) 
            {
                Match match = regex.Match(s);

                if (match.Success)
                {
                    tempDependent = match.Groups["Dependent"].Value.Trim();
                    tempDependee = match.Groups["Dependee"].Value.Trim();

                    // if dependee is empty we know that the node is good starting node as 
                    // it doesn't have any dependents (a root node) -> add it queue (FIFO)

                    if (tempDependent.Equals(""))
                    {
                        //mQueue
                    }

                    match.NextMatch();
                    Console.WriteLine("Dependent: " + tempDependent + " Dependee: " + tempDependee);
                }
            }

            return;
        }


        static void Main(string[] args)
        {
            string[] strArrGiven = new string[] { "KittenService: ", "Leetmeme: Cyberportal", "Cyberportal: Ice", "CamelCaser: KittenService", "Fraudstream: Leetmeme", "Ice: " };

            parseArrHelper(strArrGiven);
            Console.ReadLine();
        }
    }
}
