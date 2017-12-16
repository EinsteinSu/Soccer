using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Soccer.Common
{
    public class ContentPage
    {
        public string Name { get; set; }

        public Group Group { get; set; }

        public FrameworkElement Content { get; set; }

        public bool Initialized => Content != null;

        public int Index { get; set; }

        public bool RelatedGame { get; set; }

        public bool IsDefault { get; set; }
    }

    public enum Group
    {
        File,
        DataManagement,
        Display,
        GameData,
        Reports,
        About
    }
}
