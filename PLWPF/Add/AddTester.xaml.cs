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
    /// Interaction logic for AddTester.xaml
    /// </summary>
    public partial class AddTester : UserControl
    {

        Tester TempTester;
        BL.IBL bl;
        public AddTester()
        {
            InitializeComponent();

            bl = BL.BlFactory.GetBL();

            TempTester = new Tester();
            TempTester.BirthDate = DateTime.Parse("2000 01 01");


            try
            {
                grid1.DataContext = TempTester;
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }

            TempTester.Id = Data.UserID;
            car_typeComboBox.ItemsSource = Enum.GetValues(typeof(CarType));
            genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CheckAndAdd();
        }

        private void CheckAndAdd()
        {
            if (TempTester.Id.Length == 0)
            {
                MessageBox.Show("נא להכניס תעודת זהות", "", MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK
                    , MessageBoxOptions.RtlReading);
                idTextBox.Clear();
                return;
            }

            TimeSpan a = DateTime.Now - birthDateDatePicker.SelectedDate.Value;
            if (a.Days / 365 < 40)
            {
                MessageBox.Show("אין אפשרות להוסיף בוחן לפני גיל 40 שנים");
                birthDateDatePicker.SelectedDate = DateTime.Parse("01 01 2000");

                return;
            }

            if (phoneNumberTextBox.GetLineLength(0) > 0 && phoneNumberTextBox.GetLineLength(0) < 10)
            {
                MessageBox.Show("מספר טלפון לא יכול להכיל פחות מ10 ספרות");
                return;
            }

            int i = 0, j = 0;
            foreach(var item in ScheduleGrid.Children)
            {
                if(item is CheckBox)
                {
                    var value = item as CheckBox;
                    TempTester.WorkSchedule(i, j++, value.IsChecked);
                    if(j==7)
                    {
                        j = 0;
                        i++;
                    }
                }
            }

            try
            {
                bl.AddTester(TempTester);
            }
            catch (Exceptions c)
            {
                MessageBox.Show(c._message);
                return;
            }

            int choice = (int)MessageBox.Show("הבוחן נוסף בהצלחה , האם ברצונך להוסיף עוד בוחן?", "", MessageBoxButton.YesNo,
                MessageBoxImage.Asterisk, MessageBoxResult.None, MessageBoxOptions.RtlReading);
            if (choice == 6)
            {
                Data.MainUserControl = new AddTester();
                Data.Change = 1;
            }
            else
            {

            }

        }

    }
}

