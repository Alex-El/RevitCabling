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

        public override async void Execute(object parameter)
        {
            _mainVM.OnBusy();
            var getAllTraySrvc = Host.GetService<GetAllCableTraysService>();
            var drawNotesSrvc = Host.GetService<DrawTextNotesService>();

            var read_result = await getAllTraySrvc.Run();

            if (read_result == APIServiceResult.Succeeded)
            {
                _ = await drawNotesSrvc.Run();
                _mainVM.OnTrayLoadingExecute();
            }
            else
            {
                _mainVM.OnOkExecute();
            }
        }
    }
}
