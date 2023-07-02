using Autodesk.Revit.UI;
using System;
using System.Windows.Media;

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
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (String.IsNullOrEmpty(msg))
                {
                    msg = "ExternalEventCommand Exception";
                }
                msg += '\n' + Properties.Resources.ErrorMessage;

                TaskDialog.Show("Error", msg);
            }
        }

        public string GetName()
        {
            return _name;
        }

        public abstract void Execute();
    }
}
