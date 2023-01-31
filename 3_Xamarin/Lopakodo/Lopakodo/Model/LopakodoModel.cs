using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lopakodo.Persistence;

namespace Lopakodo.Model
{
    public enum LevelEnum { Basement, Showers, School }
    public enum Direction { Left, Right, Up, Down }
    
    public struct Coordinates
    {
        public Int32 _i, _j;
    }

    public class Entity
    {
        private Coordinates _position;
        public Coordinates Position { get { return _position; } }

        public void SetI(Int32 i) { _position._i = i; }
        public void SetJ(Int32 j) { _position._j = j; }

        public Entity(Coordinates position)
        {
            _position = position;
        }
        public Entity(Int32 i, Int32 j)
        {
            _position._i = i;
            _position._j = j;
        }
    }

    public class Player : Entity
    {
        public Player(Coordinates position) : base(position) { }
        public Player(Int32 i, Int32 j) : base(i, j) { }
    }

    public class Guard : Entity
    {
        private Direction _direction;
        public Direction Direction { get { return _direction; } set { _direction = value; } }

        public Guard(Direction direction, Coordinates position) : base(position)
        {
            _direction = direction;
        }
        public Guard(Direction direction, Int32 i, Int32 j) : base(i, j)
        {
            _direction = direction;
        }
    }

    public class LopakodoModel
    {
        private ILopakodoData _gameFieldData;
        private LopakodoGameField _gameField;
        private LevelEnum _selectedField = LevelEnum.Basement;
        private Random _random = new Random();

        private List<Guard> _guards;
        private Player _player;
        private Coordinates _exit;

        public LopakodoGameField GameField { get { return _gameField; } }
        public LevelEnum SelectedField { get { return _selectedField; } set { _selectedField = value; } }
        public Player player { get { return _player; } }
        public Boolean isGameOver { get { return true; } }

        public LopakodoModel(ILopakodoData gameFieldData)
        {
            _gameFieldData = gameFieldData;
        }

        public event EventHandler<LopakodoEventArgs> GameOver;
        public event EventHandler<LopakodoEventArgs> GameAdvanced;
        public event EventHandler<LopakodoEventArgs> GameCreated;

        public async Task NewGame()
        {
            if (_gameFieldData == null)
                throw new InvalidOperationException("Data access was not available");

            switch (SelectedField)
            {
                case LevelEnum.Basement:
                    _gameField = await _gameFieldData.LoadAsync("basement");
                    break;
                case LevelEnum.Showers:
                    _gameField = await _gameFieldData.LoadAsync("showers");
                    break;
                case LevelEnum.School:
                    _gameField = await _gameFieldData.LoadAsync("school");
                    break;
            }

            _player = null;
            _guards = new List<Guard>();
            InitEntities();
        }

        public async Task LoadGameAsync(String path)
        {
            if (_gameFieldData == null)
                throw new InvalidOperationException("Data access was not available");

            _gameField = await _gameFieldData.LoadAsync(path);

            _player = null;
            _guards.Clear();
            InitEntities();
        }

        public async Task SaveGameAsync(String path)
        {
            if (_gameFieldData == null)
                throw new InvalidOperationException("Data access was not available");

            await _gameFieldData.SaveAsync(path, _gameField);
        }

