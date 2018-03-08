using System;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Bars;
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

        protected TeamAndPlayerViewModel ViewModel => DataContext as TeamAndPlayerViewModel;

        private void View_OnRowUpdated(object sender, RowEventArgs e)
        {
            
        }

        private void BarItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
         
        }

        private void BarItem_Player_OnItemClick(object sender, ItemClickEventArgs e)
        {
           
        }

        private void View_Player_OnRowUpdated(object sender, RowEventArgs e)
        {
            
        }

        private void PlayerView_OnInitNewRow(object sender, InitNewRowEventArgs e)
        {
            //var context = this.DataContext as TeamAndPlayerViewModel;
            //if (context.Collection.CurrentItem == null)
            //{
            //    MessageBox.Show("Please select an item of team.");
            //    e.Handled = true;
            //}
            //else
            //{
            //    playerGrid.SetCellValue(e.RowHandle, "TeamId", context.Collection.CurrentItem.Id);
            //}
        }
    }
}