using RayProcessor.Lib;

namespace RayProcessor.Tree
{
    public class Tree
    {
        private Branch _root;

        public Branch Root => _root;

        public Tree()
        {
            _root = new Branch();
        }

        public void AddTriangle(Triangle triangle)
        {
            Branch destinationBranch = ChooseSubtree(triangle);
            destinationBranch.AddChild(triangle);
        }

        private Branch ChooseSubtree(Triangle triangle)
        {
            Branch currentBranch = _root;
            currentBranch.RefindVolume(triangle);

            while (true)
            {
                if (currentBranch.GetNodeType() == "PreBranch")
                {
                    return currentBranch;
                }

                Branch childBranch1 = (Branch) currentBranch.GetChilds()[0];
                Branch childBranch2 = (Branch) currentBranch.GetChilds()[1];
                if (childBranch1.GetNodeType() == "PreBranch")
                {
                    if (MBB.CheckOverlapChange(childBranch1.MBB, childBranch2.MBB, triangle.Center ) !=
                        0)
                    {
                        currentBranch = MBB.CheckOverlapChange(childBranch1.MBB, childBranch2.MBB, triangle.Center) > 0
                            ? childBranch1
                            : childBranch2;
                    }
                    else
                    {
                        if (MBB.CheckVolumeChange(childBranch1.MBB, childBranch2.MBB, triangle.Center) != 0)
                        {
                            currentBranch = MBB.CheckVolumeChange(childBranch1.MBB, childBranch2.MBB, triangle.Center) > 0
                                ? childBranch1
                                : childBranch2;
                        }
                        else
                        {
                            currentBranch = childBranch1.MBB.Volume <= childBranch2.MBB.Volume
                                ? childBranch1
                                : childBranch2;
                        }
                    }
                }

                else
                {
                    if (MBB.CheckVolumeChange(childBranch1.MBB, childBranch2.MBB, triangle.Center) != 0)
                    {
                        currentBranch = MBB.CheckVolumeChange(childBranch1.MBB, childBranch2.MBB, triangle.Center) > 0
                            ? childBranch1
                            : childBranch2;
                    }
                    else
                    {
                        currentBranch = childBranch1.MBB.Volume <= childBranch2.MBB.Volume
                            ? childBranch1
                            : childBranch2;
                    }
                }

                currentBranch.RefindVolume(triangle);
            }
        }
    }
}