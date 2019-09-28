using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;

namespace Flies.Wpf.Views
{
    public partial class Settings
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DrawerHost.CloseDrawerCommand?.Execute(Dock.Right, (Button)sender);
        }
    }
}
