using Flies.Shared.Participants;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Flies.Shared.ViewModelInterfaces
{
    public interface IParticipantListViewModel : IViewModelBase
    {
        ObservableCollection<IParticipantDetailViewModel> ItemsSource { get; }
        IParticipantDetailViewModel SelectedItem { get; set; }

        ICommand AddParticipantCommand { get; }
        ICommand DeleteParticipantCommand { get; }
        ICommand RefreshCommand { get; }
    }
}
