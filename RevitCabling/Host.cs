using Autodesk.Revit.UI;
using RevitCabling.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace RevitCabling
{
    internal static class Host
    {
        public static Settings Settings { get; private set; } = null;
        public static RibbonController RibbonPanel { get; private set; }
        public static DockablePanController DockablePanel { get; private set; }

        public static void Initialize(UIControlledApplication application) 
        {  
            Settings = new Settings(application);
            RibbonPanel = new RibbonController(application);
            DockablePanel = new DockablePanController(application);
        }
    }
}
