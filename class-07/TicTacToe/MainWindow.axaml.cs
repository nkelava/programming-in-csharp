using Avalonia.Controls;
using Avalonia.Interactivity;
using TicTacToe.Entities;


namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnPvp_Click(object sender, RoutedEventArgs e)
        {
            var usernameInputWindow = new NameInputWindow(GameType.PVP);
            usernameInputWindow.Show();
            
            this.Close();
        }

        private void btnPvc_Click(object sender, RoutedEventArgs e)
        {
            var usernameInputWindow = new NameInputWindow(GameType.PVC);
            usernameInputWindow.Show();

            this.Close();
        }

        private void btnLeaderboard_Click(object sender, RoutedEventArgs e)
        {
            var leaderboardWindow = new LeaderboardWindow();
            leaderboardWindow.Show();

            this.Close();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
