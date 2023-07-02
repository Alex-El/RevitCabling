using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using RevitCabling.Helpers;

namespace RevitCabling.Services
{
    internal class SelectFixtureService : ExternalEventCommand
    {
        public SelectFixtureService(string name) : base(name) { }

        public override void Execute()
        {
            Host.ProjectData.CurrentFixture = null;
            Host.ProjectData.CurrentPath.Clear();
            
            UIDocument uidoc = UIApplication.ActiveUIDocument;

            Reference reference = null;
            try
            {
                reference = uidoc.Selection.PickObject(ObjectType.Element, new ElectricalFixtureFilter(), Properties.Resources.SelectFixtureToolTip);
            }
            catch { }

            if (reference != null)
            {
                Host.ProjectData.CurrentFixture = reference;
            }
        }
    }
}
