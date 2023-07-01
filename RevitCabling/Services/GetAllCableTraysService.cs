using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Electrical;
using Autodesk.Revit.UI;
using System.Linq;

namespace RevitCabling.Services
{
    internal class GetAllCableTraysService : ExternalEventCommand
    {
        public GetAllCableTraysService(string name) : base(name) { }

        public override void Execute()
        {
            Autodesk.Revit.DB.Document doc = UIApplication.ActiveUIDocument.Document;

            Host.ProjectData.CableTrays = new FilteredElementCollector(doc)
                .OfClass(typeof(CableTray))
                .Cast<CableTray>()
                .ToList();

            //TaskDialog.Show("Test", $"Cable tray count = {Host.ProjectData.CableTrays.Count}");
        }
    }
}
