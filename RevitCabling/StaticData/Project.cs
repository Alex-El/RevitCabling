using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Electrical;
using RevitCabling.Models;
using System.Collections.Generic;
using System.Linq;

namespace RevitCabling.StaticData
{
    internal class Project
    {
        public List<CableTray> CableTrays { get; set; } = new List<CableTray>();
        public List<ElementId> TextNoteIds { get; set; } = new List<ElementId>();
        public List<XYZ> CurrentPath { get; set; } = new List<XYZ>();
        public Reference CurrentFixture { get; set; } = null;
        public Reference CurrentCableTray { get; set; } = null;
        public List<ElementId> PathElementIds { get; set; } = new List<ElementId>();

        public List<XYZc> CorrectingPath 
        { 
            get => _correctingPath; 
            set
            {
                _correctingPath = value;
                CurrentPath = value.Select(s => s as XYZ).ToList();
            } 
        }
        List<XYZc> _correctingPath = new List<XYZc>();
    }
}
