using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RayProcessor.Lib.Tests
{
    [TestClass]public class TreeTest
    {
        [TestMethod]
        public void BuildTreeTest()
        {
            Tree tree = new Tree();
            FileManager fileManager = new FileManager();
            fileManager.ReadObj(@"cow.obj", tree);
            Console.WriteLine();

            Assert.AreEqual(tree.Root.Childs.Count, 10);
            Assert.AreEqual(tree.Root.MBB.Volume, 64);
        }
    }
}