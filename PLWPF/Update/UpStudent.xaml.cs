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
    /// Interaction logic for UpStudent.xaml
    /// </summary>
    public partial class UpStudent : UserControl
    {
        BL.IBL bl = BL.BlFactory.GetBL();
        Trainee tempTrainee;
        public UpStudent()
        {
            InitializeComponent();

            var sourceList = bl.GetTraineesIdList();
            if (!sourceList.Any())
            {
                ExitFromUC();
                MessageBox.Show("אין תלמידים במאגר");
                button.IsEnabled = false;
                comboBox.IsEnabled = false;
                Data.MainUserControl = new HomePanel();
                Data.UserType = Data.Usertype.אורח;
                return;
            }

            car_typeComboBox.ItemsSource = Enum.GetValues(typeof(CarType));
            genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            gearTypeComboBox.ItemsSource = Enum.GetValues(typeof(Gear));

            if (Data.UserType == Data.Usertype.תלמיד)
            {
                Label.Visibility = Visibility.Collapsed;
                comboBox.Visibility = Visibility.Collapsed;
                tempTrainee = bl.FindTrainee(Data.UserID);
                grid1.DataContext = tempTrainee;
                return;
            }
            comboBox.ItemsSource = sourceList;
            comboBox.SelectedIndex = 0;

        }

        private void ExitFromUC()
        {
            Data.MainUserControl = new HomePanel();
            return;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox.SelectedIndex != -1)
            {
                string a = comboBox.SelectedValue.ToString().Split(' ')[0];
                tempTrainee = bl.FindTrainee(a);
                grid1.DataContext = tempTrainee;
                AddressGrid.DataContext = tempTrainee.Address;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateTrainee(tempTrainee);
            }
            catch (Exceptions a)
            {
                MessageBox.Show(a._message);
                return;
            }
            MessageBox.Show("העידכון הסתיים בהצלחה");

        }

        private void KeyDownCheckIfNotNumber(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Data.NumericCheck(sender, e);
        }


    }
}
;