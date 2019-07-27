using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Flies.Shared.Participants;
using Flies.Shared.ViewModelInterfaces;
using Prism.Commands;
using Prism.Events;
using Unity;

namespace Flies.Shared.ViewModels
{
    public class ParticipantsListViewModel : AViewModelBase, IParticipantListViewModel
    {
        #region FIELDS

        private readonly IParticipantService _participantService;
        private readonly IUnityContainer _unityContainer;

        private Participant _selectedItem;

        #endregion FIELDS


        #region CONSTRUCTOR

        public ParticipantsListViewModel(IEventAggregator eventAggregator, IUnityContainer unityContainer, IParticipantService participantService)
            : base(eventAggregator)
        {
            _unityContainer = unityContainer;
            _participantService = participantService;

            AddParticipantCommand = new DelegateCommand(AddParticipant);
            DeleteParticipantCommand = new DelegateCommand<Participant>(DeleteParticipant)

            _ = InitAsync();
        }

        public async Task InitAsync()
        {
            var participants = await _participantService.GetParticipants();
            foreach (var participant in participants)
            {
                var detailVm = _unityContainer.Resolve<IParticipantDetailViewModel>();
                detailVm.Item = participant;
                ItemsSource.Add(detailVm);
            }
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public ObservableCollection<IParticipantDetailViewModel> ItemsSource { get; } = new ObservableCollection<IParticipantDetailViewModel>();

        public Participant SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        public ICommand AddParticipantCommand { get; }
        public ICommand DeleteParticipantCommand { get; }

        #endregion PROPERTIES


        #region METHODS

        private void AddParticipant()
        {
            // TODO
        }

        private void DeleteParticipant(Participant participant)
        {
            _participantService.DeleteScore(participant.Id);
        }

        #endregion METHDOS
    }
}
