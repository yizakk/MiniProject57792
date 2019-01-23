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
        Test TempTest = new Test
        {
            Date = DateTime.Now
        };
        BL.IBL bl = BL.BlFactory.GetBL();
        int counter = 0;

        public AddTest()
        {
            InitializeComponent();

// initiaing the kowking hours of the day, for showing in the combobox to choose hour for the test
            int[] array = new int[Configuration.WorkHours];

            for(int i=0;i<Configuration.WorkHours;i++)
            {
                array[i] = i + 9;
            }
            TimeComboBox.ItemsSource = array;
            TimeComboBox.SelectedIndex = 0;
            dateDatePicker.DataContext = TempTest;

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
            {
                SearchComboBox.ItemsSource = bl.GetTraineesIdList();
            }

            AddressGrid.DataContext = TempTest.BeginAddress;
        //    button.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (BE.MapRequest.Distance == null && counter++ < 3)
            {
                MessageBox.Show("עדיין לא נמצאו בוחנים באזורך, אנא נסה שנית בעוד מספר שניות", "", MessageBoxButton.OK,
                     MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                return;
            }

            if (SearchComboBox.SelectedValue == null)
            {
                MessageBox.Show("אנא בחר תלמיד");
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
            {
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
            if (SearchComboBox.SelectedIndex != -1)
            {
                TempTest.TraineeId = SearchComboBox.SelectedValue.ToString().Split(' ')[0];
            }
        }

        private void CityTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                if (bl.FindTrainee(TempTest.TraineeId) == null)
                {
                    MessageBox.Show("אנא בחר תלמיד", "", MessageBoxButton.OK,
                                    MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                }

                else
                {
                    MessageBox.Show("החל ברקע חיפוש בוחן באזורך", "", MessageBoxButton.OK,
                                    MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                    Thread thread = new Thread(() => BE.MapRequest.MapRequestLoop(bl.FindTrainee(TempTest.TraineeId).AddressToString, TempTest.BeginAddressString));
                    thread.Start();
                }
            }
        }
    }
}
