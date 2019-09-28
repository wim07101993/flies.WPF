using Flies.Shared.Participants;
using Flies.Shared.ViewModelInterfaces;
using Flies.Shared.ViewModels;
using Flies.Wpf.ViewModelInterfaces;
using Flies.Wpf.ViewModels;
using Flies.Wpf.Views;
using Prism.Events;
using System.Windows;
using Unity;
using Settings = Flies.Wpf.Properties.Settings;

namespace Flies.Wpf
{
    public partial class App
    {
        static App()
        {
            var participantServiceSettings = Settings.Default.ParticipantServiceSettings;
            if (participantServiceSettings == null)
            {
                participantServiceSettings = new ParticipantServiceSettings
                {
                    IpAddress = "localhost",
                    PortNumber = 5000
                };
                Settings.Default.ParticipantServiceSettings = participantServiceSettings;
            }
        }

        public IUnityContainer UnityContainer { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            InitUnityContainer();

            MainWindow = UnityContainer.Resolve<MainWindow>();
            MainWindow.Show();
        }

        private void InitUnityContainer()
        {
            UnityContainer = new UnityContainer();
            UnityContainer
                .RegisterSingleton<IEventAggregator, EventAggregator>()
                .RegisterInstance(Settings.Default.ParticipantServiceSettings)
                .RegisterSingleton<IParticipantService, ParticipantService>()
                .RegisterType<IParticipantListViewModel, ParticipantListViewModel>()
                .RegisterType<IParticipantDetailViewModel, ParticipantDetailViewModel>()
                .RegisterType<ISettingsViewModel, SettingsViewModel>()
                .RegisterType<IMainWindowViewModel, MainWindowViewModel>();
        }
    }
}
