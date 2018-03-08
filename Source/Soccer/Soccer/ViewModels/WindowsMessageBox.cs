using System.Windows;
using Supeng.Wpf.Common.Interfaces;

namespace Soccer.ViewModels
{
    public class WindowsMessageBox : IMessageBox
    {
        public bool ShowMessage(string content, string subject)
        {
            return MessageBox.Show(content, subject, MessageBoxButton.YesNo) == MessageBoxResult.Yes;
        }
    }
}