

using System.Collections.Generic;

namespace RayProcessor.Lib
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

        public HitInfo GetClosestHit(Ray ray, Branch currentNode)
        {
            if (!currentNode.MBB.Intersects(ray))
            {
                return new HitInfo();
            }

            if (currentNode.Childs[0] is Leaf)
            {
                HitInfo closesHit = new HitInfo();
                foreach (Leaf child in currentNode.Childs)
                {
                    (bool intersected, Point intersection) = child.Triangle.CrossesTriangle(ray);
                    if (intersected)
                    {
                        if (!closesHit.hit)
                        {
                            closesHit = new HitInfo(true, child.Triangle, intersection);
                        }
                        else if ((ray.StartPoint - intersection).magnitude <
                                (ray.StartPoint - closesHit.hitPoint).magnitude)
                        {
                            closesHit = new HitInfo(true, child.Triangle, intersection);
                        }
                    }
                }

                return closesHit;
            }
            else
            {
                HitInfo hit1 = GetClosestHit(ray, ( currentNode).Childs[0] as Branch);
                HitInfo hit2 = GetClosestHit(ray, ( currentNode).Childs[1] as Branch);
                if (!hit1.hit && !hit2.hit)
                {
                    return new HitInfo();
                }

                if (!hit1.hit)
                {
                    return hit2;
                }

                if (!hit2.hit)
                {
                    return hit1;
                }
                if ((ray.StartPoint - hit1.hitPoint).magnitude >
                    (ray.StartPoint - hit2.hitPoint).magnitude)
                {
                    return hit2;
                }

                return hit1;
            }
        }
    }
}