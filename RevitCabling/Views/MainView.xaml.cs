using Autodesk.Revit.UI;
using RevitCabling.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RevitCabling.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Page, IDockablePaneProvider, IDisposable
    {
        public MainView()
        {
            InitializeComponent();
            var vm = new MainViewModel();
            DataContext = vm;
            vm.GetFocusEvent += OnFocus;
        }

        private void OnFocus(object sender, EventArgs e)
        {
            this.Focus();
        }

        public void SetupDockablePane(DockablePaneProviderData data)
        {
            try
            {
                int screenW = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
                int screenH = (int)System.Windows.SystemParameters.PrimaryScreenHeight;

                int winH = 150;
                int winW = 250;
                int startX = (screenW / 2) - (winW / 2);
                int startY = (screenH / 2) - (winH / 2);

                data.FrameworkElement = this as FrameworkElement;
                var rectangle = new Autodesk.Revit.DB.Rectangle(startX, startY, (startX + winW), (startY + winH));
                data.InitialState.DockPosition = DockPosition.Floating;
                data.InitialState.SetFloatingRectangle(rectangle);
            }
            catch
            {
                var rectangle = new Autodesk.Revit.DB.Rectangle(200, 100, 325, 175);
                data.InitialState.DockPosition = DockPosition.Floating;
                data.InitialState.SetFloatingRectangle(rectangle);
            }
        }

        public void Dispose()
        {
            this.Dispose();
        }
    }
}
