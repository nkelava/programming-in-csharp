using System.ComponentModel;
using System.Windows;
using TicTacToe.Entities;


namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for WinnerWindow.xaml
    /// </summary>
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

        private void OpenMainWindow(object sender, CancelEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void CloseAllWindows()
        {
            foreach (Window window in Application.Current.Windows)
            {
                window.Close();
            }
        }

        private void btnFinish_Click(object sender, RoutedEventArgs e)
        {
            this.Closing += new CancelEventHandler(this.OpenMainWindow);

            CloseAllWindows();
        }
    }
}
