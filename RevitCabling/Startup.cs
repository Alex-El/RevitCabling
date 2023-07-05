using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitCabling.PluginBL;

namespace RevitCabling
{
    [Transaction(TransactionMode.Manual)]
    public class Startup : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Host.DockablePanel.Show();

            return SetSharedParamRevitCommand.Execute(commandData.Application);
        }
    }
}
