using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCabling.PluginBL
{
    internal static class Geometry
    {
        public static List<XYZ> CorrectCircuitPath(List<XYZ> initialPath, XYZ point1, XYZ point2)
        {
            XYZ startPt;
            XYZ endPt;
            if (point1.DistanceTo(initialPath[0]) < point2.DistanceTo(initialPath[0]))
            {
                startPt = point1;
                endPt = point2;
            }
            else
            {
                startPt = point2;
                endPt = point1;
            }

            List<XYZ> path = new List<XYZ>();
            bool stepBefore = true;
            XYZ pt0 = initialPath[0];
            XYZ pt1 = initialPath[initialPath.Count - 1];
            initialPath.RemoveAt(0);
            initialPath.RemoveAt(initialPath.Count - 1);
            path.Add(pt0);

            foreach (XYZ pt in initialPath)
            {
                if (stepBefore)
                {
                    if (pt0.DistanceTo(pt) < pt0.DistanceTo(startPt))
                    {
                        path.Add(pt);
                    }
                    else
                    {
                        path.Add(startPt);
                        path.Add(endPt);
                        stepBefore = false;
                    }
                }
                else
                {
                    if (pt1.DistanceTo(pt) < pt1.DistanceTo(endPt))
                    {
                        path.Add(pt);
                    }
                }
            }
            path.Add(pt1);

            return path;
        }
    }
}
