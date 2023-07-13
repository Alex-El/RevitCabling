using Autodesk.Revit.DB.Electrical;
using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Linq;
using System;
using Autodesk.Revit.UI;
using System.Threading.Tasks;

namespace RevitCabling.Services
{
    internal class ReadCircuitPathService : ExternalEventService
    {
        public ReadCircuitPathService(string name) : base(name) { }

        protected override APIServiceResult Execute(UIApplication app)
        {
            if (Host.ProjectData.CurrentFixture == null)
            {
                return APIServiceResult.Failed;
            }
                
            var doc = app.ActiveUIDocument.Document;
            var fi = doc.GetElement(Host.ProjectData.CurrentFixture) as FamilyInstance;

            ISet<ElectricalSystem> elSys = fi.MEPModel.GetElectricalSystems();
            ElectricalSystem system = fi.MEPModel.GetElectricalSystems().FirstOrDefault();

            if (system == null)
            {
                TaskDialog.Show("Warning", "No system on the fixture.");
                return APIServiceResult.Failed;
            }

            Host.ProjectData.Path.CurrentPath = system.GetCircuitPath().ToList();
            Host.ProjectData.CurrentElectricalSystem = system;

            return APIServiceResult.Succeeded;
        }
    }
}
