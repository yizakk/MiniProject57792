using System;
using System.Windows;
using System.Windows.Controls;

using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddStudent.xaml
    /// </summary>
    public partial class AddStudent : UserControl
    {

        Trainee TempTrainee;
        BL.IBL bl;
        BE.Address address = new Address();


        public AddStudent()
        {
            Data.logged = true;
            InitializeComponent();

            bl = BL.BlFactory.GetBL();
            TempTrainee = new Trainee();
            TempTrainee.BirthDate = DateTime.Parse("2000 01 01");
            birthDateDatePicker.DisplayDateEnd = DateTime.Now.AddYears(-18);
            buildingNumberTextBox1.Text = "";
            try
            {
                grid1.DataContext = TempTrainee;
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }

            try
            {
                grid2.DataContext = address;
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }

            TempTrainee.Id = Data.UserID;
            
// קשירת הקומבובוקסים
            car_typeComboBox.ItemsSource = Enum.GetValues(typeof(CarType));
            GenderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            gearTypeComboBox.ItemsSource = Enum.GetValues(typeof(Gear));

        } 

// לחיצה על כפתור "עדכן" ר
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CheckAndAdd();
        }

        private void CheckAndAdd()
        {
            if (TempTrainee.Id.Length !=9)
            {
                MessageBox.Show("תעודת זהות צריכה  להכיל  9 ספרות","",MessageBoxButton.OK,MessageBoxImage.None,
                    MessageBoxResult.OK,MessageBoxOptions.RtlReading);
                idTextBox.Clear();
                return;
            }

            TimeSpan a = DateTime.Now - birthDateDatePicker.SelectedDate.Value;
            if (a.Days / 365 < 18)
            {
                MessageBox.Show("אין אפשרות להוסיף תלמיד שטרם מלאו לו 18 שנים", "", MessageBoxButton.OK, MessageBoxImage.None,
                    MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                birthDateDatePicker.SelectedDate = DateTime.Parse("01 01 2000");

                return;
            }

            if (phoneNumberTextBox.GetLineLength(0) > 0 && phoneNumberTextBox.GetLineLength(0) < 9)
            {
                MessageBox.Show("מספר טלפון לא יכול להכיל פחות מ9 ספרות", "", MessageBoxButton.OK, MessageBoxImage.None,
                    MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                return;
            }


            try
            {
                TempTrainee.Address = address;
                bl.AddTrainee(TempTrainee);
            }
            catch (Exception c)
            {
                if (c is MyExceptions)
                {
                    var b = (MyExceptions)c;
                    MessageBox.Show(b._message);

                    return;
                }
                MessageBox.Show(c.Message);
                return;
            }

            if (Data.UserType == Data.Usertype.תלמיד)
            {
                MessageBox.Show("התלמיד נוסף בהצלחה!", "", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK,
                    MessageBoxOptions.RtlReading);
                Data.UserID = TempTrainee.Id;
                Data.MainUserControl = new  HomePanel();
                 
                return;
            }

            int choice = (int)MessageBox.Show("התלמיד נוסף בהצלחה , האם ברצונך להוסיף עוד תלמיד?", "", MessageBoxButton.YesNo,
                            MessageBoxImage.Asterisk, MessageBoxResult.None, MessageBoxOptions.RtlReading);
            if(choice == 6)
            {
                Data.MainUserControl = new AddStudent();
            }
            else
            {
                Data.MainUserControl = new HomePanel();
            }
        }

        private void KeyDownCheckIfNotNumber(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Data.NumericCheck(sender,e);
        }

        private void IdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
    }
}
    