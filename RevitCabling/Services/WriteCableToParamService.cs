using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Electrical;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCabling.Services
{
    internal class WriteCableToParamService : ExternalEventService
    {
        public WriteCableToParamService(string serviceName) : base(serviceName) { }

        protected override APIServiceResult Execute(UIApplication app)
        {
            var doc = app.ActiveUIDocument.Document;

            using (Transaction tr = new Transaction(doc, "Create text notes"))
            {
                try
                {
                    tr.Start();

                    foreach (var data in Host.ProjectData.CurrentCableTraysWithCables)
                    {
                        var cableTray = doc.GetElement(data.Item1) as CableTray;
                        if (cableTray == null) continue;

                        string oldValue = cableTray.LookupParameter(Properties.Resources.CableParameterName).AsValueString();
                        string newValue = oldValue += data.Item2 + ";";
                        cableTray.LookupParameter(Properties.Resources.CableParameterName).Set(newValue);
                    }

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
