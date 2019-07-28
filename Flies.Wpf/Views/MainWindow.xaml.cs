using Flies.Wpf.ViewModelInterfaces;

namespace Flies.Wpf.Views
{
    public partial class MainWindow 
    {
        public MainWindow(IMainWindowViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
        }


        public IMainWindowViewModel ViewModel
        {
            get => DataContext as IMainWindowViewModel;
            set => DataContext = value;
        }
    }
}
