using RevitCabling.Services;
using RevitCabling.ViewModels;

namespace RevitCabling.Commands
{
    internal class ClearCablingCommand : BaseCommand
    {
        MainViewModel _mainVM;

        public ClearCablingCommand(MainViewModel mainVM) : 
            base((obj) => mainVM.CurrentUIMode == UIMode.Cabling || mainVM.CurrentUIMode == UIMode.TrayLoading)
        {
            _mainVM = mainVM;
        }

        public override void Execute(object parameter)
        {
            _mainVM.OnBusy();
            //BL
            Host.ExecuteService<DeleteCircuitPathService>();
            //---
            _mainVM.OnClearExecute();
        }
    }
}
