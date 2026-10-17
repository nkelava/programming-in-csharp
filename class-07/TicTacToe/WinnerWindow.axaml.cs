using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using TicTacToe.Entities;


namespace TicTacToe
{
    public partial class WinnerWindow : Window
    {
        public WinnerWindow(Player player, GameStatus gameStatus)
        {
            InitializeComponent();

            lblGameResult.Content = (gameStatus == GameStatus.Win) ? $"{player.Name} won!" : "It's a draw!";
        }

        private void btnPlayAgain_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OpenMainWindow(object sender, WindowClosingEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void CloseAllWindows()
        {
            var desktop = (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;

            foreach (Window window in desktop.Windows.ToList())
            {
                window.Close();
            }
        }

        private void btnFinish_Click(object sender, RoutedEventArgs e)
        {
            this.Closing += this.OpenMainWindow;

            CloseAllWindows();
        }
    }
}
