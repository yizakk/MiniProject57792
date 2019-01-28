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
            TempTrainee.BirthDate = DateTime.Now.AddYears(Configuration.TraineeMinAge);
            buildingNumberTextBox1.Text = "";
            try
            {
                grid1.DataContext = TempTrainee;
                grid2.DataContext = address;
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
            if (Data.UserType == Data.Usertype.תלמיד)
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
            try
            {
                if (TempTrainee.Id.Length != 9)
                {
                    MessageBox.Show("תעודת זהות צריכה  להכיל  9 ספרות", "", MessageBoxButton.OK, MessageBoxImage.None,
                        MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                    idTextBox.Clear();
                    return;
                }
                if (string.IsNullOrEmpty(firstNameTextBox.Text))
                {
                    MessageBox.Show("אנא מלא שם פרטי", "", MessageBoxButton.OK, MessageBoxImage.None,
                       MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                    idTextBox.Clear();
                    return;
                }

                if (string.IsNullOrEmpty(lastNameTextBox.Text))
                {
                    MessageBox.Show("אנא מלא שם משפחה", "", MessageBoxButton.OK, MessageBoxImage.None,
                       MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                    idTextBox.Clear();
                    return;
                }

                if (string.IsNullOrEmpty(GenderComboBox.Text))
                {
                    MessageBox.Show("אנא מלא את המגדר", "", MessageBoxButton.OK, MessageBoxImage.None,
                       MessageBoxResult.OK, MessageBoxOptions.RtlReading);
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
                if (string.IsNullOrEmpty(phoneNumberTextBox.Text))
                {
                    MessageBox.Show("נא להכניס טלפון", "", MessageBoxButton.OK, MessageBoxImage.None,
                        MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                    return;
                }
                if (TempTrainee.PhoneNumber.Length < 10)
                {
                    MessageBox.Show("טלפון צריך להיות בן 10 ספרות", "", MessageBoxButton.OK, MessageBoxImage.None,
                       MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                    idTextBox.Clear();
                    return;
                }
                if (string.IsNullOrEmpty(schoolNameTextBox.Text))
                {
                    MessageBox.Show("נא להכניס שם בית הספר", "", MessageBoxButton.OK, MessageBoxImage.None,
                        MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                    return;
                }
                if (string.IsNullOrEmpty(teacherNameTextBox.Text))
                {
                    MessageBox.Show("נא להכניס שם מורה", "", MessageBoxButton.OK, MessageBoxImage.None,
                        MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                    return;
                }
                if (string.IsNullOrEmpty(numLessonsTextBox.Text))
                {
                    MessageBox.Show("נא להכניס מספר שיעורים", "", MessageBoxButton.OK, MessageBoxImage.None,
                        MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                    return;
                }
                if (string.IsNullOrEmpty(cityTextBox1.Text))
                {
                    MessageBox.Show("נא להכניס את שם העיר", "", MessageBoxButton.OK, MessageBoxImage.None,
                        MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                    return;
                }
                if (string.IsNullOrEmpty(streetTextBox1.Text))
                {
                    MessageBox.Show("נא להכניס את שם הרחוב", "", MessageBoxButton.OK, MessageBoxImage.None,
                        MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                    return;
                }
                if (string.IsNullOrEmpty(buildingNumberTextBox1.Text))
                {
                    MessageBox.Show("נא להכניס את מספר בנין", "", MessageBoxButton.OK, MessageBoxImage.None,
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
                    if (c is MyExceptions b)
                    {
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
                    Data.MainUserControl = new HomePanel();

                    return;
                }

                int choice = (int)MessageBox.Show("התלמיד נוסף בהצלחה , האם ברצונך להוסיף עוד תלמיד?", "", MessageBoxButton.YesNo,
                                MessageBoxImage.Asterisk, MessageBoxResult.None, MessageBoxOptions.RtlReading);
                if (choice == 6)
                {
                    Data.MainUserControl = new AddStudent();
                }
                else
                {
                    Data.MainUserControl = new HomePanel();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        

        private void KeyDownCheckIfNotNumber(object sender, System.Windows.Input.KeyEventArgs e)
            {
                Data.NumericCheck(sender, e);
            }

            private void IdTextBox_TextChanged(object sender, TextChangedEventArgs e)
            {
            }
        
      
    }
}
    