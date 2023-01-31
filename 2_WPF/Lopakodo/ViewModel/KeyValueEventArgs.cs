using System;

namespace Lopakodo.Model
{
    public class KeyValueEventArgs : EventArgs
    {
        private String _key;
        public String Key { get { return _key; } }

        public KeyValueEventArgs(String key)
        {
            _key = key;
        }
    }
}
