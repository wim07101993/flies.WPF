using Prism.Mvvm;

namespace Flies.Shared.Participants
{
    public class ParticipantServiceSettings : BindableBase
    {
        private string _ipAddress;
        private uint _portNumber;

        public string IpAddress
        {
            get => _ipAddress;
            set => SetProperty(ref _ipAddress, value);
        }

        public uint PortNumber
        {
            get => _portNumber;
            set => SetProperty(ref _portNumber, value);
        }
    }
}
