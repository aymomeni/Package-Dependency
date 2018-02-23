using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DependencyExercise;
using System.Text;
using System.Collections.Generic;

/*
 * @author: Ali Momeni 
 * Date: Feb, 22 2018
 * 
 * Package Installer Exercise supplied by Pluralsight
 */
namespace DependencyExerciseTester
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Simple first test
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            string[] strArrGiven = new string[] { "KittenService: CamelCaser", "CamelCaser: " };
            DependencySolver dS = new DependencySolver();
            string actual = dS.solvePackageDependencies(strArrGiven);
            string expected = "CamelCaser, KittenService";

            Assert.AreEqual(expected, actual, "Test1 Failed");  
        }

        /// <summary>
        /// Given input output test
        /// </summary>
        [TestMethod]
        public void TestMethod2()
        {
            string[] strArrGiven = new string[] { "1: ", 
                                                  "2: 3", 
                                                  "3: 4", 
                                                  "5: 1", 
                                                  "6: 2", 
                                                  "4: " };
            DependencySolver dS = new DependencySolver();
            string actual = dS.solvePackageDependencies(strArrGiven);
            string expected = "1, 4, 5, 3, 2, 6";

            Assert.AreEqual(expected, actual, "Test2 Failed");
        }

        /// <summary>
        /// Testing circular dependency
        /// </summary>
        [TestMethod]
        public void TestMethod3()
        {
            string[] strArrGiven = new string[] { "1: 2", 
                                                  "2: 3", 
                                                  "3: 4", 
                                                  "5: 6", 
                                                  "6: 7", 
                                                  "7: 1" };
            DependencySolver dS = new DependencySolver();
            string actual = dS.solvePackageDependencies(strArrGiven);
            string expected = "invalid";

            Assert.AreEqual(expected, actual, "Test3 Failed");
        }


        /// <summary>
        /// Testing if no dependency exists
        /// </summary>
        [TestMethod]
        public void TestMethod4()
        {
            string[] strArrGiven = new string[] { "1: ", 
                                                  "2: ", 
                                                  "3: ",
                                                  "4: ",
                                                  "5: ", 
                                                  "6: ", 
                                                  "7: " };
            DependencySolver dS = new DependencySolver();
            string actual = dS.solvePackageDependencies(strArrGiven);
            string expected = "1, 2, 3, 4, 5, 6, 7";

            Assert.AreEqual(expected, actual, "Test4 Failed");
        }

        /// <summary>
        /// Testing boarder cases of only a single package
        /// </summary>
        [TestMethod]
        public void TestMethod5()
        {
            string[] strArrGiven = new string[] { "1: "};

            DependencySolver dS = new DependencySolver();
            string actual = dS.solvePackageDependencies(strArrGiven);
            string expected = "1";

            Assert.AreEqual(expected, actual, "Test5 Failed");
        }

        /// <summary>
        /// Testing boarder cases where a dependency is dependent on itself
        /// </summary>
        [TestMethod]
        public void TestMethod6()
        {
            string[] strArrGiven = new string[] { "1: 1" };

            DependencySolver dS = new DependencySolver();
            string actual = dS.solvePackageDependencies(strArrGiven);
            string expected = "invalid";

            Assert.AreEqual(expected, actual, "Test6 Failed");
        }

        /// <summary>
        /// Testing boarder cases where a dependency is dependent on itself within
        /// small number of nodes
        /// </summary>
        [TestMethod]
        public void TestMethod7()
        {
            string[] strArrGiven = new string[] { "1: 2", "2: 3", "3: 1" };

            DependencySolver dS = new DependencySolver();
            string actual = dS.solvePackageDependencies(strArrGiven);
            string expected = "invalid";

            Assert.AreEqual(expected, actual, "Test6 Failed");
        }

        /// <summary>
        /// Testing larger number of simple dependencies
        /// </summary>
        [TestMethod]
        public void TestMethod8()
        {

            List<string> sList = new List<string>();
            sList.Add("1: ");

            for (int i = 2; i < 50; i += 1)
            {
                sList.Add(i + ": " + ((int)i-1));
            }

            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < sList.Count; i++)
            {

                if (i + 1 < sList.Count)
                {
                    sBuilder.Append((i + 1) + ", ");
                }
                else
                {
                    sBuilder.Append((i + 1));
                }
                
            }

            DependencySolver dS = new DependencySolver();
            string actual = dS.solvePackageDependencies(sList.ToArray());
            string expected = sBuilder.ToString();

            Assert.AreEqual(expected, actual, "Test8 Failed");
        }


        /// <summary>
        /// Larger number of nodes without any dependencies
        /// </summary>
        [TestMethod]
        public void TestMethod9()
        {

            List<string> sList = new List<string>();

            for (int i = 0; i < 50; i++)
            {
                sList.Add(i + ": ");
            }

            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < sList.Count; i++)
            {
                if (i + 1 < sList.Count)
                {
                    sBuilder.Append((i) + ", ");
                }
                else
                {
                    sBuilder.Append((i));
                }
            }

            DependencySolver dS = new DependencySolver();
            string actual = dS.solvePackageDependencies(sList.ToArray());
            string expected = sBuilder.ToString();

            Assert.AreEqual(expected, actual, "Test9 Failed");
        }


        [TestMethod]
        public void TestMethod10()
        {
            string[] strArrGiven = new string[] { "KittenService: ", "Leetmeme: Cyberportal", "Cyberportal: Ice", "CamelCaser: KittenService", "Fraudstream: ", "Ice: Leetmeme" };
            DependencySolver dS = new DependencySolver();
            string actual = dS.solvePackageDependencies(strArrGiven);
            string expected = "invalid";

            Assert.AreEqual(expected, actual, "Test5 Failed (should return invalid)");
        }
    }
}
