using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Soccer.Business;
using Soccer.Common;
using Soccer.DataAccess;
using Soccer.ViewModels.Data;
using Soccer.Views;

namespace Soccer.ViewModels
{
    [POCOViewModel]
    public class MainWindowViewModel : ViewModelBase
    {
        private ContentPage _contentPage;
        private DelegateCommand<string> _featureClickCommand;
        private readonly FunctionClickManager _functionClickManager = new FunctionClickManager(InitializeContent);
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

        public void Click(string feature)
        {
            //todo: implement the show dialog window method 
            ContentPage = _functionClickManager.FindPage(feature);
        }

        public string Status { get; set; }

        public void MenuChanged(Group menu)
        {
            ContentPage = _functionClickManager.FindPage(menu);
        }

        public static FrameworkElement InitializeContent(string name)
        {
            switch (name)
            {
                case FunctionClickManager.TeamsAndPlayersName:var vm = new TeamAndPlayerVm();
                    vm.Refresh();
                    return new TeamAndPlayer { DataContext = vm };
                default:
                    return new TextBlock { Text = name };
            }
        }
    }

}