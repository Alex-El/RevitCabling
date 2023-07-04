using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Electrical;
using Autodesk.Revit.UI;
using System;
using System.Threading.Tasks;

namespace RevitCabling.Services
{
    internal class DrawTextNotesService : ExternalEventService
    {
        public DrawTextNotesService(string name) : base(name) { }

        protected override APIServiceResult Execute(UIApplication app)
        {
            var doc = app.ActiveUIDocument.Document;

            using (Transaction tr = new Transaction(doc, "Create text notes"))
            {
                try
                {
                    tr.Start();

                    foreach (var ct in Host.ProjectData.CableTrays)
                    {
                        string sp = ct.LookupParameter(Properties.Resources.CableParameterName).AsValueString();

                        if (string.IsNullOrEmpty(sp))
                        {
                            sp = "Empty";
                        }

                        TextNote textNote = CreateTextNote(doc, ct, sp);
                        Host.ProjectData.TextNoteIds.Add(textNote.Id);
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

        private TextNote CreateTextNote(Document doc, CableTray cableTray, string sharedParamValue)
        {
            var location = cableTray.Location as LocationCurve;
            XYZ startPt = location.Curve.GetEndPoint(0);
            XYZ endPt = location.Curve.GetEndPoint(1);
            XYZ middlePt = (startPt + endPt) / 2;

            ElementId defaultTextTypeId = doc.GetDefaultElementTypeId(ElementTypeGroup.TextNoteType);

            double noteWidth = .2;

            // make sure note width works for the text type
            double minWidth = TextNote.GetMinimumAllowedWidth(doc, defaultTextTypeId);
            double maxWidth = TextNote.GetMaximumAllowedWidth(doc, defaultTextTypeId);
            if (noteWidth < minWidth)
            {
                noteWidth = minWidth;
            }
            else if (noteWidth > maxWidth)
            {
                noteWidth = maxWidth;
            }

            TextNoteOptions opts = new TextNoteOptions(defaultTextTypeId);
            opts.HorizontalAlignment = HorizontalTextAlignment.Left;
            //opts.Rotation = Math.PI / 4;

            TextNote textNote = TextNote.Create(doc, doc.ActiveView.Id, middlePt, noteWidth, sharedParamValue, opts);

            return textNote;
        }
    }
}
