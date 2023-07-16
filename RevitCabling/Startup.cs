using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitCabling.PluginBL;
using System;

namespace RevitCabling
{
    [Transaction(TransactionMode.Manual)]
    public class Startup : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Result result = Result.Succeeded;
            Host.DockablePanel.Show();

            try
            {
                result = SetSharedParamRevitCommand.Execute(commandData.Application);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }

            return result;
        }
    }
}
