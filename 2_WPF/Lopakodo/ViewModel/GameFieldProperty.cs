using System;
using System.Collections.Generic;
using System.Text;

namespace Lopakodo.ViewModel
{
    class GameFieldProperty : ViewModelBase
    {
        private Int32 _gameFieldSize;

        public Int32 GameFieldSize
        {
            get { return _gameFieldSize; }
            set
            {
                _gameFieldSize = value;
                OnPropertyChanged();
            }
        }
    }
}
