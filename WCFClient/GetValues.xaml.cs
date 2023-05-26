using FTN.Common;
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
    /// Interaction logic for GetValues.xaml
    /// </summary>
    public partial class GetValues : Window
    {
        TestGda testGda;
        public GetValues()
        {
            InitializeComponent();
            testGda = new TestGda();
            List<long> gids = testGda.GetAllGIDS();
            List<string> stringsGIDS = new List<string>();
            foreach (long gid in gids)
            {
                stringsGIDS.Add("0x" + gid.ToString("x16"));
            }
            ComboBoxGIDS.ItemsSource = stringsGIDS;
        }

        private void GetValues_Click(object sender, RoutedEventArgs e)
        {
            Opis.Selection.Text="";
            string gid = ComboBoxGIDS.SelectedItem.ToString().Split('x')[1];
            long gidNum = Convert.ToInt64(Int64.Parse(gid, System.Globalization.NumberStyles.HexNumber));

            var selectedOptions = ComboBoxAtributs.SelectedItems;
            List<ModelCode> modelCodes = new List<ModelCode>();
            foreach (string s in selectedOptions)
            {
                ModelCode modelCode;
                if (Enum.TryParse(s, out modelCode))
                {
                    modelCodes.Add(modelCode);
                }
            }
            string ispisEntitet = testGda.GetValues(gidNum,modelCodes);
            Opis.Selection.Text = ispisEntitet;

        }


        private void ComboBoxGIDS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string gid = ComboBoxGIDS.SelectedItem.ToString().Split('x')[1];
            long gidNum = Convert.ToInt64(Int64.Parse(gid, System.Globalization.NumberStyles.HexNumber));
            List<ModelCode> modelCodes=testGda.GetModelCodesForGID(gidNum);
            List<string> strings = new List<string>(); 
            foreach(ModelCode modelCode in modelCodes)
            {
                strings.Add(modelCode + "");
            }
           ComboBoxAtributs.ItemsSource=strings;
        }

        private void ComboBoxAtributs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedOptions = ComboBoxAtributs.SelectedItems;
        }
    }
}
