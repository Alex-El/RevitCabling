using RevitCabling.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCabling.Commands
{
    internal class CablingCommand : BaseCommand
    {
        MainViewModel _mainVM { get; }

        public CablingCommand(MainViewModel mainVM)
        {
            _mainVM = mainVM;
        }

        public override void Execute(object parameter)
        {
            _mainVM.OnCablingExecute();
        }
    }
}
