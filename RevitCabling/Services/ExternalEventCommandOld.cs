using Autodesk.Revit.UI;
using System;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RevitCabling.Services
{
    internal abstract class ExternalEventCommandOld : IExternalEventHandler
    {
        internal UIApplication UIApplication { get; private set; }
        internal Task<APIServiceResult> ServiceResult { get; private set; }
        string _name;
        TaskCompletionSource<APIServiceResult> _taskCompletionSource;

        public ExternalEventCommandOld(string name)
        {
            _name = name;
            _taskCompletionSource = new TaskCompletionSource<APIServiceResult>();
            ServiceResult = _taskCompletionSource.Task;
        }

        public void Execute(UIApplication app)
        {
            UIApplication = app;
            

            try
            {
                Execute(_taskCompletionSource);
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

        public abstract void Execute(TaskCompletionSource<APIServiceResult> taskCompletion);
    }

    //public enum APIServiceResult
    //{
    //    Succeeded,
    //    Failed,
    //    Canceled
    //}
}
