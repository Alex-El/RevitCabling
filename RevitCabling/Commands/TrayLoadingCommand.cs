using Autodesk.Revit.UI;
using RevitCabling.Services;
using RevitCabling.ViewModels;

namespace RevitCabling.Commands
{
    internal class TrayLoadingCommand : BaseCommand
    {
        MainViewModel _mainVM { get; }

        public TrayLoadingCommand(MainViewModel mainVM) :
            base((obj) => mainVM.CurrentUIMode == UIMode.Default)
        {
            _mainVM = mainVM;
        }

        public override void Execute(object parameter)
        {
            _mainVM.OnBusy();

            // BL
            Host.ExecuteService<GetAllCableTraysService>();
            //---

            _mainVM.OnTrayLoadingExecute();
        }
    }
}
