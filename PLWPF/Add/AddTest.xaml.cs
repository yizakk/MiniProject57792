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

            try
            {
                FormGrid.DataContext = TempTest;
            }

            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }

            if(Data.UserType == Data.Usertype.תלמיד)
            {
                traineeDataGrid.Visibility = Visibility.Collapsed;
                traineeIdTextBox.Text  = Data.UserID;
                //traineeDataGrid.DataContext = bl.FindTrainee(Data.UserID);
                TempTest.CarType = bl.FindTrainee(Data.UserID).Car_type;
                TempTest.TraineeId = Data.UserID;
            }
            else
            {
                traineeDataGrid.DataContext = bl.GetTraineeList();
            }
           
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TempTest.Date = new DateTime(TempTest.Date.Year, TempTest.Date.Month, TempTest.Date.Day, (int)TimeComboBox.SelectedValue, 0, 0);
            if(Data.UserType != Data.Usertype.תלמיד)
            {
                var Trainee = (BE.Trainee)traineeDataGrid.SelectedValue;
                TempTest.CarType = Trainee.Car_type;
                TempTest.TraineeId = Trainee.Id;
            }
            try
            {
                bl.AddTest(TempTest);
            }

            catch (Exceptions a)
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
                        catch (Exceptions cat)
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

                Data.MainUserControl = new HomePanel();
                 
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }
    }
}
