using System.Collections.Generic;
using System.Windows;
using TicTacToe.Entities;
using TicTacToe.Services;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for LeaderboardWindow.xaml
    /// </summary>
    public partial class LeaderboardWindow : Window
    {
        public LeaderboardWindow()
        {
            InitializeComponent();
            var fileHandler = new FileHandler(@"D:\ProjDev\C#\Lab_vjezbe_C#\vjezba07\TicTacToe\Leaderboard\leaderboard.json");
            var players = fileHandler.ReadFromFile();

            // ListView
            listViewUsers.ItemsSource = players;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            
            this.Close();
        }
    }
}
