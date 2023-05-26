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
    /// Interaction logic for GetExtentValues.xaml
    /// </summary>
    public partial class GetExtentValues : Window
    {
        TestGda testGda=new TestGda();
        public GetExtentValues()
        {
            InitializeComponent();
            List<DMSType> dMSTypes=testGda.GetALLDMSTypes();
            List<string> stringsTypes = new List<string>();
            foreach(DMSType type in dMSTypes)
            {
                if (type == DMSType.MASK_TYPE)
                    continue;
                else
                    stringsTypes.Add(type+"");
            }
            ComboBoxTypes.ItemsSource=stringsTypes;
        }

        private void GetValues_Click(object sender, RoutedEventArgs e)
        {
            Opis.Selection.Text = "";
            var items = ListBoxAtributs.SelectedItems;
            List<ModelCode> modelCodes = new List<ModelCode>();
            foreach (var i in items)
            {
                ModelCode mc;
                if(Enum.TryParse(i+"",out mc))
                {
                    modelCodes.Add(mc);
                }
            }
            string type = ComboBoxTypes.SelectedItem.ToString();
            DMSType dMSType;
            if (Enum.TryParse(type, out dMSType))
            {
                string opis = testGda.GetExtentValues(dMSType, modelCodes);
                Opis.Selection.Text = opis;
            }
        }

        private void ComboBoxTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string type = ComboBoxTypes.SelectedItem.ToString();
            List<ModelCode> modelCodes = new List<ModelCode>();
            DMSType dMSType;
            if (Enum.TryParse(type, out dMSType))
            {
                modelCodes = testGda.GetModelCodesForDMSType(dMSType);
            }
            List<string> stringsTypes = new List<string>();
            foreach(var mc in modelCodes)
            {
                stringsTypes.Add(mc + "");
            }
            ListBoxAtributs.ItemsSource= stringsTypes;
        }
    }
}
