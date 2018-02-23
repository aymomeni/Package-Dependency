using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DependencyExercise;

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

        [TestMethod]
        public void TestMethod5()
        {
            string[] strArrGiven = new string[] { "KittenService: ", "Leetmeme: Cyberportal", "Cyberportal: Ice", "CamelCaser: KittenService", "Fraudstream: Leetmeme", "Ice: " };
            DependencySolver dS = new DependencySolver();
            string actual = "";
            string expected = "Invalid";

            Assert.AreEqual(expected, actual, "Test5 Failed (should return invalid)");
        }



    }
}
