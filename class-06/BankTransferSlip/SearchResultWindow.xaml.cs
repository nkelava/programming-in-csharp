using System.Windows;
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

        public void ShowSearchResult(BaseUser payer, BaseUser recipient)
        {
            var dbHandler = new DatabaseHandler();
            var dataTable = dbHandler.GetDataTable(payer, recipient);

            if (dataTable.Rows.Count > 0)
            {
                dataGridSearch.ItemsSource = dataTable.DefaultView;
                Show();
            }
            else
            {
                MessageBox.Show("There are no records to show.\n\n" +
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
