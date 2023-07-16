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

        public override async void Execute()
        {
            _mainVM.OnBusy();
            _ = await Host.GetService<DeleteCircuitPathService>().Run();
            _mainVM.OnClearExecute();
        }
    }
}
