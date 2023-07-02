using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCabling.Services
{
    internal class DeleteCircuitPathService : ExternalEventCommand
    {
        public DeleteCircuitPathService(string name) : base(name) { }

        public override void Execute()
        {
            var doc = UIApplication.ActiveUIDocument.Document;

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
        }
    }
}
