﻿using RevitCabling.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _mainVM.OnClearExecute();
        }
    }
}
