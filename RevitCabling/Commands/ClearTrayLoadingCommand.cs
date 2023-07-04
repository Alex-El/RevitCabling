using RevitCabling.Services;
using RevitCabling.ViewModels;

namespace RevitCabling.Commands
{
    internal class ClearTrayLoadingCommand : BaseCommand
    {
        MainViewModel _mainVM;

        public ClearTrayLoadingCommand(MainViewModel mainVM) :
             base((obj) => mainVM.CurrentUIMode == UIMode.Cabling || mainVM.CurrentUIMode == UIMode.TrayLoading)
        {
            _mainVM = mainVM;
        }

        public override async void Execute(object parameter)
        {
            _mainVM.OnBusy();
            _ = await Host.GetService<DeleteTextNotesService>().Run();
            _mainVM.OnClearExecute();
        }
    }
}
