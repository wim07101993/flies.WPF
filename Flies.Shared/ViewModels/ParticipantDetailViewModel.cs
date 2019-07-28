using System.Threading.Tasks;
using System.Windows.Input;
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

        #endregion PROPERTIES


        #region METHODS

        private async Task DeleteAsync()
        {
            if (IsApplyingChanges)
                return;

            IsApplyingChanges = true;

            await _participantService.DeleteParticipantAsync(Item.Id);

            IsApplyingChanges = false;
        }

        private async Task SaveAsync()
        {
            if (IsApplyingChanges)
                return;

            IsApplyingChanges = true;

            Participant participant = Item;

            if (_changedName)
            {
                participant = await _participantService.UpdateNameAsync(Item.Id, Name);
                _changedName = false;
            }

            if (_changedScore)
            {
                participant = await _participantService.UpdateScoreAsync(Item.Id, Score);
                _changedScore = false;
            }

            Item = participant;

            IsApplyingChanges = false;
        }

        private async Task IncreaseScoreAsync()
        {
            if (IsApplyingChanges)
                return;

            IsApplyingChanges = true;

            Item = await _participantService.IncreaseScoreAsync(Item.Id, 1);

            IsApplyingChanges = false;
        }

        private async Task DecreaseScoreAsync()
        {
            if (IsApplyingChanges)
                return;

            IsApplyingChanges = true;

            Item = await _participantService.DecreaseScoreAsync(Item.Id, 1);

            IsApplyingChanges = false;
        }

        #endregion METHODS
    }
}
