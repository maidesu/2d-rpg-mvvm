using System;
using System.Collections.ObjectModel;
using Lopakodo.Model;

namespace Lopakodo.ViewModel
{
    class LopakodoViewModel : ViewModelBase
    {
        private LopakodoModel _model;
        private GameFieldProperty _gfp;

        public GameFieldProperty gfp { get { return _gfp; } set { _gfp = value; } }
        public LevelEnum SelectedField { get { return _model.SelectedField; } set { _model.SelectedField = value; } }
        public ObservableCollection<LopakodoViewField> Fields { get; set; }

        public event EventHandler NewGameBasement;
        public event EventHandler NewGameShowers;
        public event EventHandler NewGameSchool;
        public event EventHandler QuitGame;
        public event EventHandler SaveGame;
        public event EventHandler LoadGame;
        public event EventHandler PauseGame;
        public event EventHandler<KeyValueEventArgs> MovePlayer;

        public Delegate NewGameBasementCommand { get; private set; }
        public Delegate NewGameShowersCommand { get; private set; }
        public Delegate NewGameSchoolCommand { get; private set; }
        public Delegate SaveGameCommand { get; private set; }
        public Delegate LoadGameCommand { get; private set; }
        public Delegate QuitGameCommand { get; private set; }
        public Delegate PauseGameCommand { get; private set; }
        public Delegate MovePlayerCommand { get; private set; }

        public LopakodoViewModel(LopakodoModel model, GameFieldProperty gfp)
        {
            _model = model;
            _gfp = gfp;
            _model.GameCreated += (sender, args) => InitViewFields();
            _model.GameAdvanced += (sender, args) => RefreshViewFields();

            NewGameBasementCommand = new Delegate(param => NewGameBasement?.Invoke(this, EventArgs.Empty));
            NewGameShowersCommand = new Delegate(param => NewGameShowers?.Invoke(this, EventArgs.Empty));
            NewGameSchoolCommand = new Delegate(param => NewGameSchool?.Invoke(this, EventArgs.Empty));
            SaveGameCommand = new Delegate(param => SaveGame?.Invoke(this, EventArgs.Empty));
            LoadGameCommand = new Delegate(param => LoadGame?.Invoke(this, EventArgs.Empty));
            QuitGameCommand = new Delegate(param => QuitGame?.Invoke(this, EventArgs.Empty));
            PauseGameCommand = new Delegate(param => PauseGame?.Invoke(this, EventArgs.Empty));
            MovePlayerCommand = new Delegate(param => MovePlayer?.Invoke(this, new KeyValueEventArgs(param.ToString())));

            Fields = new ObservableCollection<LopakodoViewField>();
        }

        private void InitViewFields()
        {
            _gfp.GameFieldSize = _model.GameField.Size;

            Fields.Clear();
            
            for (Int32 i = 0; i < _gfp.GameFieldSize; ++i)
                for (Int32 j = 0; j < _gfp.GameFieldSize; ++j)
                {
                    Fields.Add(new LopakodoViewField
                    {
                        Id = i * _gfp.GameFieldSize + j,
                        X = i,
                        Y = j,
                        Text = String.Empty
                    });
                }
        }

        private void RefreshViewFields()
        {
            for (Int32 i = 0; i < _gfp.GameFieldSize; ++i)
                for (Int32 j = 0; j < _gfp.GameFieldSize; ++j)
                {
                    switch (_model.GameField[i, j])
                    {
                        case 1:
                            Fields[i * _gfp.GameFieldSize + j].Text = "W";
                            break;
                        case 2:
                            Fields[i * _gfp.GameFieldSize + j].Text = "🐒";
                            break;
                        case 3:
                            Fields[i * _gfp.GameFieldSize + j].Text = "🎓";
                            break;
                        case 4:
                            Fields[i * _gfp.GameFieldSize + j].Text = "❗";
                            break;
                        default:
                            Fields[i * _gfp.GameFieldSize + j].Text = "";
                            break;
                    }
                }
        }
    }
}
