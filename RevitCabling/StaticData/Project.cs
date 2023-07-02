using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Electrical;
using System.Collections.Generic;

namespace RevitCabling.StaticData
{
    internal class Project
    {
        public List<CableTray> CableTrays { get; set; } = new List<CableTray>();
        public List<ElementId> TextNoteIds { get; set; } = new List<ElementId>();
    }
}
