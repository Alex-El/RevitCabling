using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;

namespace RevitCabling.Services
{
    internal class DeleteCircuitPathService : ExternalEventService
    {
        public DeleteCircuitPathService(string name) : base(name) { }

        protected override APIServiceResult Execute(UIApplication app)
        {
            var doc = app.ActiveUIDocument.Document;

            using (Transaction tr = new Transaction(doc, "Delete circuit path"))
            {
                try
                {
                    tr.Start();

                    while (Host.ProjectData.PathElementIds.Count > 0)
                    {
                        ElementId id = Host.ProjectData.PathElementIds[0];
                        doc.Delete(id);
                        Host.ProjectData.PathElementIds.RemoveAt(0);
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
