using System;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddTest.xaml
    /// </summary>
    public partial class AddTest : UserControl
    {
        // creating a test instance for adding data to , setting the time to be now
        Test TempTest = new Test
        {
            Date = DateTime.Now
        };
        BL.IBL bl = BL.BlFactory.GetBL(); // getting a bl instance

        public AddTest()
        {
            InitializeComponent();
            //MessageBox.Show("בבקשה מלא את הטופס מלמעלה למטה", "", MessageBoxButton.OK,
            //                     MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RtlReading);

            // initiaing the working hours of the day, for showing in the combobox to choose hour for the test
            int[] array = new int[Configuration.WorkHours];
            for (int i = 0; i < Configuration.WorkHours; i++)
            {
                array[i] = i + 9;
            }
            TimeComboBox.ItemsSource = array;
            TimeComboBox.SelectedIndex = 0;
            dateDatePicker.DataContext = TempTest;

            // if ser type is a trainee - hide the searching for students, only let him choose address,
            // date and time
            if (Data.UserType == Data.Usertype.תלמיד)
            {
                SearchComboBox.Visibility = Visibility.Collapsed;
                SearchTextBlock.Visibility = Visibility.Collapsed;
                traineeIdTextBox.Text = Data.UserID;
            }
            else
            { // if it's a manager or tester - let him choose one of all students
                SearchComboBox.ItemsSource = bl.GetTraineesIdList();
            }

            AddressGrid.DataContext = TempTest.BeginAddress;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TimeComboBox.SelectedIndex == -1)
            {
               // gif.Close();
                MessageBox.Show("אנא בחר שעה", "", MessageBoxButton.OK,
                                MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                return;
            }

            if (SearchComboBox.SelectedIndex == -1 && TempTest.TraineeId.Length<7)
            {
                MessageBox.Show("אנא בחר תלמיד", "", MessageBoxButton.OK,
                                MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                return;
            }

            if (dateDatePicker.SelectedDate.Value.DayOfWeek == DayOfWeek.Friday // if chosen day is friday
                || dateDatePicker.SelectedDate.Value.DayOfWeek == DayOfWeek.Saturday)// or saturday
            {
                MessageBox.Show("ימי העבודה הינם בין ראשון - חמישי", "", MessageBoxButton.OK, // prompting to user and adding two days to date
                MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                dateDatePicker.SelectedDate = dateDatePicker.SelectedDate.Value.AddDays(2);
                return;
            }
        
            if (cityTextBox.Text.Length == 0 || streetTextBox.Text.Length == 0 || buildingNumberTextBox.Text.Length == 0)
            {
                MessageBox.Show("אנא מלא את 3 השדות של הכתובת", "", MessageBoxButton.OK,
                MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                return;
            }

            TempTest.Date = ((DateTime)dateDatePicker.SelectedDate).AddHours(TimeComboBox.SelectedIndex + Configuration.StartHour);
            if (Data.UserType != Data.Usertype.תלמיד) // if user type isn't trainee - pull id from combobox
                TempTest.TraineeId = SearchComboBox.SelectedValue.ToString().Split(' ')[0];
            else // if user is a trainee - we "pull" his id from DATA class
                TempTest.TraineeId = Data.UserID;

            TempTest.CarType = bl.FindTrainee(TempTest.TraineeId).CarType; 
            
            image.Visibility = Visibility.Visible; // showing an image during the looking for tester
            button.IsEnabled = false; // disabling the button during the waiting

            new Thread(() => // Activating the bl layer by thread 
            {
                try
                {
                    bl.AddTest(TempTest);
                }
                catch (MyExceptions ex)
                {
                    if (ex.SuggestedTest == null)
                    {
                        MessageBox.Show(ex._message);
                    }
                    else // there is a suggested test to offer to user
                    {
                        int choice = (int)MessageBox.Show(ex._message, "", MessageBoxButton.YesNo,
                        MessageBoxImage.Information, MessageBoxResult.No, MessageBoxOptions.RtlReading);
                        if (choice == 6)
                        {
                            try
                            {
                                bl.AddTest(ex.SuggestedTest);
                            }
                            catch(MyExceptions Mex)
                            {
                                MessageBox.Show(Mex._message, "", MessageBoxButton.OK,
                                                MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                                Data.MainUserControl = new AddTest();
                            }
                        }
                        else
                        {
                            MessageBox.Show("נסה להוסיף טסט אחר ידנית", "", MessageBoxButton.OK,
                                    MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                            Data.MainUserControl = new AddTest();
                        }
                    }
                }
                Dispatcher.Invoke(new Action(() =>
                {
                    try
                    {
                        image.Visibility = Visibility.Hidden;
                        button.IsEnabled = true;
                        Data.MainUserControl = new AddTest();
                    }
                    catch (Exception n)
                    {
                        MessageBox.Show(n.Message);
                    }
                }));

            }).Start();
        }
       
        private void CityTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

        }
    }
}
