using Prism.Mvvm;

namespace Flies.Shared
{
    public class Participant : BindableBase
    {
        private uint _id;
        private string _name;
        private ushort _score;


        public uint Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public ushort Score
        {
            get => _score;
            set => SetProperty(ref _score, value);
        }
    }
}
