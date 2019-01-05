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
        BL.IBL bl = BL.BlFactory.GetBL();
        Tester TempTester;
        public UpTester()
        {
            InitializeComponent();

            var sourceList = bl.GetTesterIdList();
            if (!sourceList.Any())
            {
                MessageBox.Show("אין בוחנים במאגר", "", MessageBoxButton.OK, MessageBoxImage.None,
                    MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                button.IsEnabled = false;
                comboBox.IsEnabled = false;
            }
             comboBox.ItemsSource = sourceList;
            button.IsEnabled = false;
            car_typeComboBox.ItemsSource = Enum.GetValues(typeof(CarType));
            genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
           

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            button.IsEnabled = true;

            string a = comboBox.SelectedValue.ToString().Split(' ')[0];
            TempTester = bl.FindTester(a);
            grid1.DataContext = TempTester;
            AddressGrid.DataContext = TempTester.Address;

            int i = 0, j=0, k=1;
            foreach (var item in ScheduleGrid.Children)
            {
                if (item is CheckBox)
                {
                    var value = item as CheckBox;
                    if (value.Name == "checkBox" + k++)
                    {
                        Binding binding = new Binding();
                        binding.Source = TempTester.m_WorkSchedule[i,j++];
                        binding.Mode = BindingMode.TwoWay;
                       // SetBinding(value.IsChecked, binding);
                        //value.IsChecked= SetBinding(binding,binding);
                        //value.IsChecked = TempTester.WorkSchedule(i, j++);
                        if (j == 7)
                        {
                            j = 0;
                            i++;
                        }
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int i = 0, j = 0, k = 1;
            foreach (var item in ScheduleGrid.Children)
            {
                if (item is CheckBox)
                {
                    var value = item as CheckBox;
                    if (value.Name == "checkBox" + k++)
                    {
                        TempTester.WorkSchedule(i, j++, value.IsChecked);
                        if (j == 7)
                        {
                            j = 0;
                            i++;
                        }
                    }
                }
            }

            try
            {
                bl.UpdateTester(TempTester);
            }
            catch (Exceptions a)
            {
                MessageBox.Show(a._message);
                return;
            }

            MessageBox.Show("העידכון הסתיים בהצלחה");


        }

        private void CheckBoxMain1_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
