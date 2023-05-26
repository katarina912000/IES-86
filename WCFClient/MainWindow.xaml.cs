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

namespace NMSClient
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

        private void GetExtent_Click(object sender, RoutedEventArgs e)
        {
            GetExtentValues getExtentValues = new GetExtentValues();
            getExtentValues.ShowDialog();
        }

        private void GetValues_Click(object sender, RoutedEventArgs e)
        {
            GetValues getValues = new GetValues();
            getValues.ShowDialog();
        }

        private void GetRelated_Click(object sender, RoutedEventArgs e)
        {
            GetRelatedValues getRelatedValues = new GetRelatedValues();
            getRelatedValues.ShowDialog();
        }
    }
}
