using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using RevitCabling.Controllers;
using RevitCabling.Services;
using RevitCabling.StaticData;
using System;
using System.Collections.Generic;

namespace RevitCabling
{
    internal static class Host
    {
        public static Settings Settings { get; private set; } = null;
        public static RibbonController RibbonPanel { get; private set; }
        public static DockablePanController DockablePanel { get; private set; }
        public static Project ProjectData { get; private set; }

        static Dictionary<Type, ExternalEventService> _events = new Dictionary<Type, ExternalEventService>();

        public static void Initialize(UIControlledApplication application) 
        {  
            Settings = new Settings(application);
            RibbonPanel = new RibbonController(application);
            DockablePanel = new DockablePanController(application);
            ProjectData = new Project();
        }

        public static void AddServiceEvent<T>(ExternalEventService eventHandler) where T : class
        {
            _events[typeof(T)] = eventHandler;
        }

        public static ExternalEventService GetService<T>() where T : class
        {
            return _events[typeof(T)];
        }
    }
}