        public void AdvanceGame()
        {
            foreach (Guard guard in _guards)
            {
                switch (guard.Direction)
                {
                    case Direction.Up:
                        if (guard.Position._i - 1 >= 0 && GameField[guard.Position._i-1, guard.Position._j] != 1 && GameField[guard.Position._i - 1, guard.Position._j] != 4)
                        {
                            //_gameField.SetValue(guard.Position._i, guard.Position._j, 0);
                            //_gameField.SetValue(guard.Position._i - 1, guard.Position._j, 2);
                            guard.SetI(guard.Position._i - 1);
                        }
                        else
                        {
                            guard.Direction = (Direction)_random.Next(0, 4);
                        }
                        break;
                    case Direction.Down:
                        if (guard.Position._i + 1 < GameField.Size && GameField[guard.Position._i + 1, guard.Position._j] != 1 && GameField[guard.Position._i + 1, guard.Position._j] != 4)
                        {
                            //_gameField.SetValue(guard.Position._i, guard.Position._j, 0);
                            //_gameField.SetValue(guard.Position._i + 1, guard.Position._j, 2);
                            guard.SetI(guard.Position._i + 1);
                        }
                        else
                        {
                            guard.Direction = (Direction)_random.Next(0, 4);
                        }
                        break;
                    case Direction.Left:
                        if (guard.Position._j - 1 >= 0 && GameField[guard.Position._i, guard.Position._j - 1] != 1 && GameField[guard.Position._i, guard.Position._j - 1] != 4)
                        {
                            //_gameField.SetValue(guard.Position._i, guard.Position._j, 0);
                            //_gameField.SetValue(guard.Position._i, guard.Position._j - 1, 2);
                            guard.SetJ(guard.Position._j - 1);
                        }
                        else
                        {
                            guard.Direction = (Direction)_random.Next(0, 4);
                        }
                        break;
                    case Direction.Right:
                        if (guard.Position._j + 1 < GameField.Size && GameField[guard.Position._i, guard.Position._j + 1] != 1 && GameField[guard.Position._i, guard.Position._j + 1] != 4)
                        {
                            //_gameField.SetValue(guard.Position._i, guard.Position._j, 0);
                            //_gameField.SetValue(guard.Position._i, guard.Position._j + 1, 2);
                            guard.SetJ(guard.Position._j + 1);
                        }
                        else
                        {
                            guard.Direction = (Direction)_random.Next(0, 4);
                        }
                        break;
                }
            }

            // Clear guards
            for (int i = 0; i < GameField.Size; ++i)
            {
                for (int j = 0; j < GameField.Size; ++j)
                {
                    if (GameField[i, j] == 2)
                    {
                        _gameField.SetValue(i, j, 0);
                    }
                }
            }

            // Put guards to their new location
            foreach(Guard guard in _guards)
            {
                _gameField.SetValue(guard.Position._i, guard.Position._j, 2);
            }

            GameAdvanced?.Invoke(this, new LopakodoEventArgs(false));

            PlayerSpotted();
            PlayerEscaped();
        }

        public void MovePlayer(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    if (_player.Position._i - 1 >= 0 && GameField[_player.Position._i - 1, _player.Position._j] != 1)
                    {
                        _gameField.SetValue(_player.Position._i, _player.Position._j, 0);
                        _gameField.SetValue(_player.Position._i - 1, _player.Position._j, 3);
                        _player.SetI(_player.Position._i - 1);
                    }
                    break;
                case Direction.Down:
                    if (_player.Position._i + 1 < GameField.Size && GameField[_player.Position._i + 1, _player.Position._j] != 1)
                    {
                        _gameField.SetValue(_player.Position._i, _player.Position._j, 0);
                        _gameField.SetValue(_player.Position._i + 1, _player.Position._j, 3);
                        _player.SetI(_player.Position._i + 1);
                    }
                    break;
                case Direction.Left:
                    if (_player.Position._j - 1 >= 0 && GameField[_player.Position._i, _player.Position._j - 1] != 1)
                    {
                        _gameField.SetValue(_player.Position._i, _player.Position._j, 0);
                        _gameField.SetValue(_player.Position._i, _player.Position._j - 1, 3);
                        _player.SetJ(_player.Position._j - 1);
                    }
                    break;
                case Direction.Right:
                    if (_player.Position._j + 1 < GameField.Size && GameField[_player.Position._i, _player.Position._j + 1] != 1)
                    {
                        _gameField.SetValue(_player.Position._i, _player.Position._j, 0);
                        _gameField.SetValue(_player.Position._i, _player.Position._j + 1, 3);
                        _player.SetJ(_player.Position._j + 1);
                    }
                    break;
            }

            GameAdvanced?.Invoke(this, new LopakodoEventArgs(false));

            PlayerSpotted();
            PlayerEscaped();
        }

