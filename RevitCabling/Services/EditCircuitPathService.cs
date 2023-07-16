using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Electrical;
using Autodesk.Revit.UI;
using RevitCabling.PluginBL;
using System;
using System.Collections.Generic;

namespace RevitCabling.Services
{
    internal class EditCircuitPathService : ExternalEventService
    {
        public EditCircuitPathService(string serviceName) : base(serviceName) { }

        protected override APIServiceResult Execute(UIApplication app)
        {
            

            try
            {
                var doc = app.ActiveUIDocument.Document;
                var cableTray = doc.GetElement(Host.ProjectData.CurrentCableTray) as CableTray;
                var location = cableTray.Location as LocationCurve;
                XYZ startP = location.Curve.GetEndPoint(0);
                XYZ endP = location.Curve.GetEndPoint(1);

                Host.ProjectData.Path.ApplyCableTray(startP, endP);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return APIServiceResult.Succeeded;
        }
    }
}
