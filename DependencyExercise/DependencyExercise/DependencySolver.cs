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
