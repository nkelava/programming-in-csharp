using System;
using System.Windows;
using System.Windows.Controls;
using BankTransferSlip.Exceptions;
using BankTransferSlip.Entities;
using BankTransferSlip.Services;


namespace BankTransferSlip
{
    // **********************************************
    //
    // 1. Add conversion to a single currency for the database
    // 2. Add lowercase data validation
    //
    // **********************************************
    public partial class MainWindow : Window
    {
        const string _namePlaceholder = "Enter name...";
        const string _addressPlaceholder = "Enter address...";
        
        public MainWindow()
        {
            InitializeComponent();
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

        private static string FixFormat(string amount)
        {
            return (amount.Contains(",")) ? amount.Replace(",", ".") : amount;
        }

        private string ToUpperSubstring(string text, int start, int end)
        {
            return text.Substring(start, end).ToUpper() + text.Substring(end);
        }

        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newSlip = new Slip(
                            txtBoxPayerName.Text,
                            txtBoxPayerAddress.Text,
                            txtBoxRecipientName.Text,
                            txtBoxRecipientAddress.Text,
                            chkBoxUrgent.IsChecked.Value,
                            txtBoxCurrency.Text.ToUpper(),
                            decimal.Parse(FixFormat(txtBoxAmount.Text)),
                            ToUpperSubstring(txtBoxPayerIBAN.Text, 0, 2),
                            ToUpperSubstring(txtBoxRecipientIBAN.Text, 0, 2),   
                            ToUpperSubstring(txtBoxModel.Text, 0, 2),
                            txtBoxReferenceNumber.Text);
                
                newSlip.CheckFields();
                SaveRecordToDatabase(newSlip);
                
                MessageBox.Show("SUCCESS!!!\n\nYour transfer was successful.");
                btnClear_Click(sender, e);
            }
            catch (FormatException)
            {
                MessageBox.Show("Amount error. Please make sure you only enter digits.");
            }
            catch (CurrencyFormatException currencyException)
            {
                MessageBox.Show(currencyException.Message);
            }
            catch (IBANFormatException IBANException)
            {
                MessageBox.Show(IBANException.Message);
            }
            catch (ModelFormatException modelException)
            {
                MessageBox.Show(modelException.Message);
            }
            catch (ReferenceNumberFormatException referenceNumberException)
            {
                MessageBox.Show(referenceNumberException.Message);
            }
        }

        private void SaveRecordToDatabase(Slip slip)
        {
            var dbHandler = new DatabaseHandler();

            dbHandler.SaveRecord(slip);
            dbHandler.CloseDatabaseConnection();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            foreach (var child in gridSlip.Children)
            {
                if (child.GetType() == typeof(TextBox))
                {
                    ((TextBox)child).Text = "";
                    txtBoxPayerName.Text = _namePlaceholder;
                    txtBoxPayerAddress.Text = _addressPlaceholder;
                    txtBoxRecipientName.Text = _namePlaceholder;
                    txtBoxRecipientAddress.Text = _addressPlaceholder;
                    continue;
                }
                if (child.GetType() == typeof(CheckBox))
                {
                    ((CheckBox)child).IsChecked = false;
                }
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var searchWindow = new SearchResultWindow();
            var payer = new BaseUser(txtBoxPayerName.Text, txtBoxPayerAddress.Text);
            var recipient = new BaseUser(txtBoxRecipientName.Text, txtBoxRecipientAddress.Text);

            searchWindow.ShowSearchResult(payer, recipient);
        }
    }
}
