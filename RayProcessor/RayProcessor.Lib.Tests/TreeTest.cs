using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RayProcessor.Lib.Tests
{
    [TestClass]public class TreeTest
    {
        [TestMethod]
        public void NotOverloadedTreeTest()
        {
            Tree.Tree tree = new Tree.Tree();
            
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                Point point1 = new Point(random.Next(5), random.Next(5), random.Next(5));
                Point point2 = new Point(random.Next(5), random.Next(5), random.Next(5));
                Point point3 = new Point(random.Next(5), random.Next(5), random.Next(5));
                tree.AddTriangle(new Triangle(point1,point2,point3));
            }
            
            Assert.AreEqual(tree.Root.Childs.Count, 10);
            Assert.AreEqual(tree.Root.MBB.Volume, 64);
        }
    }
}