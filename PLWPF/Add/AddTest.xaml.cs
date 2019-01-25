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
        DateTime PageLoad;
        // creating a test instance for adding data to , setting the time to be now
        Test TempTest = new Test
        {
            Date = DateTime.Now
        };
        BL.IBL bl = BL.BlFactory.GetBL(); // getting a bl instance
        int counter = 0;
        bool searching = false;


        public AddTest()
        {
            PageLoad = DateTime.Now;
            InitializeComponent();
            MessageBox.Show("בבקשה מלא את הטופס מלמעלה למטה", "", MessageBoxButton.OK,
                                 MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RtlReading);

            // initiaing the working hours of the day, for showing in the combobox to choose hour for the test
            int[] array = new int[Configuration.WorkHours];
            for (int i = 0; i < Configuration.WorkHours; i++)
            {
                array[i] = i + 9;
            }
            TimeComboBox.ItemsSource = array;
            //   TimeComboBox.SelectedIndex = 0;
            dateDatePicker.DataContext = TempTest;

            // if ser type is a trainee - hide the searching for students, only let him choose address,
            // date and time
            if (Data.UserType == Data.Usertype.תלמיד)
            {
                SearchComboBox.Visibility = Visibility.Collapsed;
                SearchTextBlock.Visibility = Visibility.Collapsed;
                traineeIdTextBox.Text = Data.UserID;
                //traineeIdTextBox.Visibility = Visibility.Visible;
                TempTest.CarType = bl.FindTrainee(Data.UserID).CarType;
                TempTest.TraineeId = Data.UserID;
            }
            else
            { // if it's a manager or tester - let him choose one of all students
                SearchComboBox.ItemsSource = bl.GetTraineesIdList();
            }

            AddressGrid.DataContext = TempTest.BeginAddress;
            //    button.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TimeComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("אנא בחר שעה", "", MessageBoxButton.OK,
                                MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                return;
            }

            if (SearchComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("אנא בחר תלמיד", "", MessageBoxButton.OK,
                                MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                return;
            }

            if (dateDatePicker.SelectedDate.Value.DayOfWeek == DayOfWeek.Friday
                || dateDatePicker.SelectedDate.Value.DayOfWeek == DayOfWeek.Saturday)
            {
                MessageBox.Show("ימי העבודה הינם בין ראשון - חמישי", "", MessageBoxButton.OK,
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
            TempTest.TraineeId = SearchComboBox.SelectedValue.ToString().Split(' ')[0];
            TempTest.CarType = bl.FindTrainee(TempTest.TraineeId).CarType;

            new Thread(() =>
            {
                try
                {
                    bl.AddTest(TempTest);
                }
                catch (MyExceptions ex)
                {
                    if (ex.SuggestedTest == null)
                        MessageBox.Show(ex._message);
                    else
                    {
                        int choice = (int)MessageBox.Show(ex._message, "", MessageBoxButton.YesNo,
                        MessageBoxImage.Information, MessageBoxResult.No, MessageBoxOptions.RtlReading);
                        if (choice == 6)
                        {
                            bl.AddTest(ex.SuggestedTest);
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

                    }
                    catch (Exception n)
                    {
                        MessageBox.Show(n.Message);
                    }
                }));

            }).Start();

            //    try
            //    {
            //        bl.AddTest(TempTest);
            //    }
            //    catch(MyExceptions ex)
            //    {
            //        if(ex.SuggestedTest==null)
            //        {
            //            MessageBox.Show(ex._message, "", MessageBoxButton.OK,
            //            MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RtlReading);
            //        }
            //        else // offering user additional test
            //        {
            //            int choice = (int) MessageBox.Show(ex._message, "", MessageBoxButton.YesNo,
            //            MessageBoxImage.Information, MessageBoxResult.No, MessageBoxOptions.RtlReading);
            //            if(choice==6)
            //            {
            //                bl.AddTest(ex.SuggestedTest);
            //            }
            //            else
            //            {
            //                MessageBox.Show("נסה להוסיף טסט אחר ידנית", "", MessageBoxButton.OK,
            //                        MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RtlReading);
            //                Data.MainUserControl = new AddTest();
            //            }
            //        }
            //    }
            //}
        }

        private void CityTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

        }


















        //private void SearchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    // since we allow searching, we have to insure the ID selected is a real one retrieved from
        //    // the DS. 
        //    if (SearchComboBox.SelectedIndex != -1)
        //    {// if it is - setting the trainee ID in the test object to be the chosen id
        //        TempTest.TraineeId = SearchComboBox.SelectedValue.ToString().Split(' ')[0];
        //    }
        //}

        //private void CityTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        //{
        //    // activating the searching for distance function in a thread - if user clicked ENTER
        //    if (e.Key == System.Windows.Input.Key.Enter)
        //    {
        //        SearchDistance();
        //    }
        //}

        //private void SearchDistance()
        //{
        //    // first checking if there wasn't activated a search thread already
        //    if (!searching)
        //    {
        //        if (SearchComboBox.SelectedIndex == -1)
        //        {
        //            MessageBox.Show("אנא בחר תלמיד", "", MessageBoxButton.OK,
        //                            MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RtlReading);
        //        }

        //        else
        //        {
        //            foreach (var tester in bl.FindAvilableTesters(TempTest.Date))
        //            {
        //                searching = true;
        //                try
        //                {
        //                    MessageBox.Show("החל ברקע חיפוש בוחן באזורך", "", MessageBoxButton.OK,
        //                                    MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RtlReading);

        //                    Thread thread = new Thread(() => BE.MapRequest.MapRequestLoop(bl.FindTrainee(TempTest.TraineeId).AddressToString, TempTest.BeginAddressString));
        //                    Thread.BeginCriticalRegion();
        //                    thread.Name = "LocatingTesters";
        //                    thread.Start();

        //                    Thread.EndCriticalRegion();
        //                }
        //                catch (MyExceptions a)
        //                { bl.AddTest(TempTest); }
        //            }
        //        }
        //    }
        //}

        //private void TimeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    CheckAndFind();
        //}

        //private void DateDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    CheckAndFind();
        //}

        //private void CheckAndFind()
        //{
        //    TimeSpan timeSpan = new TimeSpan();

        //    timeSpan = DateTime.Now - PageLoad;
        //    if (TestersThread != null && TestersThread.IsAlive) 
        //        TestersThread.Abort();
        //    if (dateDatePicker.SelectedDate.Value.DayOfWeek == DayOfWeek.Friday
        //        && dateDatePicker.SelectedDate.Value.DayOfWeek == DayOfWeek.Saturday)
        //    {
        //        MessageBox.Show("ימי העבודה הינם בין ראשון - חמישי", "", MessageBoxButton.OK,
        //        MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RtlReading);
        //        dateDatePicker.SelectedDate = dateDatePicker.SelectedDate.Value.AddDays(2);
        //        return;
        //    }
        //    if (timeSpan.Seconds>=2)
        //    {
        //        if (TimeComboBox.SelectedIndex == -1)
        //        {
        //            MessageBox.Show("אנא בחר שעה", "", MessageBoxButton.OK,
        //                             MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RtlReading);
        //            return;
        //        }
        //    }
        // //   FindingAvailableTesters();
        //}


        //Thread TestersThread;
        //private void FindingAvailableTesters()
        //{
        //    if (TestersThread == null )
        //    {
        //        TestersThread = new Thread(() => bl.Kuku((DateTime)dateDatePicker.SelectedDate));
        //    TestersThread.Name = "FindingTesters";
        //    }
        //    if (!TestersThread.IsAlive)
        //    {
        //        TestersThread.Start();
        //    }
        //}
    }
}
