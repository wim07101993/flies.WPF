using Flies.Shared.Participants;
using Flies.Shared.ViewModelInterfaces;
using System.Windows.Input;

namespace Flies.Wpf.ViewModelInterfaces
{
    public interface ISettingsViewModel : IViewModelBase
    {
        ParticipantServiceSettings ParticipantServiceSettings { get; }
        ICommand SaveCommand { get; }
        ICommand ResetCommand { get; }
    }
}
