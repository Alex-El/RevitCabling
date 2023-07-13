using Autodesk.Revit.DB.Electrical;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCabling.Services
{
    internal class ApplyCircuitPathService : ExternalEventService
    {
        public ApplyCircuitPathService(string serviceName) : base(serviceName) { }

        protected override APIServiceResult Execute(UIApplication app)
        {
            var doc = app.ActiveUIDocument.Document;
            var fi = doc.GetElement(Host.ProjectData.CurrentFixture) as FamilyInstance;
            ElectricalSystem system = fi.MEPModel.GetElectricalSystems().FirstOrDefault();

            using (Transaction tr = new Transaction(doc, "Apply circuit path"))
            {
                try
                {
                    tr.Start();

                    system.SetCircuitPath(Host.ProjectData.Path.CurrentPath);

                    tr.Commit();
                }
                catch (Exception ex)
                {
                    tr.RollBack();
                    throw ex;
                }

            }
            return APIServiceResult.Succeeded;
        }
    }
}
