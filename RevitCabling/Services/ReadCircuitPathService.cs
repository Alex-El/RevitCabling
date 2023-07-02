using Autodesk.Revit.DB.Electrical;
using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Linq;
using System;
using Autodesk.Revit.UI;

namespace RevitCabling.Services
{
    internal class ReadCircuitPathService : ExternalEventCommand
    {
        public ReadCircuitPathService(string name) : base(name) { }

        public override void Execute()
        {
            if (Host.ProjectData.CurrentFixture == null)
            {
                return;
            }
                
            var doc = UIApplication.ActiveUIDocument.Document;
            var fi = doc.GetElement(Host.ProjectData.CurrentFixture) as FamilyInstance;

            ISet<ElectricalSystem> elSys = fi.MEPModel.GetElectricalSystems();
            ElectricalSystem system = fi.MEPModel.GetElectricalSystems().FirstOrDefault();

            if (system == null)
            {
                TaskDialog.Show("Warning", "No system on the fixture.");
                return;
            }

            Host.ProjectData.CurrentPath = system.GetCircuitPath().ToList();
        }
    }
}
