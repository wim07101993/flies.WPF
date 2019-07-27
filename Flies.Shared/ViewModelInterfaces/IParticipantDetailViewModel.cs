using Flies.Shared.Participants;
using System.Windows.Input;

namespace Flies.Shared.ViewModelInterfaces
{
    public interface IParticipantDetailViewModel : IViewModelBase
    {
        Participant Item { get; set; }

        ICommand DeleteCommand { get; }
    }
}
