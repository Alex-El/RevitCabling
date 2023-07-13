using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Electrical;
using RevitCabling.Models;
using RevitCabling.PluginBL;
using System.Collections.Generic;
using System.Linq;

namespace RevitCabling.StaticData
{
    internal class Project
    {
        public List<CableTray> CableTrays { get; set; } = new List<CableTray>();
        public List<ElementId> TextNoteIds { get; set; } = new List<ElementId>();

        //public List<XYZ> CurrentPath 
        //{
        //    get => _currentPath;
        //    set
        //    {
        //        _currentPath = value;
        //        _correctingPath = value.Select(s => new XYZc(s)).ToList();
        //    }
        //}
        //List<XYZ> _currentPath = new List<XYZ>();

        public Reference CurrentFixture { get; set; } = null;
        public Reference CurrentCableTray { get; set; } = null;
        public List<ElementId> PathElementIds { get; set; } = new List<ElementId>();
        public PathGeometry Path { get; private set; }

        public Project() 
        { 
            Path = new PathGeometry();
        }


        //public List<XYZc> CorrectingPath 
        //{ 
        //    get => _correctingPath; 
        //    set
        //    {
        //        _correctingPath = value;
        //        _currentPath = value.Select(s => s as XYZ).ToList();
        //    } 
        //}
        //List<XYZc> _correctingPath = new List<XYZc>();
    }
}
