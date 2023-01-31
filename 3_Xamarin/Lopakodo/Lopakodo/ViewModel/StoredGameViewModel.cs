using System;

namespace Lopakodo.ViewModel
{
    public class StoredGameViewModel : ViewModelBase
    {
        private String _name;
        private DateTime _modified;

        public String Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime Modified
        {
            get { return _modified; }
            set
            {
                if (_modified != value)
                {
                    _modified = value;
                    OnPropertyChanged();
                }
            }
        }

        public Delegate LoadGameCommand { get; set; }

        public Delegate SaveGameCommand { get; set; }
    }
}
