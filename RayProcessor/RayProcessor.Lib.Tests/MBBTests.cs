using Microsoft.VisualStudio.TestTools.UnitTesting;
using RayProcessor.Tree;

namespace RayProcessor.Lib.Tests
{
    [TestClass]public class MBBTests
    {
        [TestMethod]
        public void MBBCreationTest()
        {
            MBB mbb= new MBB();
            Triangle triangle = new Triangle(new Point(1, -1, 2),
                new Point(4, 2, 7), new Point(-5, 5, -6));
            mbb.AddTriangle(triangle);
            Assert.AreEqual(-5, mbb._xMin);
            Assert.AreEqual(4, mbb._xMax);
            Assert.AreEqual(-1, mbb._yMin);
            Assert.AreEqual(5, mbb._yMax);
            Assert.AreEqual(-6, mbb._zMin);
            Assert.AreEqual(7, mbb._zMax);
        }

        [TestMethod]
        public void AnotherCtorTest()
        {
            MBB mbb= new MBB();
            Triangle triangle = new Triangle(new Point(1, -1, 2),
                new Point(4, 2, 7), new Point(-5, 5, -6));
            mbb.AddTriangle(triangle);
            MBB mbb1 = new MBB(triangle);
            Assert.AreEqual(mbb1._xMin, mbb._xMin);
            Assert.AreEqual(mbb1._xMax, mbb._xMax);
            Assert.AreEqual(mbb1._yMin, mbb._yMin);
            Assert.AreEqual(mbb1._yMax, mbb._yMax);
            Assert.AreEqual(mbb1._zMin, mbb._zMin);
            Assert.AreEqual(mbb1._zMax, mbb._zMax);
        }

        [TestMethod]
        public void OverlapVolumeTest()
        {
            MBB mbb1 = new MBB(new Point(0, 0, 0), new Point(2, 2, 2));
            MBB mbb2 = new MBB(new Point(1, 1, 1), new Point(2, 2, 2));
            Assert.AreEqual(1, MBB.CheckOverlapVolume(mbb1,mbb2));
        }

        [TestMethod]
        public void OverlapVolumeChangeTest()
        {
            MBB mbb1 = new MBB(new Point(0, 0, 0), new Point(2, 2, 2));
            MBB mbb2 = new MBB(new Point(1, 1, 1), new Point(4, 4, 4));
            Assert.AreEqual(0, MBB.CheckOverlapChange(mbb2,mbb1,new Point(2,2,2)));
        }

        [TestMethod]
        public void VolumeChangeTest()
        {
            MBB mbb1 = new MBB(new Point(0, 0, 0), new Point(2, 2, 8));
            MBB mbb2 = new MBB(new Point(0, 0, 0), new Point(2, 2, 2));
            Assert.AreEqual(-30, MBB.CheckVolumeChange(mbb1,mbb2,new Point(3,3,0)));
        }
    }
}