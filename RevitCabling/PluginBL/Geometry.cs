using Autodesk.Revit.DB;
using RevitCabling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCabling.PluginBL
{
    internal static class Geometry
    {
        public static List<XYZc> CorrectCircuitPath(XYZ point1, XYZ point2)
        {
            const double Precision = 0.15;

            var initialPath = Host.ProjectData.CorrectingPath;

            if (initialPath == null ) 
            {
                throw new Exception("CorrectingPath == null");
            }


            var newPath = new List<XYZc>();
            XYZc ptEnd0 = null;
            XYZc ptEnd1 = null;
            XYZc basePt  = null;
            double gap = 0;
            bool isReverse = false;

            // detect 2 free pt (if not set them free)
            if (PtOnThePath(initialPath, point1, Precision) && PtOnThePath(initialPath, point2, Precision))
            {
                // set free
                foreach (XYZc p in initialPath)
                {
                    if ((p.DistanceTo(point1) < Precision) || (p.DistanceTo(point2) < Precision))
                    {
                        var np = new XYZc(new XYZ(p.X, p.Y, p.Z + 10));
                        np.IsOnTray = false;
                        newPath.Add(np);
                    }
                    else
                    {
                        newPath.Add(p);
                    }
                }
                return newPath;
            }

            // if all path not on trays get start and end pts
            if (initialPath.All(s => s.IsOnTray == false))
            {
                var basesPts = new List<XYZc>() { initialPath[0], initialPath[initialPath.Count-1] };
                (basePt, ptEnd0, ptEnd1) = GetBaseZeroOnePts(basesPts, point1, point2);

                if (initialPath[0].DistanceTo(basePt) < Precision)
                {
                    isReverse = false;
                }
                else
                {
                    isReverse = true;
                }
            }
            else
            {
                var basesPts = new List<XYZc>() { initialPath[0] };

                foreach(XYZc p in initialPath)
                {
                    if (p.IsOnTray == true)
                    {
                        basesPts.Add(p);
                    }
                }

                basesPts.Add(initialPath[initialPath.Count - 1]);

                (basePt, ptEnd0, ptEnd1) = GetBaseZeroOnePts(basesPts, point1, point2);

                isReverse = false;
                foreach(XYZc p in initialPath)
                {
                    if(p.IsOnTray == false)
                    {
                        isReverse = true;
                        break;
                    }
                    if (p.DistanceTo(basePt) < Precision)
                    {
                        break;
                    }
                }
            }

            gap = basePt.DistanceTo(ptEnd1);


            if (isReverse)
            {
                initialPath.Reverse();
            }

            // add (merge) pts
            bool inserted = false;
            foreach(XYZc p in initialPath)
            {
                if (p.IsOnTray == true && !inserted)
                {
                    newPath.Add(p);
                    continue;
                }

                if (!inserted)
                {
                    basePt.IsOnTray = true;
                    ptEnd0.IsOnTray = true;
                    ptEnd1.IsOnTray = true;
                    newPath.Add(basePt);
                    newPath.Add(ptEnd0);
                    newPath.Add(ptEnd1);
                    inserted = true;
                    continue;
                }

                if (inserted && p.DistanceTo(basePt) > gap)
                {
                    newPath.Add(p);
                }

            }

            // revers back
            if (isReverse)
            {
                initialPath.Reverse();
            }

            // merge nearest
            for (int i = 0; i < newPath.Count - 2; i++)
            {
                if (newPath[i].DistanceTo(newPath[i + 1]) < Precision)
                {
                    newPath.RemoveAt(i);
                }
            }

            return newPath;

            //XYZ startPt;
            //XYZ endPt;
            //if (point1.DistanceTo(initialPath[0]) < point2.DistanceTo(initialPath[0]))
            //{
            //    startPt = point1;
            //    endPt = point2;
            //}
            //else
            //{
            //    startPt = point2;
            //    endPt = point1;
            //}

            //List<XYZ> path = new List<XYZ>();
            //bool stepBefore = true;
            //XYZ pt0 = initialPath[0];
            //XYZ pt1 = initialPath[initialPath.Count - 1];
            //initialPath.RemoveAt(0);
            //initialPath.RemoveAt(initialPath.Count - 1);
            //path.Add(pt0);

            //foreach (XYZ pt in initialPath)
            //{
            //    if (stepBefore)
            //    {
            //        if (pt0.DistanceTo(pt) < pt0.DistanceTo(startPt))
            //        {
            //            path.Add(pt);
            //        }
            //        else
            //        {
            //            path.Add(startPt);
            //            path.Add(endPt);
            //            stepBefore = false;
            //        }
            //    }
            //    else
            //    {
            //        if (pt1.DistanceTo(pt) < pt1.DistanceTo(endPt))
            //        {
            //            path.Add(pt);
            //        }
            //    }
            //}
            //path.Add(pt1);

            ////return path;
        }

        // return BasePt, ZeroPt, OnePt
        static (XYZc, XYZc, XYZc) GetBaseZeroOnePts(List<XYZc> path, XYZ pt1, XYZ pt2)
        {
            XYZc basePt = null;
            XYZc zeroPt = null;
            XYZc onePt = null;

            double minDistPt1 = path.Select(s => s.DistanceTo(pt1)).Min();
            double minDistPt2 = path.Select(s => s.DistanceTo(pt2)).Min();

            if (minDistPt1 < minDistPt2)
            {
                zeroPt = new XYZc(pt1);
                onePt = new XYZc(pt2);
            }
            else
            {
                zeroPt = new XYZc(pt2);
                onePt = new XYZc(pt1);
            }
            
            int indxMin = path.Select(s => s.DistanceTo(zeroPt))
                .ToList()
                .IndexOf(path.Select(s => s.DistanceTo(zeroPt))
                .Min());

            basePt = path[indxMin];

            return (basePt, zeroPt, onePt);
        }

        static bool PtOnThePath(List<XYZc> path, XYZ pt, double precision)
        {
            foreach (XYZc p in path)
            {
                if (p.DistanceTo(pt) < precision && p.IsOnTray == true)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
