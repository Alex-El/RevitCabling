using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using RevitCabling.Controllers;
using RevitCabling.Services;
using System;

namespace RevitCabling
{
    [Transaction(TransactionMode.Manual)]
    public class Application : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            Host.Initialize(application);

            // Services
            Host.AddServiceEvent<GetAllCableTraysService>(new GetAllCableTraysService("GetAllCableTrays"));
            Host.AddServiceEvent<DrawTextNotesService>(new DrawTextNotesService("DrawTextNotes"));
            Host.AddServiceEvent<DeleteTextNotesService>(new DeleteTextNotesService("DeleteTextNotes"));
            Host.AddServiceEvent<ReadCircuitPathService>(new ReadCircuitPathService("ReadCircuitPath"));
            Host.AddServiceEvent<DrawCircuitPathService>(new DrawCircuitPathService("DrawCircuitPath"));
            Host.AddServiceEvent<DeleteCircuitPathService>(new DeleteCircuitPathService("DeleteCircuitPath"));
            Host.AddServiceEvent<SelectFixtureService>(new SelectFixtureService("SelectFixture"));
            //--------

            application.ControlledApplication.DocumentOpened += OnDocumentOpened;

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            application.ControlledApplication.DocumentOpened -= OnDocumentOpened;

            return Result.Succeeded;
        }

        private void OnDocumentOpened(object sender, DocumentOpenedEventArgs e)
        {
            Host.DockablePanel.Hide();
        }
    }
}
