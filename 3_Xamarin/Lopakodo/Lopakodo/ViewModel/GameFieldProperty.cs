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
                OnPropertyChanged(nameof(GraphicsFieldSize));
            }
        }

        public Int32 GraphicsFieldSize
        {
            //get { return (int)Math.Floor(400.0 / _gameFieldSize + 5.0); }
            get { return 10; }
        }
    }
}
