using Flies.Shared.ViewModelInterfaces;

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
    }
}
