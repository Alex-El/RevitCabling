using RevitCabling.Services;
using RevitCabling.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCabling.Commands
{
    internal class OkCablingCommand : BaseCommand
    {
        MainViewModel _mainVM { get; }

        public OkCablingCommand(MainViewModel mainVM) :
             base((obj) => mainVM.CurrentUIMode == UIMode.Cabling && Host.ProjectData.Path.PathValid())
        {
            _mainVM = mainVM;
        }

        public override async void Execute()
        {
            _mainVM.OnBusy();

            _ = await Host.GetService<DeleteCircuitPathService>().Run();
            _ = await Host.GetService<ApplyCircuitPathService>().Run();
            _ = await Host.GetService<WriteCableToParamService>().Run();

            _mainVM.OnOkExecute();
        }
    }
}
