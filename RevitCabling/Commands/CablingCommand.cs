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

        public override async void Execute(object parameter)
        {
            _mainVM.OnBusy();

            var selectFixtureSrvc = Host.GetService<SelectFixtureService>();
            var readCircPathSrvc = Host.GetService<ReadCircuitPathService>();
            var drawCircPathSrvc = Host.GetService<DrawCircuitPathService>();

            var select_result = await selectFixtureSrvc.Run();

            if (select_result == APIServiceResult.Succeeded)
            {
                var read_result = await readCircPathSrvc.Run();

                if (read_result == APIServiceResult.Succeeded)
                {
                    _ = await drawCircPathSrvc.Run();
                    _mainVM.OnCablingExecute();
                    return;
                }
            }
            _mainVM.OnOkExecute();
        }
    }
}
