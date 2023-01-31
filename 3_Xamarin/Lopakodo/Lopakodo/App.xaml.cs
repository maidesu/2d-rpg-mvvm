using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using Lopakodo.Model;
using Lopakodo.Persistence;
using Lopakodo.View;
using Lopakodo.ViewModel;

namespace Lopakodo
{
    public partial class App : Application
    {
        private ILopakodoData _persistence;
        private LopakodoModel _model;
        private LopakodoViewModel _viewModel;

        private GamePage _viewGame;
        private SettingsPage _viewSettings;
        private NavigationPage _view;

        private IStore _store;
        private StoredGameBrowserModel _viewBrowserModel;
        private StoredGameBrowserViewModel _viewBrowserViewModel;
        private LoadGamePage _viewLoadGame;
        private SaveGamePage _viewSaveGame;

        private Boolean _timer;
        private GameFieldProperty _gfp;


        public App()
        {
            _persistence = DependencyService.Get<ILopakodoData>();
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


            _viewGame = new GamePage();
            _viewGame.BindingContext = _viewModel;

            _viewSettings = new SettingsPage();
            _viewSettings.BindingContext = _viewModel;

            _store = DependencyService.Get<IStore>();
            _viewBrowserModel = new StoredGameBrowserModel(_store);
            _viewBrowserViewModel = new StoredGameBrowserViewModel(_viewBrowserModel);

            _viewBrowserViewModel.GameLoading += new EventHandler<StoredGameEventArgs>(Browser_Load_Game);
            _viewBrowserViewModel.GameSaving += new EventHandler<StoredGameEventArgs>(Browser_Save_Game);


            _viewLoadGame = new LoadGamePage();
            _viewLoadGame.BindingContext = _viewBrowserViewModel;
            _viewSaveGame = new SaveGamePage();
            _viewSaveGame.BindingContext = _viewBrowserViewModel;

            _view = new NavigationPage(_viewGame);
            MainPage = _view;
        }

        protected override async void OnStart()
        {
            _timer = false;

            await _persistence.InitMaps();

            await _model.NewGame();
            _gfp.GameFieldSize = _model.GameField.Size;

            _viewModel.InitViewFields();
            _viewModel.RefreshViewFields();


            if (!_timer)
            {
                _timer = true;
                Device.StartTimer(TimeSpan.FromSeconds(1), () => { _model.AdvanceGame(); return _timer; });
            }
        }

        protected override void OnSleep()
        {
            _timer = false;

            try { Task.Run(async () => await _model.SaveGameAsync("Suspended")); }
            catch { }
        }

        protected override void OnResume()
        {
            try { Task.Run(async () => { await _model.LoadGameAsync("Suspended");
                if (!_timer)
                {
                    _timer = true;
                    Device.StartTimer(TimeSpan.FromSeconds(1), () => { _model.AdvanceGame(); return _timer; });
                }
            }); }
            catch { }
        }

        private async void Browser_Load_Game(object sender, StoredGameEventArgs e)
        {
            await _view.PopAsync();

            try
            {
                await _model.LoadGameAsync(e.Name);
                _viewModel.InitViewFields();
                _viewModel.RefreshViewFields();

                await MainPage.DisplayAlert("Lopakodo game", "Game loaded.", "OK");

                if (!_timer)
                {
                    _timer = true;
                    Device.StartTimer(TimeSpan.FromSeconds(1), () => { _model.AdvanceGame(); return _timer; });
                }
            }
            catch
            {
                await MainPage.DisplayAlert("Lopakodo game", "Failed to load game..", "OK");
            }
        }

        private async void Browser_Save_Game(object sender, StoredGameEventArgs e)
        {
            await _view.PopAsync();
            _timer = false;

            try
            {
                await _model.SaveGameAsync(e.Name);
                await MainPage.DisplayAlert("Lopakodo game", "Game saved.", "OK");
            }
            catch {
                await MainPage.DisplayAlert("Lopakodo game", "Failed to save game..", "OK");
            }

            
        }


        private async void Quit_Game(object sender, EventArgs e)
        {
            _timer = false;
            await _view.PushAsync(_viewSettings);
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

        private async void Save_Game(object sender, EventArgs e)
        {
            _timer = false;

            await _viewBrowserModel.UpdateAsync();
            await _view.PushAsync(_viewSaveGame);

            if (!_timer)
            {
                _timer = true;
                Device.StartTimer(TimeSpan.FromSeconds(1), () => { _model.AdvanceGame(); return _timer; });
            }
        }

        private async void Pause_Game(object sender, EventArgs e)
        {
            _timer = false;

            await MainPage.DisplayAlert("Lopakodo Game", "The game is paused, press OK to continue", "OK");

            if (!_timer)
            {
                _timer = true;
                Device.StartTimer(TimeSpan.FromSeconds(1), () => { _model.AdvanceGame(); return _timer; });
            }
        }

        private async void Load_Game(object sender, EventArgs e)
        {
            _timer = false;

            await _viewBrowserModel.UpdateAsync();
            await _view.PushAsync(_viewLoadGame);

            if (!_timer)
            {
                _timer = true;
                Device.StartTimer(TimeSpan.FromSeconds(1), () => { _model.AdvanceGame(); return _timer; });
            }
        }


        private async void New_Game(object sender, EventArgs e)
        {
            _timer = false;

            await _model.NewGame();
            _gfp.GameFieldSize = _model.GameField.Size;

            if (!_timer)
            {
                _timer = true;
                Device.StartTimer(TimeSpan.FromSeconds(1), () => { _model.AdvanceGame(); return _timer; });
            }
        }

        private async void Game_Over(Object sender, LopakodoEventArgs e)
        {
            _timer = false;

            if (e.PlayerEscaped)
            {
                await MainPage.DisplayAlert("The player successfully escaped!", "Lopakodo game complete", "OK");
            }
            else
            {
                await MainPage.DisplayAlert("The player was spotted!", "Lopakodo game lost", "OK");
            }

            await _model.NewGame();

            _viewModel.InitViewFields();
            _viewModel.RefreshViewFields();

            if (!_timer)
            {
                _timer = true;
                Device.StartTimer(TimeSpan.FromSeconds(1), () => { _model.AdvanceGame(); return _timer; });
            }
        }
    }
}
