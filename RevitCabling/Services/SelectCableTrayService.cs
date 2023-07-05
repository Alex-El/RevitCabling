using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using RevitCabling.Helpers;

namespace RevitCabling.Services
{
    internal class SelectCableTrayService : ExternalEventService
    {
        public SelectCableTrayService(string serviceName) : base(serviceName) { }

        protected override APIServiceResult Execute(UIApplication app)
        {
            Host.ProjectData.CurrentCableTray = null;

            UIDocument uidoc = app.ActiveUIDocument;

            Reference reference = null;
            try
            {
                reference = uidoc.Selection.PickObject(ObjectType.Element, new CableTrayFilter(), Properties.Resources.SelectCadleTrayToolTip);
            }
            catch
            {
                return APIServiceResult.Canceled;
            }

            if (reference != null)
            {
                Host.ProjectData.CurrentCableTray = reference;
            }
            else
            {
                return APIServiceResult.Failed;
            }

            return APIServiceResult.Succeeded;
        }
    }
}
