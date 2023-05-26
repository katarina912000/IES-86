using FTN.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private List<long> combo1 = new List<long>();
        private long gid1;
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            Combo1 = GetAllGids();
        }

        public List<long> Combo1
        {
            get
            {
                return Combo1;
            }
            set
            {
                Combo1 = value;
                OnPropertyChanged("ComboGid1");
            }

        }

        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }
        public List<long> GetAllGids()
        {
            //treba da dobavim sve gidove od svih instanci
            ModelResourcesDesc modelResourcesDesc = new ModelResourcesDesc();
            List<ModelCode> properties = new List<ModelCode>();
            // properties.Add(ModelCode.IDOBJ_GID);
            //modelResourcesDesc.GetPropertyIds()
           // Array dmsovci[] = Enum.GetValues(typeof(DMSType));

            List<long> ids = new List<long>();
            // try
            // {
            //     foreach(ModelCode code in properties)
            //     {
            //         ids.Add();
            //     }

            // }catch(Exception e)
            // {
            //     MessageBox.Show(e.Message);
            // }

            //int iteratorId = 0;
            //int numberOfResources = 1000;
            //DMSType currType = 0;
            //properties.Add(ModelCode.IDOBJ_GID);
            //try
            //{
            //    foreach (DMSType type in Enum.GetValues(typeof(DMSType)))
            //    {
            //        currType = type;

            //        if (type != DMSType.MASK_TYPE)
            //        {
            //           // iteratorId = GDAQueryProxy.GetExtentValues(modelResourcesDesc.GetModelCodeFromType(type), properties);
            //           // int count = GDAQueryProxy.IteratorResourcesLeft(iteratorId);

            //            //while (count > 0)
            //            //{
            //            //   // List<ResourceDescription> rds = GDAQueryProxy.IteratorNext(numberOfResources, iteratorId);

            //            //    for (int i = 0; i < rds.Count; i++)
            //            //    {
            //            //        ids.Add(rds[i].Id);
            //            //    }

            //            //   // count = GDAQueryProxy.IteratorResourcesLeft(iteratorId);
            //            //}

            //           // bool ok = GDAQueryProxy.IteratorClose(iteratorId);

            //        }
            //    }
            //}
            //catch (Exception)
            //{
            //    throw;
            //}

            return ids;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListBoxProperties_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged_3(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged_4(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
