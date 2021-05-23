using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RayProcessor.Lib
{
    public class Branch:Node
    {
        public MBB MBB
        {
            get => _mbb;
        }

        public List<Node> Childs
        {
            get => _childs;
        }

        private static int _maxChild = 10;
        private static int _minChild = 4;
        private List<Node> _childs;
        private Branch _mother;
        private MBB _mbb;

        public Branch(List<Node> childs, Branch mother, MBB mbb)
        {
            _childs = childs;
            _mother = mother;
            _mbb = mbb;
        }

        public Branch()
        {
            _childs = new List<Node>();
        }

        public Branch(Branch mother, MBB mbb)
        {
            _mother = mother;
            _mbb = mbb;
        }

        public override string GetNodeType()
        {
            if (_childs.Count == 0 || _childs[0].GetNodeType() == "Leaf")
            {
                return "PreBranch";
            }

            return "Branch";
        }

        public List<Node> GetChilds()
        {
            return _childs;
        }

        public void RefindVolume(Triangle triangle)
        {
            if (_mbb == null)
            {
                InitMBB(triangle);
            }
            else
            {
                MBB.AddTriangle(triangle);
            }
        }

        public void AddChild(Triangle triangle)
        {
            AddChild(new Leaf(this, triangle));
        }

        private void AddChild(Leaf leaf)
        {
            _childs.Add(leaf);
            RefindVolume(leaf.Triangle);
            if (_childs.Count > _maxChild)
            {
                DivideBranch();
            }
        }

        private void DivideBranch()
        {
            char axis = ChooseSplitAxis();
            Leaf[] sortedLeaf = GetChilds().Select(node => (Leaf) node).ToArray();
            if (axis == 'x')
            {
                Array.Sort(sortedLeaf, new LeafComparerX());
            }
            else if(axis == 'y')
            {
                Array.Sort(sortedLeaf, new LeafComparerY());
            }
            else
            {
                Array.Sort(sortedLeaf, new LeafComparerZ());
            }
        
            int splitIndex = ChooseSplitIndex(sortedLeaf);
            Branch firstChild = new Branch();
            Branch secondChild = new Branch();
            for (int i = 0; i < sortedLeaf.Length; i++)
            {
                if (i < splitIndex)
                {
                    firstChild.AddChild(sortedLeaf[i]);
                }
                else
                {
                    secondChild.AddChild(sortedLeaf[i]);
                }
            }
        
            _childs = new List<Node> {firstChild, secondChild};
        }

        private void InitMBB(Triangle triangle)
        {
            _mbb = new MBB(triangle);
        }

        private char ChooseSplitAxis()
        {
            if (MinMarginDiv(new LeafComparerX()) < MinMarginDiv(new LeafComparerY()))
            {
                if (MinMarginDiv(new LeafComparerX()) < MinMarginDiv(new LeafComparerZ()))
                {
                    return 'x';
                }
                return 'z';
            }
            return 'y';
        }

        private int ChooseSplitIndex(Leaf[] sortedLeaf)
        {
            int k = _maxChild - 2 * _minChild + 2;
            double[] overlapShapes = new double[k];
            double[] volumes = new double[k];
            for (int i = 1; i <= k; i++)
            {
                MBB mbb1 = new MBB();
                MBB mbb2 = new MBB();
                for (int j = 0; j < sortedLeaf.Length; j++)
                {
                    if (j < _minChild - 1 + i)
                    {
                        mbb1.AddTriangle(sortedLeaf[j].Triangle);
                    }
                    else
                    {
                        mbb2.AddTriangle(sortedLeaf[j].Triangle);
                    }
                }
        
                overlapShapes[i - 1] = MBB.CheckOverlapVolume(mbb1, mbb2);
                volumes[i - 1] = mbb1.Volume + mbb2.Volume;
            }
        
            (double minOverlap, int splitIndex) = MinOfArray(overlapShapes);
            int repeatedMinOverlap = 0;
            for (int i = 0; i < overlapShapes.Length; i++)
            {
                if (overlapShapes[i] == minOverlap)
                {
                    repeatedMinOverlap++;
                }
            }
        
            if (repeatedMinOverlap == 1)
            {
                return splitIndex + _minChild;
            }
        
            (_, splitIndex) = MinOfArray(volumes);
            return splitIndex + _minChild;
        }
        
        private double MinMarginDiv(IComparer<Leaf> comparer)
        {
            int k = _maxChild - 2 * _minChild + 2;
            Leaf[] sorted = GetChilds().Select(node => (Leaf) node).ToArray();
            Array.Sort(sorted, comparer);
            double[] S = new double[k];
            for (int i = 1; i <= k; i++)
            {
                MBB mbb1 = new MBB();
                MBB mbb2 = new MBB();
                for (int j = 0; j < sorted.Length; j++)
                {
                    if (j < _minChild - 1 + i)
                    {
                        mbb1.AddTriangle(sorted[j].Triangle);
                    }
                    else
                    {
                        mbb2.AddTriangle(sorted[j].Triangle);
                    }
                }
        
                S[i - 1] = mbb1.Margin + mbb2.Margin;
            }
        
            (double result, _) = MinOfArray(S);
            return result;
        }
        
        private (double, int) MinOfArray(double[] array)
        {
            double min = array[0];
            int minId = 0;
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < min)
                {
                    min = array[i];
                    minId = i;
                }
            }
        
            return (min, minId);
        }
    }
}