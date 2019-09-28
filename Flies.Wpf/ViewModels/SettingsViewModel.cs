using System.Windows.Input;
using Flies.Shared.Participants;
using Flies.Shared.ViewModels;
using Flies.Wpf.Properties;
using Flies.Wpf.ViewModelInterfaces;
using Prism.Commands;
using Prism.Events;

namespace Flies.Wpf.ViewModels
{
    public class SettingsViewModel : AViewModelBase, ISettingsViewModel
    {
        public SettingsViewModel(IEventAggregator eventAggregator) 
            : base(eventAggregator)
        {
            if (Settings.Default.ParticipantServiceSettings == null)
            {
                Settings.Default.ParticipantServiceSettings = new ParticipantServiceSettings
                {
                    IpAddress = "10.101.90.59",
                    PortNumber = 5000,
                };
                Settings.Default.Save();
            }

            SaveCommand = new DelegateCommand(Save);
            ResetCommand = new DelegateCommand(Reset);
        }


        public ParticipantServiceSettings ParticipantServiceSettings => Settings.Default.ParticipantServiceSettings;

        public ICommand SaveCommand { get; }
        public ICommand ResetCommand { get; }


        public void Save() => Settings.Default.Save();
        public void Reset()
        {
            Settings.Default.Reload();
            RaisePropertyChanged(nameof(ParticipantServiceSettings));
        }
    }
}
