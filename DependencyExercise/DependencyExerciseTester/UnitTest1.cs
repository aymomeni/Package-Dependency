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
            string actual = "";
            string expected = "CamelCaser, KittenService";

            Assert.AreEqual(expected, actual, "Test1 Failed");  
        }

        [TestMethod]
        public void TestMethod2()
        {
            string[] strArrGiven = new string[] { "KittenService: ", "Leetmeme: Cyberportal", "Cyberportal: Ice", "CamelCaser: KittenService", "Fraudstream: Leetmeme", "Ice: " };
            string actual = "";
            string expected = "CamelCaser, KittenService";

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
