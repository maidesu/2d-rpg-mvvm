using System;

namespace Lopakodo.Persistence
{
    public class LopakodoGameField
    {
        private Int32 _fieldSize;
        private Int32[,] _field;

        private Boolean _entry = false, _exit = false;


        public Int32 Size { get { return _fieldSize; } }
        public Int32 this[Int32 i, Int32 j] { get { return GetValue(i, j); } }
        public Boolean HasEntry { get { return _entry; } }
        public Boolean HasExit { get { return _exit; } }


        public LopakodoGameField(Int32 fieldSize)
        {
            if (fieldSize < 2)
                throw new ArgumentOutOfRangeException("Field size argument less than 2", "tableSize");

            _fieldSize = fieldSize;
            _field = new Int32[_fieldSize, _fieldSize];
        }


        public Int32 GetValue(Int32 i, Int32 j)
        {
            if (i < 0 || i > this.Size+1)
                throw new ArgumentOutOfRangeException("Argument out of range", "i");
            if (j < 0 || j > this.Size+1)
                throw new ArgumentOutOfRangeException("Argument out of range", "j");

            return _field[i, j];
        }

        public void SetValue(Int32 i, Int32 j, Int32 val)
        {
            if (i < 0 || i > this.Size + 1)
                throw new ArgumentOutOfRangeException("Argument out of range", "i");
            if (j < 0 || j > this.Size + 1)
                throw new ArgumentOutOfRangeException("Argument out of range", "j");
            if (val == 3) {
                if (HasEntry)
                {
                    throw new ArgumentException("There cannot be multiple entry points");
                }
                else
                    _entry = true;
            }
            if (val == 4)
            {
                if (HasExit)
                {
                    throw new ArgumentException("There cannot be multiple exit points");
                }
                else
                    _exit = true;
            }

            if (_field[i, j] == 3 && val == 0) { _entry = false; }
            _field[i, j] = val;
        }

    }
}
