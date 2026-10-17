using Avalonia.Controls;
using Avalonia.Interactivity;
using BankTransferSlip.Entities;
using BankTransferSlip.Services;

namespace BankTransferSlip
{
    public partial class SearchResultWindow : Window
    {
        public SearchResultWindow()
        {
            InitializeComponent();
        }

        public async void ShowSearchResult(Window owner, BaseUser payer, BaseUser recipient)
        {
            var dbHandler = new DatabaseHandler();
            var userRecords = dbHandler.GetUserRecords(payer, recipient);

            if (userRecords.Count > 0)
            {
                dataGridSearch.ItemsSource = userRecords;
                Show();
            }
            else
            {
                await MessageBox.Show(owner, "There are no records to show.\n\n" +
                                             "1. Make sure you entered the payer's information, the recipient's information or both.\n" +
                                             "2. Make sure the entered information is correct.");
            }
            
            dbHandler.CloseDatabaseConnection();
        }

        private void btnCloseSearch_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
