using System;
using DevExpress.Xpf.Ribbon;
using Soccer.ViewModels;

namespace Soccer
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void RibbonControl_OnSelectedPageChanged(object sender, RibbonPropertyChangedEventArgs e)
        {
            if (!(DataContext is MainWindowViewModel context))
            {
                Console.WriteLine("wow");
            }
            else
            {
                if (sender is RibbonControl content){
                    var currentPage = content.SelectedPage;
                    if (currentPage?.Tag != null)
                        context.MenuChanged((Menu) int.Parse(currentPage.Tag.ToString()));
                }
            }
        }
    }
}