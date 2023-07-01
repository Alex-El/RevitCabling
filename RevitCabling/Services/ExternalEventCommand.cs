using Autodesk.Revit.UI;
using System;

namespace RevitCabling.Services
{
    internal abstract class ExternalEventCommand : IExternalEventHandler
    {
        internal UIApplication UIApplication { get; private set; }
        string _name;

        public ExternalEventCommand(string name)
        {
            _name = name;
        }

        public void Execute(UIApplication app)
        {
            UIApplication = app;

            try
            {
                Execute();
            }
            catch { }
        }

        public string GetName()
        {
            return _name;
        }

        public abstract void Execute();
    }
}
