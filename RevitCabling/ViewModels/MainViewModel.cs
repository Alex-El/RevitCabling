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
        public UIMode CurrentUIMode;


        public MainViewModel()
        {
            _trayLoadingCommand = new TrayLoadingCommand(this);
            _cablingCommand = new CablingCommand(this);
            _clearCommand = new ClearCablingCommand(this);
            _okCommand = new OkCablingCommand(this);

            CurrentUIMode = UIMode.Default;
        }

        BaseCommand _trayLoadingCommand;
        BaseCommand _cablingCommand;
        BaseCommand _clearCommand;
        BaseCommand _okCommand;

        public void OnCablingExecute()
        {
            CurrentUIMode = UIMode.Cabling;
            _clearCommand = new ClearCablingCommand(this);
            _okCommand = new OkCablingCommand(this);
            OnPropertyChanged("ClearCommand");
            OnPropertyChanged("OkCommand");
            GetFocus();
        }

        public void OnTrayLoadingExecute()
        {
            CurrentUIMode = UIMode.TrayLoading;
            _clearCommand = new ClearTrayLoadingCommand(this);
            _okCommand = new OkTrayLoadingCommand(this);
            OnPropertyChanged("ClearCommand");
            OnPropertyChanged("OkCommand");
            GetFocus();
        }

        public void OnClearExecute()
        {
            CurrentUIMode = UIMode.Default;
            GetFocus();
        }

        public void OnOkExecute()
        {
            CurrentUIMode = UIMode.Default;
            GetFocus();
        }

        public void OnBusy()
        {
            CurrentUIMode = UIMode.NonActive;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public event EventHandler GetFocusEvent;
        void GetFocus()
        {
            GetFocusEvent?.Invoke(this, EventArgs.Empty);
        }
    }

    enum UIMode
    {
        Default,
        Cabling,
        TrayLoading,
        NonActive
    }
}
