using System;
using System.Drawing;
using System.Windows.Forms;
using Lopakodo.Model;
using Lopakodo.Persistence;

namespace Lopakodo.View
{
    public partial class LopakodoForm : Form
    {
        private const Int32 GuardWaits = 1000;

        private ILopakodoData _gameFieldData;
        private LopakodoModel _model;
        private Button[,] _buttonField;
        private Timer _guardTimer;
        
        public LopakodoForm()
        {
            InitializeComponent();
            KeyPreview = true;
            KeyDown += new KeyEventHandler(ControlsKeyDown);
        }

        private async void LopakodoForm_Load(Object sender, EventArgs e)
        {
            _gameFieldData = new LopakodoFileData();
            _model = new LopakodoModel(_gameFieldData);

            try
            {
                await _model.NewGame();
            }
            catch (LopakodoDataException)
            {
                MessageBox.Show("A default map was inaccessable!", "Load error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

            _model.GameOver += new EventHandler<LopakodoEventArgs>(ViewGameOver);

            _guardTimer = new Timer();
            _guardTimer.Interval = GuardWaits;
            _guardTimer.Tick += new EventHandler(TimerTick);

            GenerateEmptyButtonField();
            RefreshButtonField();

            _guardTimer.Start();
        }

        private async void BasementMenuItem_Click(Object sender, EventArgs e)
        {
            _guardTimer.Stop();

            DestroyButtonField(_model.GameField.Size);

            _model.SelectedField = LevelEnum.Basement;

            try
            {
                await _model.NewGame();
            }
            catch (LopakodoDataException)
            {
                MessageBox.Show("A default map was inaccessable!", "Load error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

            GenerateEmptyButtonField();
            RefreshButtonField();

            _guardTimer.Start();
        }

        private async void ShowersMenuItem_Click(Object sender, EventArgs e)
        {
            _guardTimer.Stop();

            DestroyButtonField(_model.GameField.Size);

            _model.SelectedField = LevelEnum.Showers;

            try
            {
                await _model.NewGame();
            }
            catch (LopakodoDataException)
            {
                MessageBox.Show("A default map was inaccessable!", "Load error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

            GenerateEmptyButtonField();
            RefreshButtonField();

            _guardTimer.Start();
        }

        private async void SchoolMenuItem_Click(Object sender, EventArgs e)
        {
            _guardTimer.Stop();

            DestroyButtonField(_model.GameField.Size);

            _model.SelectedField = LevelEnum.School;

            try
            {
                await _model.NewGame();
            }
            catch (LopakodoDataException)
            {
                MessageBox.Show("A default map was inaccessable!", "Load error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

            GenerateEmptyButtonField();
            RefreshButtonField();

            _guardTimer.Start();
        }

        private async void SaveGameMenuItem_Click(Object sender, EventArgs e)
        {
            _guardTimer.Stop();

            if (_saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    await _model.SaveGameAsync(_saveFileDialog.FileName);
                }
                catch (LopakodoDataException)
                {
                    MessageBox.Show("The given path was inaccessable", "Save error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            _guardTimer.Start();
        }

        private async void LoadGameMenuItem_Click(Object sender, EventArgs e)
        {
            _guardTimer.Stop();

            if (_openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Int32 oldFieldSize = _model.GameField.Size;

                try
                {
                    await _model.LoadGameAsync(_openFileDialog.FileName);
                    DestroyButtonField(oldFieldSize); 
                    GenerateEmptyButtonField();
                    RefreshButtonField();
                }
                catch (LopakodoDataException)
                {
                    MessageBox.Show("The given path was inaccessable", "Load error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            _guardTimer.Start();
        }

        private void QuitGameMenuItem_Click(Object sender, EventArgs e)
        {
            _guardTimer.Stop();

            if (MessageBox.Show("Are you sure you want to quit?", "Lopakodo Game", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Close();
            }

            _guardTimer.Start();
        }

        private void ControlsKeyDown(Object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.W:
                    _model.MovePlayer(Direction.Up);
                    RefreshButtonField();
                    break;
                case Keys.S:
                    _model.MovePlayer(Direction.Down);
                    RefreshButtonField();
                    break;
                case Keys.A:
                    _model.MovePlayer(Direction.Left);
                    RefreshButtonField();
                    break;
                case Keys.D:
                    _model.MovePlayer(Direction.Right);
                    RefreshButtonField();
                    break;
                case Keys.Space:
                    GamePaused();
                    break;
                default:
                    break;
            }
        }

        private async void ViewGameOver(Object sender, LopakodoEventArgs e)
        {
            _guardTimer.Stop();

            if (e.PlayerEscaped)
            {
                MessageBox.Show("The player successfully escaped!", "Lopakodo game complete", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("The player was spotted!", "Lopakodo game lost", MessageBoxButtons.OK);
            }

            try
            {
                await _model.NewGame();
            }
            catch (LopakodoDataException)
            {
                MessageBox.Show("A default map was inaccessable!", "Load error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

            RefreshButtonField();

            _guardTimer.Start();
        }

        private void TimerTick(Object sender, EventArgs e)
        {
            _model.AdvanceGame();
            RefreshButtonField();
        }

        private void GenerateEmptyButtonField()
        {
            Int32 fieldSize = _model.GameField.Size;
            _buttonField = new Button[fieldSize, fieldSize];

            for (Int32 i = 0; i < fieldSize; ++i)
            {
                for (Int32 j = 0; j < fieldSize; ++j)
                {
                    _buttonField[i, j] = new Button();
                    _buttonField[i, j].Location = new Point(4 + (460 / fieldSize) * j, 28 + (460 / fieldSize) * i);
                    _buttonField[i, j].Size = new Size(460 / fieldSize, 460 / fieldSize);
                    _buttonField[i, j].Font = new Font(FontFamily.GenericSansSerif, (460 / fieldSize)/2);
                    _buttonField[i, j].Enabled = false;
                    _buttonField[i, j].FlatStyle = FlatStyle.Flat;

                    Controls.Add(_buttonField[i, j]);
                }
            }
        }

        private void RefreshButtonField()
        {
            Int32 fieldSize = _model.GameField.Size;

            for (Int32 i = 0; i < fieldSize; ++i)
            {
                for (Int32 j = 0; j < fieldSize; ++j)
                {
                    switch (_model.GameField[i,j])
                    {
                        case 1:
                            _buttonField[i, j].BackColor = Color.Black;
                            break;
                        case 2:
                            _buttonField[i, j].Text = "🐒";
                            break;
                        case 3:
                            _buttonField[i, j].Text = "🎓" ;
                            break;
                        case 4:
                            _buttonField[i, j].Text = "❗";
                            break;
                        default:
                            _buttonField[i, j].Text = "";
                            break;
                    }
                }
            }
        }

        private void DestroyButtonField(Int32 oldFieldSize)
        {
            for (Int32 i = 0; i < oldFieldSize; ++i)
            {
                for (Int32 j = 0; j < oldFieldSize; ++j)
                {
                    Button currentButton = _buttonField[i, j];

                    _buttonField[i, j] = null;
                    Controls.Remove(currentButton);
                }
            }
        }

        private void GamePaused()
        {
            _guardTimer.Stop();

            MessageBox.Show("The game is paused, press OK to continue", "Lopakodo Game", MessageBoxButtons.OK, MessageBoxIcon.Question);

            _guardTimer.Start();
        }
    }
}
