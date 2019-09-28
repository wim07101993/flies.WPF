using Flies.Shared.Events;
using Flies.Wpf.ViewModelInterfaces;
using MaterialDesignThemes.Wpf;
using Prism.Events;
using System;

namespace Flies.Wpf.Views
{
    public partial class MainWindow 
    {
        public MainWindow(IMainWindowViewModel viewModel, IEventAggregator eventAggregator)
        {
            eventAggregator?.GetEvent<ExceptionEvent>().Subscribe(ShowException);

            InitializeComponent();
            ViewModel = viewModel;
        }


        public IMainWindowViewModel ViewModel
        {
            get => DataContext as IMainWindowViewModel;
            set => DataContext = value;
        }

        public static void ShowException(Exception e) => DialogHost.Show(new ExceptionDialogContent(e));
    }
}
