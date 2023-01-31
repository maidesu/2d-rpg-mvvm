using System;

namespace Lopakodo.Model
{
    public class LopakodoEventArgs : EventArgs
    {
        private Boolean _playerEscaped;
        //private Boolean _playerSpotted = false;

        public Boolean PlayerEscaped { get { return _playerEscaped; } }
        //public Boolean PlayerSpotted { get { return _playerSpotted; } }

        public LopakodoEventArgs(Boolean playerEscaped)//, Boolean playerSpotted)
        {
            _playerEscaped = playerEscaped;
            //_playerSpotted = playerSpotted;
        }
    }
}
