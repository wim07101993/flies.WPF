using Flies.Shared.Participants;
using System.Windows.Input;

namespace Flies.Shared.ViewModelInterfaces
{
    public interface IParticipantDetailViewModel : IViewModelBase
    {
        string Name { get; set; }
        ushort Score { get; set; }

        ICommand DeleteCommand { get; }
        ICommand SaveCommand { get; }
        ICommand IncreaseScoreCommand { get; }
        ICommand DecreaseScoreCommand { get; }
    }
}
