using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        public static readonly DependencyProperty ChekedProperty =
        DependencyProperty.Register("Cheked", typeof(Boolean), typeof(UpTester));


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
            car_typeComboBox.ItemsSource = Enum.GetValues(typeof(CarType));
            genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));

            if(Data.UserType == Data.Usertype.בוחן)
            {
                TempTester = bl.FindTester(Data.UserID);
                grid1.DataContext = TempTester;
                comboBox.Visibility = Visibility.Collapsed;
                textBlock.Visibility = Visibility.Collapsed;
                return;
            }
            comboBox.ItemsSource = sourceList;
            button.IsEnabled = false;

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            button.IsEnabled = true;
            checkBoxMain.IsChecked = false;
            checkBoxMain1.IsChecked = false;
            checkBoxMain2.IsChecked = false;
            checkBoxMain3.IsChecked = false;
            checkBoxMain4.IsChecked = false;
            checkBoxMain5.IsChecked = false;

            if (comboBox.SelectedIndex != -1)
            {
                try
                {

                    string a = comboBox.SelectedValue.ToString().Split(' ')[0];
                    TempTester = bl.FindTester(a);
                    if (TempTester != null)
                        grid1.DataContext = TempTester;
                    else
                        throw new Exception("בוחן לא נמצא");

                    int i = 0, j = 0, k = 1;
                    foreach (var item in ScheduleGrid.Children)
                    {
                        if (item is CheckBox)
                        {
                            var value = item as CheckBox;
                            if (value.Name == "checkBox" + k++)
                            {
                                Binding binding = new Binding();
                                binding.Source = TempTester.WorkSchedule(i, j++);
                                binding.Mode = BindingMode.OneTime;
                                value.SetBinding(ToggleButton.IsCheckedProperty, binding);
                                if (j == 7)
                                {
                                    j = 0;
                                    i++;
                                }
                            }
                        }
                    }
                }
                catch(Exception)
                {
                    MessageBox.Show("משהו השתבש , נסה שנית! \n (מפתח-קליטת ת.ז ממחרוזת נכשלה)");
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
            var Box = sender as CheckBox;
            int StartIndex = Box.Name.First(t => t>48 && t<57) - 48 ;
            StartIndex *= Configuration.WorkHours;
            int EndIndex = StartIndex;
            StartIndex -= Configuration.WorkHours - 1;

            foreach (var item in ScheduleGrid.Children)
            {
                if (item is CheckBox)
                {
                    var box = item as CheckBox;
                    int.TryParse(box.Name.Remove(0, 8),out int BoxNumber);
                    if (BoxNumber >= StartIndex && BoxNumber <= EndIndex)
                    {
                        box.IsChecked = Box.IsChecked;
                    }
                }
            }
        }

        private void CheckBoxMain_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxMain1.IsChecked = checkBoxMain.IsChecked;
            checkBoxMain2.IsChecked = checkBoxMain.IsChecked;
            checkBoxMain3.IsChecked = checkBoxMain.IsChecked;
            checkBoxMain4.IsChecked = checkBoxMain.IsChecked;
            checkBoxMain5.IsChecked = checkBoxMain.IsChecked;
        }

        private void KeyDownCheckIfNotNumber(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Data.NumericCheck(sender, e);
        }
    }
}
