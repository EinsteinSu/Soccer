using System.Data.Entity;
using Soccer.DataAccess;

namespace Soccer.Views
{
    /// <summary>
    ///     Interaction logic for TeamAndPlayer.xaml
    /// </summary>
    public partial class TeamAndPlayer
    {
        private SoccerContext _context = new SoccerContext();
        public TeamAndPlayer()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource viewSource =
                ((System.Windows.Data.CollectionViewSource)(this.FindResource("teamViewSource")));

            // Load is an extension method on IQueryable, 
            // defined in the System.Data.Entity namespace.
            // This method enumerates the results of the query, 
            // similar to ToList but without creating a list.
            // When used with Linq to Entities this method 
            // creates entity objects and adds them to the context.
            _context.Teams.Load();

            // After the data is loaded call the DbSet<T>.Local property 
            // to use the DbSet<T> as a binding source.
            viewSource.Source = _context.Teams.Local;
        }
    }
}