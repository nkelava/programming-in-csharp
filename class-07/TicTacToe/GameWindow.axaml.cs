using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using TicTacToe.Entities;
using TicTacToe.Services;


namespace TicTacToe
{
    public partial class GameWindow : Window
    {
        private static Game _game;
        private static List<Button> _gridButtons;

    public GameWindow(Player p1, Player p2, GameType gameType)
        {
            InitializeComponent();

            _game = new Game(p1, p2, gameType);
            _gridButtons = new List<Button> { btn0, btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8 };

            SetInfoDisplay();
            SubscribeButtons();

            this.Closed += new EventHandler(this.UpdateLeaderboardFile);
        }

        private void SetInfoDisplay()
        {
            lblPlayer1Name.Content = _game.Player1.Name;
            lblPlayer2Name.Content = _game.Player2.Name;
            SetScoreDisplay();
            lblPlayerTurn.Content = _game.GetPlayersTurnText();
        }

        private void SetScoreDisplay()
        {
            lblPlayer1Score.Content = _game.Player1.Score.ToString();
            lblPlayer2Score.Content = _game.Player2.Score.ToString();
        }

        private void SubscribeButtons()
        {
            foreach (var btn in _gridButtons)
            {
                btn.Click += this.PlayMove;
            }
        }

        private void ModifyButtonAfterClick(Button button, Player player)
        {
            button.Content = player.Icon;
            button.Foreground = SolidColorBrush.Parse(player.Color);
            button.IsEnabled = false;
        }

        private void HighlightWinningLine()
        {
            foreach (var btn in _gridButtons)
            {
                foreach (var winPosition in _game._winLine)
                {
                    if (btn.Name.Contains(winPosition.ToString()))
                    {
                        btn.IsEnabled = true;
                        btn.Background = Brushes.PeachPuff;
                    }
                }
            }
        }

        private async Task ShowWinner(Player player)
        {
            var winnerWindow = new WinnerWindow(player, _game.Status);
            await winnerWindow.ShowDialog(this);
        }

        private void ResetButtons()
        {
            foreach (var btn in _gridButtons) 
            {
                btn.Content = "";
                btn.Background = Brushes.White;
                btn.IsEnabled = true;
            }
        }

        private void UpdateLeaderboardFile(object sender, EventArgs e)
        {
            var fileHandler = new FileHandler(FileHandler.LeaderboardPath);
            fileHandler.SaveOrUpdatePlayers(new List<Player>() { _game.Player1, _game.Player2 });
        }

        private void StartNextGame()
        {
            ResetButtons();
            SetScoreDisplay();
            _game.ResetGame();
        }

        private async void PlayMove(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Player currentPlayer = (_game.IsPlayer1Turn) ? _game.Player1 : _game.Player2;
            var buttonNumber = Convert.ToInt32(string.Join("", button.Name.ToCharArray().Where(Char.IsDigit)));

            ModifyButtonAfterClick(button, currentPlayer);
            
            _game.PlayMove(buttonNumber, currentPlayer.Id);
            lblPlayerTurn.Content = _game.GetPlayersTurnText();

            if (_game.Status == GameStatus.Win || _game.Status == GameStatus.Draw) 
            {
                if (_game.Status == GameStatus.Win) 
                {
                    HighlightWinningLine();
                }

                await ShowWinner(currentPlayer);
                StartNextGame();
            }

            if(_game.GameType.Equals(GameType.PVC) && !_game.IsPlayer1Turn) 
            {
                ComputerMoveStart();
            }
        }

        private async void ComputerMoveStart()
        {
            Button newButton = GetRandomButton();
            this.IsEnabled = false;
            await Task.Delay(1000);
            this.IsEnabled = true;
            newButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, newButton));
        }

        private Button GetRandomButton()
        {
            var randButton = new Button();

            foreach (var button in _gridButtons) 
            {
                if (button.IsEnabled == true) 
                {
                    return button;
                }
            }
            return randButton;
        }
    }
}
