using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace RevitCabling.StaticData
{
    internal class Settings
    {
        public string IconPath { get; private set; }
        public string AppDllFullName { get; private set; }
        public string EntryPointUI { get; private set; }
        public LanguageType LanguageType { get; private set; }
        

        public Settings(UIControlledApplication application)
        {
            LanguageType = application.ControlledApplication.Language;
            //if (LanguageType == LanguageType.Russian)
            //{
            //    var ci = new System.Globalization.CultureInfo("ru-RU");
            //    System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
            //    System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            //}
            //else
            //{
            //    var ci = new System.Globalization.CultureInfo("en-IE");
            //    System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
            //    System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            //}

            string appPath = Properties.Settings.Default.DebugAppPath;

#if DEBUG
            IconPath = Path.Combine(Properties.Settings.Default.DebugAppPath, Properties.Settings.Default.ImageFolderName);
            AppDllFullName = Path.Combine(Properties.Settings.Default.DebugAppPath, Properties.Settings.Default.AssembleName);
            EntryPointUI = Properties.Settings.Default.EntryPoint;
#else
            //IconPath = Path.Combine(Properties.Settings.Default.DebugAppPath, Properties.Settings.Default.ImageFolderName);
            //AppDllFullName = Path.Combine(Properties.Settings.Default.DebugAppPath, Properties.Settings.Default.AssembleName);
            //EntryPointUI = Properties.Settings.Default.EntryPoint;
#endif
#if v22
            AppDllFullName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Properties.Settings.Default.v22AppPath,
                Properties.Settings.Default.AssembleName);
            IconPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Properties.Settings.Default.v22AppPath,
                Properties.Settings.Default.ImageFolderName);
            EntryPointUI = Properties.Settings.Default.EntryPoint;
#endif
#if v23
            AppDllFullName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Properties.Settings.Default.v23AppPath,
                Properties.Settings.Default.AssembleName);
            IconPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Properties.Settings.Default.v23AppPath,
                Properties.Settings.Default.ImageFolderName);
            EntryPointUI = Properties.Settings.Default.EntryPoint;
#endif
#if v24
            AppDllFullName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Properties.Settings.Default.v24AppPath,
                Properties.Settings.Default.AssembleName);
            IconPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Properties.Settings.Default.v24AppPath,
                Properties.Settings.Default.ImageFolderName);
            EntryPointUI = Properties.Settings.Default.EntryPoint;
#endif

        }
    }
}
