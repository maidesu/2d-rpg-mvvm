using System;

namespace Lopakodo.ViewModel
{
    class LopakodoViewField : ViewModelBase
    {
        private String _text;

        public Int32 Id { get; set; }

        public String Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        public Int32 X { get; set; }
        public Int32 Y { get; set; }
    }
}
