using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;

namespace RevitCabling.Services
{
    internal class DeleteTextNotesService : ExternalEventService
    {
        public DeleteTextNotesService(string name) : base(name) { }

        protected override APIServiceResult Execute(UIApplication app)
        {
            var doc = app.ActiveUIDocument.Document;

            using (Transaction tr = new Transaction(doc, "Delete text notes"))
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

            return APIServiceResult.Succeeded;
        }
    }
}
