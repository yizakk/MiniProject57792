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
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for UpTester.xaml
    /// </summary>
    public partial class UpTester : UserControl
    {
        BL.IBL bL = BL.BlFactory.GetBL();
        Tester TempTtester;
        public UpTester()
        {
            InitializeComponent();

            comboBox.ItemsSource = bL.GetTesterIdList();


            car_typeComboBox.ItemsSource = Enum.GetValues(typeof(CarType));
            genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           string a = comboBox.SelectedValue.ToString();
            TempTtester = bL.FindTester(a);
            grid1.DataContext = TempTtester;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
                try
                {
                bL.UpdateTester(TempTtester);
                }
                catch (Exceptions a)
                {
                    MessageBox.Show(a._message);
                return;
                }

                    MessageBox.Show("העידכון הסתיים בהצלחה");


        }
    }
}
