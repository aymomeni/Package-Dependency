using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DependencyExercise;

namespace DependencyExerciseTester
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string[] strArrGiven = new string[] { "KittenService: CamelCaser", "CamelCaser: " };
            DependencySolver dS = new DependencySolver();
            string actual = dS.solvePackageDependencies(strArrGiven);
            string expected = "CamelCaser, KittenService";

            Assert.AreEqual(expected, actual, "Test1 Failed");  
        }

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

        [TestMethod]
        public void TestMethod3()
        {
            string[] strArrGiven = new string[] { "KittenService: ", "Leetmeme: Cyberportal", "Cyberportal: Ice", "CamelCaser: KittenService", "Fraudstream: Leetmeme", "Ice: " };
            string actual = "";
            string expected = "Invalid";

            Assert.AreEqual(expected, actual, "Test3 Failed (should return invalid)");
        }



    }
}
