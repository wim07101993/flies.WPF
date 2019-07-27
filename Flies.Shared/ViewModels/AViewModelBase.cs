using Flies.Shared.ViewModelInterfaces;
using Prism.Events;
using Prism.Mvvm;

namespace Flies.Shared.ViewModels
{
    public class AViewModelBase : BindableBase, IViewModelBase
    {
        public AViewModelBase(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
        }

        protected IEventAggregator EventAggregator { get; }
    }
}
