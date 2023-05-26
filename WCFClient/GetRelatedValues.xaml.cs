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
using System.Windows.Shapes;

namespace NMSClient
{
    /// <summary>
    /// Interaction logic for GetRelatedValues.xaml
    /// </summary>
    public partial class GetRelatedValues : Window
    {
        TestGda testGda = new TestGda();
        public GetRelatedValues()
        {
            InitializeComponent();
            List<long> gids = testGda.GetAllGIDS();
            List<string> stringsGIDS = new List<string>();
            foreach (long gid in gids)
            {
                stringsGIDS.Add("0x" + gid.ToString("x16"));
            }
            ComboBoxGIDS.ItemsSource = stringsGIDS;
            List<DMSType> dMSTypes = testGda.GetALLDMSTypes();
            List<string> stringsTypes = new List<string>();
            foreach (DMSType type in dMSTypes)
            {
                if (type == DMSType.MASK_TYPE)
                    continue;
                else
                    stringsTypes.Add(type + "");
            }
            ComboBoxTyps.ItemsSource = stringsTypes;
        }

        private void GetValues_Click(object sender, RoutedEventArgs e)
        {
            Opis.Selection.Text = "";
            string gid = ComboBoxGIDS.SelectedItem.ToString().Split('x')[1];
            long gidNum = Convert.ToInt64(Int64.Parse(gid, System.Globalization.NumberStyles.HexNumber));
            var atributs = ComboBoxAtribut.SelectedItems;
            List<ModelCode> modelCodes = new List<ModelCode>();
            foreach (var modelCode in atributs)
            {
                ModelCode mc;
                if (Enum.TryParse(modelCode+"",out mc)) {

                    modelCodes.Add(mc);
                }
            }
            ModelCode referenca;
            Enum.TryParse(ComboBoxReference.SelectedItem + "", out referenca);
            DMSType tip;
            Enum.TryParse(ComboBoxTyps.SelectedItem + "", out tip);
            string opis = testGda.GetRelatedValues(gidNum, modelCodes, referenca, tip);
            Opis.Selection.Text = opis;
        }

        private void ComboBoxGIDS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string gid = ComboBoxGIDS.SelectedItem.ToString().Split('x')[1];
            long gidNum = Convert.ToInt64(Int64.Parse(gid, System.Globalization.NumberStyles.HexNumber));
            var modelCodes = testGda.GetReferences(gidNum);
            List<string> strings=new List<string>();
            foreach(var mc in modelCodes)
            {
                strings.Add(mc+"");
            }
            ComboBoxReference.ItemsSource = strings;

        }

        private void ComboBoxTyps_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string gid = ComboBoxGIDS.SelectedItem.ToString().Split('x')[1];
            long gidNum = Convert.ToInt64(Int64.Parse(gid, System.Globalization.NumberStyles.HexNumber));
            string tip = ComboBoxTyps.SelectedItem.ToString();
            DMSType dMSType;
            Enum.TryParse(tip,out dMSType);
            
            List<ModelCode> modelCodes = testGda.GetModelCodesForDMSType(dMSType);
            List<string> strings = new List<string>();
            foreach (ModelCode modelCode in modelCodes)
            {
                strings.Add(modelCode + "");
            }
            ComboBoxAtribut.ItemsSource = strings;
        }

        private void ComboBoxReference_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
