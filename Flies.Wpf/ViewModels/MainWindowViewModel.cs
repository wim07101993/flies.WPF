using Flies.Shared.ViewModelInterfaces;
using Flies.Shared.ViewModels;
using Flies.Wpf.ViewModelInterfaces;
using Prism.Events;

namespace Flies.Wpf.ViewModels
{
    public class MainWindowViewModel : AViewModelBase, IMainWindowViewModel
    {
        #region CONSTRUCTOR

        public MainWindowViewModel(IEventAggregator eventAggregator, IParticipantListViewModel participantListViewModel) 
            : base(eventAggregator)
        {
            ParticipantListViewModel = participantListViewModel;
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public IParticipantListViewModel ParticipantListViewModel { get; }

        #endregion PROPERTIES
    }
}
