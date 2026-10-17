using System;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
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

        private void TextBox_GotFocus(object sender, FocusChangedEventArgs e)
        {
            var textBox = (TextBox)sender;

            textBox.Text = string.Empty;
            textBox.GotFocus -= TextBox_GotFocus;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;

            if (string.IsNullOrEmpty(textBox.Text))
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
            end = Math.Min(end, text.Length);
            return text.Substring(start, end).ToUpper() + text.Substring(end);
        }

        private async void btnProcess_Click(object sender, RoutedEventArgs e)
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
                            decimal.Parse(FixFormat(txtBoxAmount.Text), CultureInfo.InvariantCulture),
                            ToUpperSubstring(txtBoxPayerIBAN.Text, 0, 2),
                            ToUpperSubstring(txtBoxRecipientIBAN.Text, 0, 2),   
                            ToUpperSubstring(txtBoxModel.Text, 0, 2),
                            txtBoxReferenceNumber.Text);
                
                newSlip.CheckFields();
                SaveRecordToDatabase(newSlip);
                
                await MessageBox.Show(this, "SUCCESS!!!\n\nYour transfer was successful.");
                btnClear_Click(sender, e);
            }
            catch (Exception exception) when (exception is FormatException || exception is OverflowException)
            {
                await MessageBox.Show(this, "Amount error. Please make sure you only enter digits.");
            }
            catch (CurrencyFormatException currencyException)
            {
                await MessageBox.Show(this, currencyException.Message);
            }
            catch (IBANFormatException IBANException)
            {
                await MessageBox.Show(this, IBANException.Message);
            }
            catch (ModelFormatException modelException)
            {
                await MessageBox.Show(this, modelException.Message);
            }
            catch (ReferenceNumberFormatException referenceNumberException)
            {
                await MessageBox.Show(this, referenceNumberException.Message);
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

            searchWindow.ShowSearchResult(this, payer, recipient);
        }
    }
}
