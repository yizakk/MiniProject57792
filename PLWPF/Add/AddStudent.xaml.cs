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
            genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
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

            if (phoneNumberTextBox.GetLineLength(0) > 0 && phoneNumberTextBox.GetLineLength(0) < 10)
            {
                MessageBox.Show("מספר טלפון לא יכול להכיל פחות מ10 ספרות", "", MessageBoxButton.OK, MessageBoxImage.None,
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
                if (c is Exceptions)
                {
                    var b = (Exceptions)c;
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
            //if (e.Key == System.Windows.Input.Key.Tab )
            //{ return; }
            //if(e.Key == System.Windows.Input.Key.Enter)
            //{
            //    return;
            //}

            //string key = e.Key.ToString().TrimStart('D');
            //int value = -1;
            //bool convert = int.TryParse(key, out value);

            
            //if (!convert)
            //{
                
            //    e.Handled = true;
            //    var item = (TextBox)sender;

            //    MessageBox.Show("השדה " +  " יכול להכיל רק מספרים!","",MessageBoxButton.OK,MessageBoxImage.None,
            //                        MessageBoxResult.OK,MessageBoxOptions.RtlReading);
            //}
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {

            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }

        private void UserControl_Loaded_2(object sender, RoutedEventArgs e)
        {

            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }

        private void IdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
    }
}
    