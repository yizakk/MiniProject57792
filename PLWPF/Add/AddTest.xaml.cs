using System;
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

        Test TempTest;
        BL.IBL bl;
        public AddTest()
        {
            InitializeComponent();
            bl = BL.BlFactory.GetBL();
            TempTest = new Test();

            TempTest.Date = DateTime.Now;


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
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            TempTest.Date =(DateTime) dateDatePicker.SelectedDate;
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

            catch (MyExceptions a)
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

            TempTest = new Test();
            TempTest.Date = DateTime.Now;
        }


        private void SearchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SearchComboBox.SelectedIndex != -1)
            {

            }
        }
    }
}
