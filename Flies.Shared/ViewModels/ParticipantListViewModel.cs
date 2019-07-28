using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Flies.Shared.Participants;
using Flies.Shared.ViewModelInterfaces;
using Prism.Commands;
using Prism.Events;
using Unity;
using Unity.Resolution;

namespace Flies.Shared.ViewModels
{
    public class ParticipantListViewModel : AViewModelBase, IParticipantListViewModel
    {
        #region FIELDS

        private readonly IParticipantService _participantService;
        private readonly IUnityContainer _unityContainer;

        private IParticipantDetailViewModel _selectedItem;

        #endregion FIELDS


        #region CONSTRUCTOR

        public ParticipantListViewModel(IEventAggregator eventAggregator, IUnityContainer unityContainer, IParticipantService participantService)
            : base(eventAggregator)
        {
            _unityContainer = unityContainer;
            _participantService = participantService;

            AddParticipantCommand = new DelegateCommand<ParticipantDetailViewModel>(x => _ = AddParticipantAsync(x));
            DeleteParticipantCommand = new DelegateCommand<ParticipantDetailViewModel>(x => _ = DeleteParticipantAsync(x));
            RefreshCommand = new DelegateCommand(() => _ = RefreshParticipantAsync());

            _ = InitAsync();
        }

        public async Task InitAsync()
        {
            try
            {
                await RefreshParticipantAsync();
            }
            catch (Exception e)
            {
                // TODO
                throw;
            }
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public ObservableCollection<IParticipantDetailViewModel> ItemsSource { get; } = new ObservableCollection<IParticipantDetailViewModel>();

        public IParticipantDetailViewModel SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        public ICommand AddParticipantCommand { get; }
        public ICommand DeleteParticipantCommand { get; }
        public ICommand RefreshCommand { get; }

        #endregion PROPERTIES


        #region METHODS

        private IParticipantDetailViewModel CreateParticipantDetailViewModel(Participant participant)
        {
            return _unityContainer.Resolve<IParticipantDetailViewModel>(new ParameterOverride("participant", participant));
        }

        private async Task AddParticipantAsync(ParticipantDetailViewModel viewModel)
        {
            var participant = await _participantService.CreateAsync(viewModel.Item);
            ItemsSource.Add(CreateParticipantDetailViewModel(participant));
        }

        private async Task DeleteParticipantAsync(ParticipantDetailViewModel viewModel)
        {
            await _participantService.DeleteParticipantAsync(viewModel.Id);
        }

        private async Task RefreshParticipantAsync()
        {
            var participants = await _participantService.GetParticipantsAsync();

            ItemsSource.Clear();
            foreach (var participant in participants)
                ItemsSource.Add(CreateParticipantDetailViewModel(participant));
        }

        #endregion METHDOS
    }
}