        private void InitEntities()
        {
            for (int i = 0; i < GameField.Size; ++i)
            {
                for (int j = 0; j < GameField.Size; ++j)
                {
                    if (GameField[i, j] == 2)
                    {
                        _guards.Add(new Guard((Direction)_random.Next(0, 4), i, j));
                    }
                    else if (_player == null && GameField[i, j] == 3)
                    {
                        _player = new Player(i, j);
                    }
                    else if (GameField[i, j] == 4)
                    {
                        _exit._i = i;
                        _exit._j = j;
                    }
                }
            }
            GameCreated?.Invoke(this, new LopakodoEventArgs(false));
        }

        private void PlayerSpotted()
        {
            Boolean spotted = false;
            Int32 pI = _player.Position._i, pJ = _player.Position._j;

            foreach (Guard guard in _guards)
            {
                Int32 gI = guard.Position._i, gJ = guard.Position._j;

                // A distance of one cannot have walls in between
                if (Math.Abs(pI-gI) < 2 && Math.Abs(pJ - gJ) < 2) { spotted = true; }

                // Distance of 2
                // Straight lines
                if (pI - gI == 2)
                {
                    if (pJ == gJ && (GameField[gI + 1,gJ] == 0)) { spotted = true; }
                }
                if (pI - gI == -2)
                {
                    if (pJ == gJ && (GameField[gI - 1, gJ] == 0)) { spotted = true; }
                }
                if (pJ - gJ == 2)
                {
                    if (pI == gI && (GameField[gI, gJ + 1] == 0)) { spotted = true; }
                }
                if (pJ - gJ == -2)
                {
                    if (pI == gI && (GameField[gI, gJ - 1] == 0)) { spotted = true; }
                }

                // Corners
                if (pI - gI == 2 && pJ - gJ == 2)
                {
                    if (GameField[gI + 1, gJ + 1] == 0) { spotted = true; }
                }
                if (pI - gI == -2 && pJ - gJ == 2)
                {
                    if (GameField[gI - 1, gJ + 1] == 0) { spotted = true; }
                }
                if (pI - gI == 2 && pJ - gJ == -2)
                {
                    if (GameField[gI + 1, gJ - 1] == 0) { spotted = true; }
                }
                if (pI - gI == -2 && pJ - gJ == -2)
                {
                    if (GameField[gI - 1, gJ - 1] == 0) { spotted = true; }
                }

                // In between
                if (pI - gI == 2 && pJ - gJ == 1)
                {
                    if (GameField[gI + 1, gJ] == 0 && GameField[gI + 1, gJ + 1] == 0) { spotted = true; }
                }
                if (pI - gI == -2 && pJ - gJ == 1)
                {
                    if (GameField[gI - 1, gJ] == 0 && GameField[gI - 1, gJ + 1] == 0) { spotted = true; }
                }
                if (pI - gI == 2 && pJ - gJ == -1)
                {
                    if (GameField[gI + 1, gJ] == 0 && GameField[gI + 1, gJ - 1] == 0) { spotted = true; }
                }
                if (pI - gI == -2 && pJ - gJ == -1)
                {
                    if (GameField[gI - 1, gJ] == 0 && GameField[gI - 1, gJ - 1] == 0) { spotted = true; }
                }

                if (pI - gI == 1 && pJ - gJ == 2)
                {
                    if (GameField[gI, gJ + 1] == 0 && GameField[gI + 1, gJ + 1] == 0) { spotted = true; }
                }
                if (pI - gI == 1 && pJ - gJ == -2)
                {
                    if (GameField[gI, gJ - 1] == 0 && GameField[gI + 1, gJ - 1] == 0) { spotted = true; }
                }
                if (pI - gI == -1 && pJ - gJ == 2)
                {
                    if (GameField[gI, gJ + 1] == 0 && GameField[gI - 1, gJ + 1] == 0) { spotted = true; }
                }
                if (pI - gI == -1 && pJ - gJ == -2)
                {
                    if (GameField[gI, gJ - 1] == 0 && GameField[gI - 1, gJ - 1] == 0) { spotted = true; }
                }

            }

            if (spotted) { GameOver?.Invoke(this, new LopakodoEventArgs(false)); }
        }

        private void PlayerEscaped()
        {
            if (_player.Position._i == _exit._i && _player.Position._j == _exit._j)
            {
                GameOver?.Invoke(this, new LopakodoEventArgs(true));
            }
        }
    }
}
