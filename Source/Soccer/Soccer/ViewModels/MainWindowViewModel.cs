using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Soccer.DataAccess;
using Soccer.Views;

namespace Soccer.ViewModels
{
    [POCOViewModel]
    public class MainWindowViewModel : ViewModelBase
    {
        protected readonly List<ContentPage> Pages = new List<ContentPage>();
        private ContentPage _contentPage;
        private DelegateCommand<string> _featureClickCommand;

        public DelegateCommand<string> FeatureClickCommand
        {
            get => _featureClickCommand;
            set => SetProperty(ref _featureClickCommand, value, "FeatureClickCommand");
        }

        public MainWindowViewModel()
        {
            Status = "No Game selected.";
            _featureClickCommand = new DelegateCommand<string>(Click);
        }
        public string Title { get; set; }

        public Schedule CurrentSchedule { get; set; }

        public ContentPage ContentPage
        {
            get => _contentPage;
            set => SetProperty(ref _contentPage, value, "ContentPage");
        }

        public void Click(string feature){
            MessageBox.Show(feature);
        }

        public string Status { get; set; }

        public void MenuChanged(Menu menu)
        {
            //todo: 1 if has no default page, create; 2 if has multiplies page created, show the recent page by index.
            var page = new ContentPage();
            page.Name = menu.ToString();
            page.Group = menu;

            switch (menu)
            {
                case Menu.DataManagement:
                    page.Content = new TeamAndPlayer();
                    page.Content.DataContext = new TeamAndPlayerViewModel(); //input Iprogress
                    break;
                default:
                    page.Content = new TextBlock { Text = "Click me" };
                    break;
            }
            ContentPage = page;
        }}

    [POCOViewModel]
    public class ContentPage : ViewModelBase
    {
        private FrameworkElement _content; public string Name { get; set; }

        public Menu Group { get; set; }

        public FrameworkElement Content
        {
            get => _content;
            set => SetProperty(ref _content, value, "Content");
        }

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