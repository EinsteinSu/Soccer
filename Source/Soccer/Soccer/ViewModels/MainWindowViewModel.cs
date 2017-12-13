using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Soccer.DataAccess;

namespace Soccer.ViewModels
{
    [POCOViewModel]
    public class MainWindowViewModel : ViewModelBase
    {
        protected readonly List<ContentPage> Pages = new List<ContentPage>();

        public MainWindowViewModel()
        {
            Status = "No Game selected.";
        }

        public string Title { get; set; }

        public Schedule CurrentSchedule { get; set; }

        public ContentPage ContentPage { get; set; }

        public string Status { get; set; }

        public void MenuChanged(Menu menu)
        {
            //todo: 1 if has no default page, create; 2 if has multiplies page created, show the recent page by index.
            //MessageBox.Show(menu.ToString());
            var page = new ContentPage
            {
                Name = menu.ToString(),
                Content = new TextBox()
            };
            ContentPage = page;
        }
    }

    [POCOViewModel]
    public class ContentPage
    {
        public string Name { get; set; }

        public Menu Group { get; set; }

        public FrameworkElement Content { get; set; }

        public int Index { get; set; }

        public bool RelatedGame { get; set; }
    }

    public enum Menu
    {
        File,
        DataManagement,
        Display,
        GameData,
        Reports,
        About
    }
}