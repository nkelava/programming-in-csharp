using System;
using System.Windows;
using System.Windows.Controls;


namespace CurrencyConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private decimal _valueTo;
        public MainWindow()
        {
            InitializeComponent();
            txtBoxTo.Text = "0";
            GetTextBoxValues();
            SetComboBoxItemsSource();
        }

        private void GetTextBoxValues()
        {
            try
            {
                _valueTo = decimal.Parse(txtBoxTo.Text);
            }
            catch
            {
                _valueTo = 0;
            }
        }

        private void SetComboBoxItemsSource()
        {
            ConversionHandler conversionHandler = new ConversionHandler();

            cmbBoxFrom.ItemsSource = conversionHandler.CurrencyList;
            cmbBoxTo.ItemsSource = conversionHandler.CurrencyList;
        }
        private void txtBoxFrom_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtBoxTo.Text != null)
            {
                _valueTo = ConversionHandler.Calculate(txtBoxFrom.Text, (Currency)cmbBoxFrom.SelectedItem, (Currency)cmbBoxTo.SelectedItem);
                txtBoxTo.Text = _valueTo.ToString();
            }
        }
        private void FromDropDownClosed(object sender, EventArgs e)
        {
            txtBoxFrom_TextChanged(sender, null);
        }
        private void ToDropDownClosed(object sender, EventArgs e)
        {
            txtBoxFrom_TextChanged(sender, null);
        }
    }
}
