using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitCabling
{
    [Transaction(TransactionMode.Manual)]
    public class Application : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            TaskDialog.Show("Test AddIn", "Halo 2022!");

            return Result.Succeeded;
        }
    }
}
