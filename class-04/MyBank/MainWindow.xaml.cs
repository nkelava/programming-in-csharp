using System;
using System.Windows;
using System.Windows.Controls;


namespace MyBank
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            accountTypeComboBox.ItemsSource = Enum.GetValues(typeof(AccountType));
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
                textBox.Text = textBox.Tag.ToString();
                textBox.GotFocus += TextBox_GotFocus;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newAccount = new Account(
                    nameTextBox.Text,
                    lastNameTextBox.Text,
                    (DateTime)birtdayDatePicker.SelectedDate,
                    long.Parse(oibTextBox.Text),
                    (maleRadioButton.IsChecked == true) ? SexType.Male : SexType.Female,
                    BankAccounts.GenerateAccountNumber(),
                    (AccountType)accountTypeComboBox.SelectedItem,
                    decimal.Parse(accountBalanceTextBox.Text));

                BankAccounts.Add(newAccount);
                MessageBox.Show("The user has been successfully added.");
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Please, enter all information asked in the form except account number.");
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Tuple<bool, Account> searchResult = BankAccounts.SearchAccount(long.Parse(oibTextBox.Text));

                if (searchResult.Item1)
                {
                    Account account = searchResult.Item2;

                    nameTextBox.Text = account.FirstName;
                    lastNameTextBox.Text = account.LastName;
                    oibTextBox.Text = account.OIB.ToString();
                    birtdayDatePicker.SelectedDate = account.Birthday;

                    if (account.Sex == SexType.Male)
                    {
                        SetSexRadioButton(true, false);
                    }
                    else
                    {
                        SetSexRadioButton(false, true);
                    }

                    accountTypeComboBox.SelectedItem = account.Type;
                    accountBalanceTextBox.Text = account.Balance.ToString();
                    accountNumberTextBox.Text = account.ID.ToString();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please, enter users OIB for search.");
            }
        }

        private void SetSexRadioButton(bool maleStatus, bool femaleStatus)
        {
            maleRadioButton.IsChecked = maleStatus;
            femaleRadioButton.IsChecked = femaleStatus;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            foreach(Control child in appGrid.Children)
            {
                var childType = child.GetType();

                if (childType == typeof(TextBox))
                {
                    TextBox textBox = (TextBox)child;
                    textBox.Text = textBox.Tag.ToString();
                    textBox.GotFocus += TextBox_GotFocus;
                    continue;
                }

                if(childType == typeof(DatePicker))
                {
                    ((DatePicker)child).SelectedDate = null;
                    continue;
                }

                if(childType == typeof(RadioButton))
                {
                    ((RadioButton)child).IsChecked = false;
                    continue;
                }
                
                if(childType == typeof(ComboBox))
                {
                    ((ComboBox)child).SelectedItem = null;
                }
            }
        }
    }
}
