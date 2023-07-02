using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCabling.Services
{
    internal class DeleteTextNotesService : ExternalEventCommand
    {
        public DeleteTextNotesService(string name) : base(name) { }

        public override void Execute()
        {
            var doc = UIApplication.ActiveUIDocument.Document;

            using (Transaction tr = new Transaction(doc, "Create text notes"))
            {
                try
                {
                    tr.Start();

                    while (Host.ProjectData.TextNoteIds.Count > 0)
                    {
                        ElementId id = Host.ProjectData.TextNoteIds[0];
                        doc.Delete(id);
                        Host.ProjectData.TextNoteIds.RemoveAt(0);
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
