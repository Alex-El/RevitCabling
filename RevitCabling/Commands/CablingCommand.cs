using RevitCabling.Services;
using RevitCabling.ViewModels;

namespace RevitCabling.Commands
{
    internal class CablingCommand : BaseCommand
    {
        MainViewModel _mainVM { get; }

        public CablingCommand(MainViewModel mainVM) : 
            base((obj) => mainVM.CurrentUIMode == UIMode.Default )
        {
            _mainVM = mainVM;
        }

        public override void Execute(object parameter)
        {
            _mainVM.OnBusy();
            // BL
            Host.ExecuteService<SelectFixtureService>();
            Host.ExecuteService<ReadCircuitPathService>();
            Host.ExecuteService<DrawCircuitPathService>();
            //---
            _mainVM.OnCablingExecute();
        }
    }
}
