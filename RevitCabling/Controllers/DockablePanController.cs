using Autodesk.Revit.UI;
using RevitCabling.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCabling.Controllers
{
    internal class DockablePanController
    {
        public DockablePaneId PanelId { get; private set; }

        public DockablePanController(UIControlledApplication application)
        {
            var data = new DockablePaneProviderData();
            var dPage = new MainView();

            data.FrameworkElement = dPage as System.Windows.FrameworkElement;

            PanelId = new DockablePaneId(new Guid(Properties.Settings.Default.DockableGUID));
            string paneName = Properties.Settings.Default.DockableName;

            application.RegisterDockablePane(PanelId, paneName, dPage as IDockablePaneProvider);

        }
    }
}
