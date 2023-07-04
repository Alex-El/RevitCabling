using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Electrical;
using Autodesk.Revit.UI;
using System.Linq;
using System.Threading.Tasks;

namespace RevitCabling.Services
{
    internal class GetAllCableTraysService : ExternalEventService
    {
        public GetAllCableTraysService(string serviceName) : base(serviceName) { }

        protected override APIServiceResult Execute(UIApplication app)
        {
            Autodesk.Revit.DB.Document doc = app.ActiveUIDocument.Document;

            Host.ProjectData.CableTrays = new FilteredElementCollector(doc)
                .OfClass(typeof(CableTray))
                .Cast<CableTray>()
                .ToList();

            return APIServiceResult.Succeeded;
        }
    }
}
