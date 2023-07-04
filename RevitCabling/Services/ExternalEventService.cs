using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCabling.Services
{
    internal abstract class ExternalEventService
    {
        private ServiceHandler _handler;
        private TaskCompletionSource<APIServiceResult> _tcs;
        private ExternalEvent _externalEvent;

        public ExternalEventService(string serviceName)
        {
            _handler = new ServiceHandler(serviceName);
            _handler.EventCompleted += OnEventCompleted;
            _handler.Func = (app) => Execute(app);
            _externalEvent = ExternalEvent.Create(_handler);
        }

        public Task<APIServiceResult> Run()
        {
            _tcs = new TaskCompletionSource<APIServiceResult>();
            _externalEvent.Raise();

            return _tcs.Task;
        }

        private void OnEventCompleted(object sender, APIServiceResult result)
        {
            if (_handler.Exception == null)
            {
                _tcs.TrySetResult(result);
            }
            else
            {
                _tcs.TrySetException(_handler.Exception);
            }
        }

        protected abstract APIServiceResult Execute(UIApplication uIApplication);

        /// <summary>
        /// 
        /// </summary>
        private class ServiceHandler : IExternalEventHandler
        {
            private Func<UIApplication, APIServiceResult> _func;
            private string _serviceName;

            public event EventHandler<APIServiceResult> EventCompleted;
            public Exception Exception { get; private set; }
            public Func<UIApplication, APIServiceResult> Func
            {
                get => _func;
                set => _func = value ??
                    throw new ArgumentNullException();
            }

            public ServiceHandler(string serviceName)
            {
                _serviceName = serviceName;
            }

            public void Execute(UIApplication app)
            {
                APIServiceResult result = APIServiceResult.NotExecuted;

                Exception = null;

                try
                {
                    result = Func(app);
                }
                catch (Exception ex)
                {
                    Exception = ex;
                }

                EventCompleted?.Invoke(this, result);
            }

            public string GetName()
            {
                return _serviceName;
            }
        }
    }

    public enum APIServiceResult
    {
        Succeeded,
        Failed,
        Canceled,
        NotExecuted
    }
}
