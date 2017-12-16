using System.Windows;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Soccer.ViewModels;

namespace Soccer.Common
{
    [POCOViewModel]
    public class ContentPage : ViewModelBase
    {
        private FrameworkElement _content;

        public string Name { get; set; }

        public Menu Group { get; set; }

        public FrameworkElement Content
        {
            get => _content;
            set => SetProperty(ref _content, value, "Content");
        }

        public int Index { get; set; }

        public bool IsDefault { get; set; }

        public bool RelatedGame { get; set; }
    }
}