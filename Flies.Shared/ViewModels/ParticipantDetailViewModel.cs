using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Flies.Shared.Events;
using Flies.Shared.Participants;
using Flies.Shared.ViewModelInterfaces;
using Prism.Commands;
using Prism.Events;

namespace Flies.Shared.ViewModels
{
    public class ParticipantDetailViewModel : AViewModelBase, IParticipantDetailViewModel
    {
        #region FIELDS

        private readonly IParticipantService _participantService;

        private readonly object _lock = new object();

        private Participant _item;

        private bool _changedName;
        private bool _changedScore;
        private bool _isApplyingChanges;

        #endregion FIELDS


        #region CONSTRUCTOR

        public ParticipantDetailViewModel(IEventAggregator eventAggregator, IParticipantService participantService, Participant participant)
            : this(eventAggregator, participantService)
        {
            Item = participant;
        }

        public ParticipantDetailViewModel(IEventAggregator eventAggregator, IParticipantService participantService)
            : base(eventAggregator)
        {
            _participantService = participantService;

            DeleteCommand = new DelegateCommand(() => _ = DeleteAsync());
            SaveCommand = new DelegateCommand(() => _ = SaveAsync());
            IncreaseScoreCommand = new DelegateCommand(() => _ = IncreaseScoreAsync());
            DecreaseScoreCommand = new DelegateCommand(() => _ = DecreaseScoreAsync());
            CancelCommand = new DelegateCommand(() => _ = CancelAsync());
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public Participant Item
        {
            get => _item;
            set
            {
                if (!SetProperty(ref _item, value))
                    return;

                RaisePropertyChanged(nameof(Id));
                RaisePropertyChanged(nameof(Name));
                RaisePropertyChanged(nameof(Score));
            }
        }

        public uint Id => Item.Id;

        public string Name
        {
            get => Item.Name;
            set
            {
                if (Equals(Item.Name, value))
                    return;

                Item.Name = value;
                _changedName = true;
                RaisePropertyChanged();
            }
        }

        public ushort Score
        {
            get => Item.Score;
            set
            {
                if (Equals(Item.Score, value))
                    return;

                Item.Score = value;
                _changedScore = true;
                RaisePropertyChanged();
            }
        }

        public bool IsApplyingChanges
        {
            get => _isApplyingChanges;
            set => SetProperty(ref _isApplyingChanges, value);
        }

        public ICommand DeleteCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand IncreaseScoreCommand { get; }
        public ICommand DecreaseScoreCommand { get; }
        public ICommand CancelCommand { get; }

        #endregion PROPERTIES


        #region METHODS

        public async Task DeleteAsync()
        {
            if (IsApplyingChanges)
                return;

            IsApplyingChanges = true;

            await TryDeleteParticipantAsync(Item.Id);

            IsApplyingChanges = false;
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

        public async Task SaveAsync()
        {
            if (IsApplyingChanges)
                return;

            IsApplyingChanges = true;

            Participant participant = Item;

            if (_changedName)
            {
                try
                {
                    participant = await _participantService.UpdateNameAsync(Item.Id, Name);

                }
                catch (Exception e)
                {
                    EventAggregator.GetEvent<ExceptionEvent>().Publish(e);
                }
                _changedName = false;
            }

            if (_changedScore)
            {
                try
                {
                    participant = await _participantService.UpdateScoreAsync(Item.Id, Score);
                }
                catch (Exception e)
                {
                    EventAggregator.GetEvent<ExceptionEvent>().Publish(e);
                }
                _changedScore = false;
            }

            Item = participant;

            IsApplyingChanges = false;
        }

        public async Task IncreaseScoreAsync()
        {
            if (IsApplyingChanges)
                return;

            IsApplyingChanges = true;

            Item = await TryIncreaseScoreAsync(Item.Id);

            IsApplyingChanges = false;
        }

        private async Task<Participant> TryIncreaseScoreAsync(uint id)
        {
            return await _participantService.IncreaseScoreAsync(id, 1);
        }

        public async Task DecreaseScoreAsync()
        {
            if (IsApplyingChanges)
                return;

            IsApplyingChanges = true;

            Item = await TryDecreaseScoreAsync(Item.Id);

            IsApplyingChanges = false;
        }

        private async Task<Participant> TryDecreaseScoreAsync(uint id)
        {
            return await _participantService.DecreaseScoreAsync(id, 1);
        }

        public async Task CancelAsync()
        {
            Item = await _participantService.GetParticipantAsync(Item.Id);
        }

        #endregion METHODS
    }
}
