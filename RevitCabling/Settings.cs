using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCabling
{
    internal class Settings
    {
        public string IconPath { get; private set; }
        public string AppDllFullName { get; private set; }
        public string EntryPointUI { get; private set; }

        public Settings(UIControlledApplication application)
        {
            var lang = application.ControlledApplication.Language;

            string appPath = Properties.Settings.Default.DebugAppPath;

#if DEBUG
            IconPath = Path.Combine(Properties.Settings.Default.DebugAppPath, Properties.Settings.Default.ImageFolderName);
            AppDllFullName = Path.Combine(Properties.Settings.Default.DebugAppPath, Properties.Settings.Default.AssembleName);
            EntryPointUI = Properties.Settings.Default.EntryPoint;
#else

#endif
        }
    }
}
