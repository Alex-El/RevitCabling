using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RevitCabling
{
    internal static class RibbonController
    {
        public static void CreatePanel(UIControlledApplication application)
        {
            string iconFullName32 = Path.Combine(Host.Settings.IconPath, "CableTray32.png");
            string iconFullName16 = Path.Combine(Host.Settings.IconPath, "CableTray16.png");
            string dllFullName = Host.Settings.AppDllFullName;
            string entryPoint = Host.Settings.EntryPointUI;

            RibbonPanel addinPanel = application.CreateRibbonPanel("Cabling");
            PushButton startButton = addinPanel.AddItem(new PushButtonData("MainBtn", "Put cable into trays", dllFullName, entryPoint)) as PushButton;
            startButton.LargeImage = new BitmapImage(new Uri(iconFullName32, UriKind.RelativeOrAbsolute));
            startButton.Image = new BitmapImage(new Uri(iconFullName16, UriKind.RelativeOrAbsolute));
        }
    }
}
