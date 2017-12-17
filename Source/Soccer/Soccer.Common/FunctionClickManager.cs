using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Soccer.Common
{
    public class FunctionClickManager
    {
        private readonly Func<string, FrameworkElement> _initializeContent;
        private readonly ObservableCollection<ContentPage> _pages;
        public const string TeamsAndPlayersName = "TeamsAndPlayers";
        public const string ScheduleName = "Schedule";
        public const string DisplayGameBeforeName = "DisplayGameBefore";
        public const string DisplayGameName = "DisplayGame";
        public const string DisplayGameAfterName = "DisplayGameAfter";
        public const string GameDataEditName = "GameDataEdit";
        public FunctionClickManager(Func<string, FrameworkElement> initializeContent)
        {
            _initializeContent = initializeContent;
            LicenseController controller = new LicenseController();
            controller.Validated(new MacAddressSerialNumber());


            #region create the groups and functions
            _pages = new ObservableCollection<ContentPage>
            {
                new ContentPage
                {
                    Group = Group.DataManagement,
                    Name = TeamsAndPlayersName,
                    IsDefault = true,
                    Index = 0
                },
                new ContentPage
                {
                    Group = Group.DataManagement,
                    Name = ScheduleName,
                    Index = 999
                },
                new ContentPage
                {
                    Group = Group.Display,
                    Name = DisplayGameName,
                    IsDefault = true,
                    Index = 0
                },
                new ContentPage
                {
                    Group = Group.Display,
                    Name = DisplayGameBeforeName,
                    Index = 999
                },
                new ContentPage
                {
                    Group = Group.Display,
                    Name = DisplayGameAfterName,
                    Index = 999
                },
                new ContentPage
                {
                    Group = Group.GameData,
                    Name = GameDataEditName,
                    IsDefault = true,
                    Index = 0
                }
            };
            #endregion
        }

        public ContentPage FindPage(Group group)
        {
            var page = _pages.OrderBy(o => o.Index).FirstOrDefault(f => f.Group == group);
            if (page == null)
                throw new Exception("No content page can be found");
            CreatePageContent(page);
            return page;
        }

        public ContentPage FindPage(string functionName)
        {
            var page = _pages.OrderBy(o => o.Index).FirstOrDefault(f => f.Name.Equals(functionName));
            if (page == null)
                throw new Exception("Not content page can be found");
            page.Index = 0;
            foreach (var p in GetAnotherPage(page))
            {
                p.Index = 999;
            }
            CreatePageContent(page);
            return page;
        }

        public IEnumerable<ContentPage> GetAnotherPage(ContentPage page)
        {
            return _pages.Where(w => !w.Name.Equals(page.Name) && w.Group == page.Group);
        }

        protected void CreatePageContent(ContentPage page)
        {
            if (!page.Initialized)
            {
               page.Content = _initializeContent?.Invoke(page.Name);
            }
        }
    }
}