using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using Lopakodo.Model;
using Lopakodo.Persistence;
using Lopakodo.View;
using Lopakodo.ViewModel;
using Microsoft.Win32;

namespace Lopakodo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private LopakodoFileData _persistence;
        private LopakodoModel _model;
        private LopakodoViewModel _viewModel;
        private MainWindow _view;
        private DispatcherTimer _timer;
        private GameFieldProperty _gfp;

        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
        }

        private async void App_Startup(object sender, StartupEventArgs e)
        {
            _persistence = new LopakodoFileData();
            _gfp = new GameFieldProperty();
            
            _model = new LopakodoModel(_persistence);
            _model.GameOver += new EventHandler<LopakodoEventArgs>(Game_Over);


            _viewModel = new LopakodoViewModel(_model, _gfp);

            _viewModel.NewGameBasement += (sender, args) => _model.SelectedField = LevelEnum.Basement;
            _viewModel.NewGameBasement += new EventHandler(New_Game);

            _viewModel.NewGameShowers += (sender, args) => _model.SelectedField = LevelEnum.Showers;
            _viewModel.NewGameShowers += new EventHandler(New_Game);

            _viewModel.NewGameSchool += (sender, args) => _model.SelectedField = LevelEnum.School;
            _viewModel.NewGameSchool += new EventHandler(New_Game);

            _viewModel.QuitGame += new EventHandler(Quit_Game);
            _viewModel.SaveGame += new EventHandler(Save_Game);
            _viewModel.LoadGame += new EventHandler(Load_Game);
            _viewModel.PauseGame += new EventHandler(Pause_Game);
            _viewModel.MovePlayer += new EventHandler<KeyValueEventArgs>(Move_Player);            

            try
            {
                await _model.NewGame();
                _gfp.GameFieldSize = _model.GameField.Size;
            }
            catch (LopakodoDataException)
            {
                MessageBox.Show("A default map was inaccessable!", "Load error", MessageBoxButton.OK, MessageBoxImage.Error);
                _view.Close();
            }

            _view = new MainWindow();
            _view.DataContext = _viewModel;
            _view.Closing += new System.ComponentModel.CancelEventHandler(View_Quit);
            _view.Show();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1); // Guard waits
            _timer.Tick += new EventHandler(Timer_Tick);
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _model.AdvanceGame();
        }

        private void View_Quit(object sender, CancelEventArgs e)
        {
            _timer.Stop();

            if (MessageBox.Show("Are you sure you want to quit?", "Lopakodo Game", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }

            _timer.Start();
        }

        private void Quit_Game(object sender, EventArgs e)
        {
            _view.Close();
        }

        private void Move_Player(object sender, KeyValueEventArgs e)
        {
            switch (e.Key)
            {
                case "W":
                    _model.MovePlayer(Direction.Up);
                    break;
                case "S":
                    _model.MovePlayer(Direction.Down);
                    break;
                case "A":
                    _model.MovePlayer(Direction.Left);
                    break;
                case "D":
                    _model.MovePlayer(Direction.Right);
                    break;
                default:
                    break;
            }
        }

        private async void New_Game(object sender, EventArgs e)
        {
            await _model.NewGame();
            _timer.Start();
        }

        private async void Save_Game(object sender, EventArgs e)
        {
            _timer.Stop();

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "Save current game";
            saveDialog.Filter = "Lopakodo save (*.txt)|*.txt";

            if (true == saveDialog.ShowDialog())
            {
                try
                {
                    await _model.SaveGameAsync(saveDialog.FileName);
                }
                catch (LopakodoDataException)
                {
                    MessageBox.Show("The given path was inaccessable", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            _timer.Start();
        }

        private void Pause_Game(object sender, EventArgs e)
        {
            _timer.Stop();

            MessageBox.Show("The game is paused, press OK to continue", "Lopakodo Game", MessageBoxButton.OK, MessageBoxImage.Information);

            _timer.Start();
        }

        private async void Load_Game(object sender, EventArgs e)
        {
            _timer.Stop();

            OpenFileDialog loadDialog = new OpenFileDialog();
            loadDialog.Title = "Load saved game"; ;
            loadDialog.Filter = "Lopakodo save (*.txt)|*.txt";
            
            if (true == loadDialog.ShowDialog())
            {
                try
                {
                    await _model.LoadGameAsync(loadDialog.FileName);
                }
                catch (LopakodoDataException)
                {
                    MessageBox.Show("The given path was inaccessable", "Load error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            _timer.Start();
        }

        private async void Game_Over(Object sender, LopakodoEventArgs e)
        {
            _timer.Stop();

            if (e.PlayerEscaped)
            {
                MessageBox.Show("The player successfully escaped!", "Lopakodo game complete", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("The player was spotted!", "Lopakodo game lost", MessageBoxButton.OK);
            }

            try
            {
                await _model.NewGame();
            }
            catch (LopakodoDataException)
            {
                MessageBox.Show("A default map was inaccessable!", "Load error", MessageBoxButton.OK, MessageBoxImage.Error);
                _view.Close();
            }

            _timer.Start();
        }
    }
}
