using Flies.Shared.Participants;
using Flies.Shared.ViewModelInterfaces;
using Flies.Shared.ViewModels;
using Flies.Wpf.ViewModelInterfaces;
using Prism.Events;

namespace Flies.Wpf.ViewModels
{
    public class MainWindowViewModel : AViewModelBase, IMainWindowViewModel
    {
        #region CONSTRUCTOR

        public MainWindowViewModel(IEventAggregator eventAggregator, 
            IParticipantListViewModel participantListViewModel, ISettingsViewModel settingsViewModel) 
            : base(eventAggregator)
        {
            ParticipantListViewModel = participantListViewModel;
            SettingsViewModel = settingsViewModel;
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public IParticipantListViewModel ParticipantListViewModel { get; }
        public ISettingsViewModel SettingsViewModel { get; }

        #endregion PROPERTIES
    }
}
