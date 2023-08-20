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
        public Reference CurrentFixture { get; set; } = null;
        public ElectricalSystem CurrentElectricalSystem { get; set; } = null;
        public Reference CurrentCableTray { get; set; } = null;
        public List<ElementId> PathElementIds { get; set; } = new List<ElementId>();
        public PathGeometry Path { get; private set; }
        public List<(Reference, string)> CurrentCableTraysWithCables { get; private set; } = new List<(Reference, string)>();

        public Project() 
        { 
            Path = new PathGeometry();
        }

        public void AddCurrentCableToParam()
        {
            CurrentCableTraysWithCables.Add((CurrentCableTray, CurrentElectricalSystem.Name));
        }

        public void ClearCableTraysWithCables()
        {
            CurrentCableTraysWithCables.Clear();
        }
    }
}
