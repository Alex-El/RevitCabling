using RevitCabling.Services;
using RevitCabling.ViewModels;

namespace RevitCabling.Commands
{
    internal class OkTrayLoadingCommand : BaseCommand
    {
        MainViewModel _mainVM;

        public OkTrayLoadingCommand(MainViewModel mainVM) :
             base((obj) => mainVM.CurrentUIMode == UIMode.Cabling || mainVM.CurrentUIMode == UIMode.TrayLoading)
        {
            _mainVM = mainVM;
        }

        public override async void Execute(object parameter)
        {
            _mainVM.OnBusy();
            _ = await Host.GetService<DeleteTextNotesService>().Run();
            _mainVM.OnOkExecute();
        }
    }
}
