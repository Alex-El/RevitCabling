using Autodesk.Revit.DB.Electrical;
using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.UI;
using System;

namespace RevitCabling.Services
{
    internal class DrawCircuitPathService : ExternalEventCommand
    {
        public DrawCircuitPathService(string name) : base(name) { }

        public override void Execute()
        {
            if (Host.ProjectData.CurrentFixture ==  null)
            {
                return;
            }

            var doc = UIApplication.ActiveUIDocument.Document;

            List<ConduitType> conTypes = new FilteredElementCollector(doc)
                .OfClass(typeof(ConduitType))
                .Cast<ConduitType>()
                .ToList();

            ConduitType conduitType = conTypes.Where(ct => ct.Name.Contains(Properties.Resources.PartOfCondutTypeName)).FirstOrDefault();

            if (conduitType == null)
            {
                TaskDialog.Show("Error", $"No conduit type name with {Properties.Resources.PartOfCondutTypeName}");
                return;
            }

            ElementId levelId = doc.GetElement(Host.ProjectData.CurrentFixture.ElementId).LevelId;

            // graphics
            Color color = new Color(250, 0, 0);

            using (Transaction tr = new Transaction(doc, "Create conduit"))
            {
                try
                {
                    tr.Start();

                    var cp = Host.ProjectData.CurrentPath;

                    for (int i = 0; i < cp.Count - 1; i++)
                    {
                        ElementId id = Conduit.Create(doc, conduitType.Id, cp[i], cp[i + 1], levelId).Id;
                        OverrideGraphicSettings gr = doc.ActiveView.GetElementOverrides(id);
                        if (gr != null)
                        {
                            gr.SetProjectionLineColor(color);
                        }
                        doc.ActiveView.SetElementOverrides(id, gr);

                        Host.ProjectData.PathElementIds.Add(id);
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
