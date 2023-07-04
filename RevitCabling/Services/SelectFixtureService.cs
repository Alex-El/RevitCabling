using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using RevitCabling.Helpers;
using System.Threading.Tasks;

namespace RevitCabling.Services
{
    internal class SelectFixtureService : ExternalEventService
    {
        public SelectFixtureService(string name) : base(name) { }

        protected override APIServiceResult Execute(UIApplication app)
        {
            Host.ProjectData.CurrentFixture = null;
            Host.ProjectData.CurrentPath.Clear();
            
            UIDocument uidoc = app.ActiveUIDocument;

            Reference reference = null;
            try
            {
                reference = uidoc.Selection.PickObject(ObjectType.Element, new ElectricalFixtureFilter(), Properties.Resources.SelectFixtureToolTip);
            }
            catch 
            {
                return APIServiceResult.Canceled;
            }

            if (reference != null)
            {
                Host.ProjectData.CurrentFixture = reference;
            }
            else
            {
                return APIServiceResult.Failed;
            }

            return APIServiceResult.Succeeded;
        }
    }
}
