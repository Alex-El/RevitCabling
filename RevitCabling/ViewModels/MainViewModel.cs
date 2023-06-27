using RevitCabling.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RevitCabling.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public BaseCommand TrayLoadingCommand { get => _trayLoadingCommand; }
        public BaseCommand CablingCommand { get => _cablingCommand; }
        public BaseCommand ClearCommand { get => _clearCommand; }
        public BaseCommand OkCommand { get => _okCommand; }


        public MainViewModel()
        {
            _trayLoadingCommand = new TrayLoadingCommand(this);
            _cablingCommand = new CablingCommand(this);
            _clearCommand = new ClearCablingCommand(this);
            _okCommand = new OkCablingCommand(this);
        }

        BaseCommand _trayLoadingCommand;
        BaseCommand _cablingCommand;
        BaseCommand _clearCommand;
        BaseCommand _okCommand;

        public void OnCablingExecute()
        {

        }

        public void OnTrayLoadingExecute()
        {

        }

        public void OnClearExecute()
        {

        }

        public void OnOkExecute()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
