using RevitCabling.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCabling.Commands
{
    internal class ClearTrayLoadingCommand : BaseCommand
    {
        MainViewModel _mainVM;

        public ClearTrayLoadingCommand(MainViewModel mainVM)
        {
            _mainVM = mainVM;
        }

        public override void Execute(object parameter)
        {
            _mainVM.OnClearExecute();
        }
    }
}
