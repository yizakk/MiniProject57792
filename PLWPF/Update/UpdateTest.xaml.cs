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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for UpdateTest.xaml
    /// </summary>
    public partial class UpdateTest : UserControl
    {
        BE.Address Address = new BE.Address();
        BE.Test TestItem;
        BL.IBL bl = BL.BlFactory.GetBL();
        public UpdateTest()
        {
            InitializeComponent();

            var sourceList = bl.GetTestsIdList();
            if (!sourceList.Any())
            {
                MessageBox.Show("אין טסטים במאגר", "", MessageBoxButton.OK, MessageBoxImage.None,
                    MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                button.IsEnabled = false;
                comboBox.IsEnabled = false;
            }
            comboBox.ItemsSource = sourceList;
            grid2.DataContext = Address;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox.SelectedIndex != -1)
            {
                TestItem = bl.FindTest(int.Parse(comboBox.SelectedValue.ToString()));
                grid1.DataContext = TestItem;
                ParametersGrid.DataContext = TestItem.Parameters;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                TestItem.BeginAddress = Address;


                string temp = cityTextBox.Text + " " + streetTextBox.Text + " " + buildingNumberTextBox;
               TestItem.adress = temp;
                bl.UpdateTest(TestItem);
            }
            catch (BE.Exceptions cat)
            {
                MessageBox.Show(cat._message);
            }
            int choice = (int)MessageBox.Show("המבחן עודכן בהצלחה, האם ברצונך לעדכן עוד מבחן?", "", MessageBoxButton.YesNo,
                                MessageBoxImage.Asterisk, MessageBoxResult.None, MessageBoxOptions.RtlReading);
            if (choice == 6)
            {
                Data.MainUserControl = new UpdateTest();
            }
            else
                Data.MainUserControl = new HomePanel();

        }
    }
}
