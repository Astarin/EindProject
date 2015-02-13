using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EindProjectBusinessModels;

namespace tmptest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource teamViewSource = (
                (System.Windows.Data.CollectionViewSource)(this.FindResource("teamViewSource")));
            // Load data by setting the CollectionViewSource.Source property:

            DbEindproject db = new DbEindproject();
            
            var x = from t in db.Teams
                    select t;

            teamViewSource.Source = x;

            System.Windows.Data.CollectionViewSource werknemerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("werknemerViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // werknemerViewSource.Source = [generic data source]
        }
    }
}
