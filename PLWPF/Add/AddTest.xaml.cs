using System;
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
            for(int i=0;i<Configuration.WorkHours;i++)
            {
                array[i] = i + 9;
            }
            TimeComboBox.ItemsSource = array;
         //   TimeComboBox.SelectedIndex = 0;
            dateDatePicker.DataContext = TempTest;
            
            // if ser type is a trainee - hide the searching for students, only let him choose address,
            // date and time
            if(Data.UserType == Data.Usertype.תלמיד)
            {
                SearchComboBox.Visibility = Visibility.Collapsed;
                SearchTextBlock.Visibility = Visibility.Collapsed;
                traineeIdTextBox.Text  = Data.UserID;
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
            if(!searching)
            {
                SearchDistance();
                return;
            }
            if (SearchComboBox.SelectedValue == null)
            {
                MessageBox.Show("אנא בחר תלמיד");
                return;
            }

            if (MapRequest.Distance == null && counter++ < 1)
            {
                MessageBox.Show("עדיין לא נמצאו בוחנים באזורך, אנא נסה שנית בעוד מספר שניות", "", MessageBoxButton.OK,
                     MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                return;
            }

            // taking the date user chose , adding to it the chosen hour (by creating a new
            //instance with the old values of day month and year + the hour from the combobox
            TempTest.Date = (DateTime)dateDatePicker.SelectedDate;
            TempTest.Date = new DateTime(TempTest.Date.Year, TempTest.Date.Month, TempTest.Date.Day, (int)TimeComboBox.SelectedValue, 0, 0);

            if (Data.UserType != Data.Usertype.תלמיד)
            {
                Trainee tempTrainee = bl.FindTrainee(SearchComboBox.SelectedValue.ToString().Split(' ')[0]);
                TempTest.CarType = tempTrainee.CarType;
                TempTest.TraineeId = tempTrainee.Id;
            }

            try
            {
                bl.AddTest(TempTest);
            }

            catch (MyExceptions a) // here we might get another optional test to offer to the trainee 
            { // for details - see program documentation of contact dev. (or just look in the BL doc.)
                if (a.SuggestedTest != null)
                {
                    int x = (int)MessageBox.Show(a._message, "הצעת טסט חלופי", MessageBoxButton.YesNo, MessageBoxImage.Question,
                        MessageBoxResult.No, MessageBoxOptions.RtlReading);
                    if (x == 6)
                    {
                        try
                        {
                            bl.AddTest(a.SuggestedTest);
                        }
                        catch (MyExceptions cat)
                        {
                            MessageBox.Show(cat._message, "", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK,
                                MessageBoxOptions.RtlReading);
                        }
                    }
                    else
                    {
                        MessageBox.Show("נסה להוסיף טסט אחר ידנית");
                    }

                }
                else
                    MessageBox.Show(a._message);
            }

            TempTest = new Test
            {
                Date = DateTime.Now
            };
            BE.MapRequest.Distance = null; // Re-setting distance as null, for further calculation
        }

        private void SearchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // since we allow searching, we have to insure the ID selected is a real one retrieved from
            // the DS. 
            if (SearchComboBox.SelectedIndex != -1)
            {// if it is - setting the trainee ID in the test object to be the chosen id
                TempTest.TraineeId = SearchComboBox.SelectedValue.ToString().Split(' ')[0];
            }
        }

        private void CityTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // activating the searching for distance function in a thread - if user clicked ENTER
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                SearchDistance();
            }
        }

        private void SearchDistance()
        {
            // first checking if there wasn't activated a search thread already
            if (!searching)
            {
                if (SearchComboBox.SelectedIndex == -1)
                {
                    MessageBox.Show("אנא בחר תלמיד", "", MessageBoxButton.OK,
                                    MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                }

                else
                {
                    searching = true;
                    try
                    {
                        MessageBox.Show("החל ברקע חיפוש בוחן באזורך", "", MessageBoxButton.OK,
                                        MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RtlReading);

                        Thread thread = new Thread(() => BE.MapRequest.MapRequestLoop(bl.FindTrainee(TempTest.TraineeId).AddressToString, TempTest.BeginAddressString));
                        Thread.BeginCriticalRegion();
                        thread.Name = "LocatingTesters";
                        thread.Start();
                        
                        Thread.EndCriticalRegion();
                    }
                    catch(MyExceptions a)
                    { bl.AddTest(TempTest); }
                }
            }
        }

        private void TimeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            
                CheckAndFind();

            
        }

        private void DateDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckAndFind();
        }

        private void CheckAndFind()
        {
            TimeSpan timeSpan = new TimeSpan();

            timeSpan = DateTime.Now - PageLoad;
            if (TestersThread != null && TestersThread.IsAlive) 
                TestersThread.Abort();
            if (dateDatePicker.SelectedDate.Value.DayOfWeek == DayOfWeek.Friday
                && dateDatePicker.SelectedDate.Value.DayOfWeek == DayOfWeek.Saturday)
            {
                MessageBox.Show("ימי העבודה הינם בין ראשון - חמישי", "", MessageBoxButton.OK,
                MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                dateDatePicker.SelectedDate = dateDatePicker.SelectedDate.Value.AddDays(2);
                return;
            }
            if (timeSpan.Seconds>=2)
            {
                if (TimeComboBox.SelectedIndex == -1)
                {
                    MessageBox.Show("אנא בחר שעה", "", MessageBoxButton.OK,
                                     MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                    return;
                }
            }
            FindingAvailableTesters();
        }

        Thread TestersThread;

        private void FindingAvailableTesters()
        {
            if (TestersThread == null )
            {
                TestersThread = new Thread(() => bl.Kuku((DateTime)dateDatePicker.SelectedDate));
            }
            if (!TestersThread.IsAlive)
            {
                TestersThread.Start();
            }
        }
    }
}
