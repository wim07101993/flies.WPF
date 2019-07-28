using Flies.Shared.ViewModelInterfaces;

namespace Flies.Wpf.ViewModelInterfaces
{
    public interface IMainWindowViewModel
    {
        IParticipantListViewModel ParticipantListViewModel { get; }
    }
}
