using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using RevitCabling.Controllers;
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

        static Dictionary<Type, ExternalEvent> _events = new Dictionary<Type, ExternalEvent>();
        static Queue<ExternalEvent> ExEH = new Queue<ExternalEvent>();

        public static void Initialize(UIControlledApplication application) 
        {  
            Settings = new Settings(application);
            RibbonPanel = new RibbonController(application);
            DockablePanel = new DockablePanController(application);
            ProjectData = new Project();

            application.Idling += EventRase;
        }

        private static void EventRase(object sender, IdlingEventArgs e)
        {
            if (ExEH.Count > 0)
            {
                ExternalEvent exeh = ExEH.Dequeue();
                exeh.Raise();
                return;
            }
        }

        public static void AddServiceEvent<T>(IExternalEventHandler eventHandler) where T : class
        {
            _events[typeof(T)] = ExternalEvent.Create(eventHandler);
        }

        public static void ExecuteService<T>() where T : class
        {
            ExternalEvent externalEvent = _events[typeof(T)];
            ExEH.Enqueue(externalEvent);
        }

        static ExternalEvent GetServiceEvent<T>() where T : class
        {
            return _events[typeof(T)];
        }
    }
}
