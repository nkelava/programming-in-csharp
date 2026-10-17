using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace TicTacToe
{
    static class MessageBox
    {
        public static Task Show(Window owner, string text)
        {
            var okButton = new Button { Content = "OK", Width = 75, HorizontalAlignment = HorizontalAlignment.Right };
            var dialog = new Window
            {
                Title = "",
                SizeToContent = SizeToContent.WidthAndHeight,
                CanResize = false,
                ShowInTaskbar = false,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Content = new StackPanel
                {
                    Margin = new Thickness(20),
                    Spacing = 20,
                    Children =
                    {
                        new TextBlock { Text = text, MaxWidth = 400, TextWrapping = TextWrapping.Wrap },
                        okButton
                    }
                }
            };

            okButton.Click += (sender, e) => dialog.Close();
            return dialog.ShowDialog(owner);
        }
    }
}
