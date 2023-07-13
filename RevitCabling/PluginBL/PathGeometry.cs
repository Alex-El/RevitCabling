using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitCabling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCabling.PluginBL
{
    internal class PathGeometry
    {
        const double PRECISION = 0.2;
        const double LASTSEGMENT = 3; // sign for path completed

        public List<XYZ> CurrentPath
        { 
            get => _currentPath;
            set
            {
                _currentPath = value;
                _onTray = false;
            }
        }

        List<XYZ> _currentPath = new List<XYZ>();
        bool _onTray = false;

        public bool PathValid()
        {
            if (Host.ProjectData.CurrentElectricalSystem != null)
            {
                return Host.ProjectData.CurrentElectricalSystem.IsCircuitPathValid(_currentPath);
            }
            return false;
        }

        public void ApplyCableTray(XYZ point1, XYZ point2)
        {
            // clear path from points which not on tray (first tray adding)
            if (!_onTray)
            {
                _currentPath = new List<XYZ>() { _currentPath[0], _currentPath[_currentPath.Count - 1] };
                _onTray = true;
            }

            (int nearestPt1Indx, int nearestPt2Indx) = GetNearestPts(point1, point2);

            // if already lay on tray - do nothing
            if (_currentPath[nearestPt1Indx].DistanceTo(point1) < PRECISION && _currentPath[nearestPt2Indx].DistanceTo(point2) < PRECISION)
            {
                //if (nearestPt1Indx != 0 && nearestPt1Indx != (_currentPath.Count - 1))
                //{
                //    _currentPath.RemoveAt(nearestPt1Indx);
                //}

                //if (nearestPt2Indx != 0 && nearestPt2Indx != (_currentPath.Count - 1))
                //{
                //    _currentPath.RemoveAt(nearestPt2Indx);
                //}

                return;
            }

            _currentPath = InsertPoints(nearestPt1Indx, point1, point2, nearestPt2Indx);

            if (IsPathCompleted())
            {
                AlignPath();
            }
        }

        // Z alignment
        private void AlignPath()
        {
            for (int i = 1; i < _currentPath.Count; i++)
            {
                AlignPoint(false, i);
            }
            //AlignPoint(false, 1);
        }

        private void AlignPoint(bool isForvard, int i)
        {
            XYZ correctedPt = _currentPath[i];
            XYZ targetPt;
            if (isForvard)
            {
                targetPt = _currentPath[i + 1];
            }
            else
            {
                targetPt = _currentPath[i - 1];
            }

            if (Math.Abs(correctedPt.Z - targetPt.Z) > PRECISION) // vertical case
            {
                correctedPt = new XYZ(targetPt.X, targetPt.Y, correctedPt.Z);
            }
            else // horizontal case
            {
                correctedPt = new XYZ(correctedPt.X, correctedPt.Y, targetPt.Z);
            }

            _currentPath[i] = correctedPt;
        }

        private bool IsPathCompleted()
        {
            if (_currentPath[_currentPath.Count - 1].DistanceTo(_currentPath[_currentPath.Count - 2]) < LASTSEGMENT)
            {
                return true;
            }
            return false;
        }

        private List<XYZ> InsertPoints(int nearestPt1Indx, XYZ point1, XYZ point2, int nearestPt2Indx)
        {
            List<XYZ> newPath = new List<XYZ>();
            bool inserted = false;

            for (int i = 0; i < _currentPath.Count; i++)
            {
                if (i == nearestPt1Indx && !inserted)
                {
                    List<XYZ> insertPart = LinkPoints(_currentPath[nearestPt1Indx], point1, point2, _currentPath[nearestPt2Indx]);
                    newPath = newPath.Concat(insertPart).ToList();
                    inserted = true;
                    continue;
                }
                if (i == nearestPt2Indx && !inserted)
                {
                    List<XYZ> insertPart = LinkPoints(_currentPath[nearestPt2Indx], point2, point1, _currentPath[nearestPt1Indx]);
                    newPath = newPath.Concat(insertPart).ToList();
                    inserted = true;
                    continue;
                }
                newPath.Add(_currentPath[i]);
            }

            return newPath;
        }

        /// <summary>
        /// main rule here
        /// connect by upper level
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <param name="pt3"></param>
        /// <param name="pt4"></param>
        /// <returns></returns>
        private List<XYZ> LinkPoints(XYZ pathPt1, XYZ trayPt1, XYZ trayPt2, XYZ pathPt2)
        {
           
            List <XYZ> separated = DetectSeparated(pathPt1, trayPt1, trayPt2, pathPt2); // 2 pts minimum
            var result = new List<XYZ>() { separated[0] };

            for (int i = 1; i < separated.Count - 1; i++)
            {
                result = AddUnique(result, Link2Points(separated[i - 1], separated[i]));
            }

            return result;
        }

        private List<XYZ> AddUnique(List<XYZ> beforeList, List<XYZ> newList)
        {
            foreach (var newPt in newList)
            {
                var distances = beforeList.Select(s => s.DistanceTo(newPt));
                if (distances.All(s => s > PRECISION))
                {
                    beforeList.Add(newPt);
                }
            }

            return beforeList;
        }

        private List<XYZ> Link2Points(XYZ pt1, XYZ pt2)
        {
            var result = new List<XYZ>();

            if (Math.Abs(pt1.Z - pt2.Z) < PRECISION) // lay on one level
            {
                result.Add(pt1);
                result.Add(pt2);
            }
            else if (Math.Abs(pt1.Z - pt2.Z) < PRECISION) // move to upper lavel
            {
                double upperLevel = Math.Max(pt1.Z, pt2.Z);
                if (pt1.Z == upperLevel)
                {
                    result.Add(pt1);
                    result.Add(new XYZ(pt1.X, pt1.Y, upperLevel));
                }
                else
                {
                    result.Add(new XYZ(pt2.X, pt2.Y, upperLevel));
                    result.Add(pt2);
                }
            }
            else // add 3rd point
            {
                double upperLevel = Math.Max(pt1.Z, pt2.Z);
                XYZ middlePt;
                if (pt1.Z == upperLevel)
                {
                    middlePt = new XYZ(pt2.X, pt2.Y, upperLevel);
                }
                else
                {
                    middlePt = new XYZ(pt1.X, pt1.Y, upperLevel);
                }

                result.Add(pt1);
                result.Add(middlePt);
                result.Add(pt2);
            }

            return result;
        }

        // level diff may be < PRESISION
        private List<XYZ> DetectSeparated(XYZ pathPt1, XYZ trayPt1, XYZ trayPt2, XYZ pathPt2)
        {
            var result = new List<XYZ>();

            if (IsSeparated(pathPt1, trayPt1))
            {
                if (IsSeparated(trayPt2, pathPt2))
                {
                    if (IsSeparated(trayPt1, trayPt2))
                    {
                        // 1234
                        result.Add(pathPt1);
                        result.Add(trayPt1);
                        result.Add(trayPt2);
                        result.Add(pathPt2);
                    }
                    else
                    {
                        // 124
                        result.Add(pathPt1);
                        result.Add(trayPt1);
                        result.Add(pathPt2);
                    }
                }
                else
                {
                    if (IsSeparated(trayPt1 , pathPt2))
                    {
                        // 124
                        result.Add(pathPt1);
                        result.Add(trayPt1);
                        result.Add(pathPt2);
                    }
                    else
                    {
                        // 14
                        result.Add(pathPt1);
                        result.Add(pathPt2);
                    }
                }
            }
            else
            {
                if (IsSeparated(trayPt2, pathPt2))
                {
                    if (IsSeparated(pathPt1, trayPt2))
                    {
                        // 134
                        result.Add(pathPt1);
                        result.Add(trayPt2);
                        result.Add(pathPt2);
                    }
                    else
                    {
                        // 14
                        result.Add(pathPt1);
                        result.Add(pathPt2);
                    }
                }
                else
                {
                    // 14
                    result.Add(pathPt1);
                    result.Add(pathPt2);
                }
            }

            return result;
        }

        private bool IsSeparated(XYZ pt1, XYZ pt2)
        {
            if (pt1.DistanceTo(pt2) < PRECISION)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// start from panel and detect first tray point
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        private (int, int) GetNearestPts(XYZ point1, XYZ point2)
        {
            // detect start edge
            List<double> distancesPt1 = _currentPath.Select(s => point1.DistanceTo(s)).ToList();
            double minDistPt1 = distancesPt1.Min();
            int minDistPt1Indx = distancesPt1.IndexOf(minDistPt1);

            List<double> distancesPt2 = _currentPath.Select(s => point2.DistanceTo(s)).ToList();
            double minDistPt2 = distancesPt2.Min();
            int minDistPt2Indx = distancesPt2.IndexOf(minDistPt2);


            if (minDistPt1Indx == minDistPt2Indx)
            {
                if (minDistPt1 < minDistPt2) // point 1 first
                {
                    distancesPt2 = new List<double>();
                    for (int i = 0; i < _currentPath.Count; i++)
                    {
                        if (i <= minDistPt1Indx)
                        {
                            distancesPt2.Add(100000);
                        }
                        else
                        {
                            distancesPt2.Add(point2.DistanceTo(_currentPath[i]));
                        }
                    }
                    minDistPt2 = distancesPt2.Min();
                    minDistPt2Indx = distancesPt2.IndexOf(minDistPt2);
                }
                else // point 2 first
                {
                    distancesPt1 = new List<double>();
                    for (int i = 0; i < _currentPath.Count; i++)
                    {
                        if (i <= minDistPt2Indx)
                        {
                            distancesPt1.Add(100000);
                        }
                        else
                        {
                            distancesPt1.Add(point1.DistanceTo(_currentPath[i]));
                        }
                    }
                    minDistPt1 = distancesPt1.Min();
                    minDistPt1Indx = distancesPt1.IndexOf(minDistPt1);
                }
            }

            return (minDistPt1Indx,  minDistPt2Indx);
        }

    }
}
