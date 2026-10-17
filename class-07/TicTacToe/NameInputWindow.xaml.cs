using System.Windows;
using System.Windows.Controls;
using TicTacToe.Entities;


namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for NameInputWindow.xaml
    /// </summary>
    public partial class NameInputWindow : Window
    {
        private string _inputPlaceholder = "Enter username...";
        private string _defaultNameP1 = "Player 1";
        private string _defaultNameP2 = "Player 2";
        private string _iconP1 = "X";
        private string _iconP2 = "O";
        private string _colorP1 = "#FF35D479";
        private string _colorP2 = "#FF76B2F0";
        private GameType _gameType;

        public NameInputWindow(GameType gameType)
        {
            InitializeComponent();
            _gameType = gameType;

            // Disable P2 input if it's PVC game mode
            if (_gameType.Equals(GameType.PVC))
            {
                txtBoxNamePlayer2.IsEnabled = false;
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;

            textBox.Text = string.Empty;
            textBox.GotFocus -= TextBox_GotFocus;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;

            if (textBox.Text == string.Empty)
            {
                textBox.Text = _inputPlaceholder;
                textBox.GotFocus += TextBox_GotFocus;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            var player1 = new Player(1, (!txtBoxNamePlayer1.Text.Equals(_inputPlaceholder)) ? txtBoxNamePlayer1.Text : _defaultNameP1, _iconP1, _colorP1);
            var player2 = new Player(2, (!txtBoxNamePlayer2.Text.Equals(_inputPlaceholder)) ? txtBoxNamePlayer2.Text : _defaultNameP2, _iconP2, _colorP2);

            if(player1.Name != player2.Name)
            {
                var gameWindow = new GameWindow(player1, player2, _gameType);
                gameWindow.Show();
                
                this.Close();
            }
            else
            {
                MessageBox.Show("Usernames should be different.\nPlease, change one of them.");
            }
        }
    }
}
