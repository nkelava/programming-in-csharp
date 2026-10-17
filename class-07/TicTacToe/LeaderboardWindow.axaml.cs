using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using TicTacToe.Entities;
using TicTacToe.Services;

namespace TicTacToe
{
    public partial class LeaderboardWindow : Window
    {
        public LeaderboardWindow()
        {
            InitializeComponent();
            var fileHandler = new FileHandler(FileHandler.LeaderboardPath);
            var players = fileHandler.ReadFromFile();

            dataGridUsers.ItemsSource = players;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            
            this.Close();
        }
    }
}
