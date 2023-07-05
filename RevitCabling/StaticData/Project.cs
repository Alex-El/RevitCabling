using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Electrical;
using System.Collections.Generic;

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
    }
}
