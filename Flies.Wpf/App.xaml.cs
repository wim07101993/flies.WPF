using Flies.Shared.Participants;
using Flies.Shared.ViewModelInterfaces;
using Flies.Shared.ViewModels;
using Flies.Wpf.Views;
using Prism.Events;
using System.Windows;
using Unity;

namespace Flies.Wpf
{
    public partial class App
    {
        public static IUnityContainer UnityContainer { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            InitUnityContainer();

            MainWindow = new MainWindow();
            MainWindow.Show();
        }

        private void InitUnityContainer()
        {
            UnityContainer = new UnityContainer();
            UnityContainer
                .RegisterSingleton<IEventAggregator, EventAggregator>()
                .RegisterSingleton<IParticipantService, ParticipantService>()
                .RegisterType<IParticipantListViewModel, ParticipantListViewModel>()
                .RegisterType<IParticipantDetailViewModel, ParticipantDetailViewModel>();
        }
    }
}
