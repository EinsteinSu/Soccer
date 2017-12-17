using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Grid;
using Soccer.ViewModels;

namespace Soccer.Views
{
    /// <summary>
    ///     Interaction logic for TeamAndPlayer.xaml
    /// </summary>
    public partial class TeamAndPlayer
    {
        public TeamAndPlayer()
        {
            InitializeComponent();
        }
        private void View_OnRowUpdated(object sender, RowEventArgs e)
        {
            var context = this.DataContext as TeamAndPlayerViewModel;
            context?.Save();
        }
    }
}