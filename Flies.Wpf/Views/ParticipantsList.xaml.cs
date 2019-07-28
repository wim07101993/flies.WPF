using Flies.Shared.ViewModelInterfaces;
using System.Windows.Input;

namespace Flies.Wpf.Views
{
    public partial class ParticipantsList 
    {
        public ParticipantsList()
        {
            InitializeComponent();
        }


        public IParticipantListViewModel ViewModel
        {
            get => DataContext as IParticipantListViewModel;
            set => DataContext = value;
        }


        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Delete:
                    ViewModel.DeleteParticipantCommand?.Execute(DataGrid.SelectedItem);
                    break;
            }
        }
    }
}
