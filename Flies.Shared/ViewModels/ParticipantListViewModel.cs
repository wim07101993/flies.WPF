using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Flies.Shared.Events;
using Flies.Shared.Extensions;
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

        private bool _wasntMe;

        #endregion FIELDS


        #region CONSTRUCTOR

        public ParticipantListViewModel(IEventAggregator eventAggregator, IUnityContainer unityContainer, IParticipantService participantService)
            : base(eventAggregator)
        {
            _unityContainer = unityContainer;
            _participantService = participantService;

            ItemsSource.CollectionChanged += OnCollectionChanged;

            AddParticipantCommand = new DelegateCommand<Participant>(x => _ = AddParticipantAsync(x));
            DeleteParticipantCommand = new DelegateCommand<Participant>(x => _ = DeleteParticipantAsync(x));
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

        public async Task AddParticipantAsync(Participant participant)
        {
            _wasntMe = false;

            try
            {
                participant = await TryAddParticipantAsync(participant);

                if (participant == null)
                    return;

                ItemsSource.Add(CreateParticipantDetailViewModel(participant));
            }
            catch (Exception e)
            {
                EventAggregator.GetEvent<ExceptionEvent>().Publish(e);
            }

            _wasntMe = true;
        }

        private async Task<Participant> TryAddParticipantAsync(Participant participant)
        {
            try
            {
                return  await _participantService.CreateAsync(participant);
            }
            catch (Exception e)
            {
                EventAggregator.GetEvent<ExceptionEvent>().Publish(e);
                return default;
            }
        }

        public async Task DeleteParticipantAsync(Participant participant)
        {
            _wasntMe = false;

            try
            {
                if (await TryDeleteParticipantAsync(participant.Id))
                    ItemsSource.RemoveFirst(x => x.Id == participant.Id);

            }
            catch (Exception e)
            {
                EventAggregator.GetEvent<ExceptionEvent>().Publish(e);
            }

            _wasntMe = true;
        }

        private async Task<bool> TryDeleteParticipantAsync(uint id)
        {
            try
            {
                await _participantService.DeleteParticipantAsync(id);
                return true;
            }
            catch (Exception e)
            {
                EventAggregator.GetEvent<ExceptionEvent>().Publish(e);
                return false;
            }
        }

        public async Task RefreshParticipantAsync()
        {
            var participants = await GetParticipantsAsync();

            if (participants == null)
                return;

            _wasntMe = false;

            try
            {
                ItemsSource.Clear();
                foreach (var participant in participants)
                    ItemsSource.Add(CreateParticipantDetailViewModel(participant));
            }
            catch (Exception e)
            {
                EventAggregator.GetEvent<ExceptionEvent>().Publish(e);
            }

            _wasntMe = true;
        }

        private async Task<IList<Participant>> GetParticipantsAsync()
        {
            try
            {
                return await _participantService.GetParticipantsAsync();
            }
            catch (Exception e)
            {
                EventAggregator.GetEvent<ExceptionEvent>().Publish(e);
                return default;
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!_wasntMe)
                return;

            var newItems = e.NewItems?.Cast<IParticipantDetailViewModel>();
            var oldItems = e.OldItems?.Cast<IParticipantDetailViewModel>();

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var item in newItems)
                        _ = _participantService.CreateAsync(item.Item);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var item in oldItems)
                        _ = _participantService.DeleteParticipantAsync(item.Id);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    foreach (var item in oldItems)
                        _ = _participantService.DeleteParticipantAsync(item.Id);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    foreach (var item in ItemsSource)
                        _ = _participantService.DeleteParticipantAsync(item.Id);
                    break;
            }
        }

        #endregion METHDOS
    }
}
