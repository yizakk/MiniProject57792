using System.Windows;
using System.Windows.Controls;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// 
    /// this class is made to log the user into the system, using his ID 
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
            IdTextBox.Focus() ;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // we save the ID user inputed in a local class named DATA , for re using later
            Data.UserID = IdTextBox.Password;
            // checking there are any char. in the textbox
            if (Data.UserID.Length == 0)
            {
                MessageBox.Show("אנא הכנס מספר!",
                    "", MessageBoxButton.OK, MessageBoxImage.Asterisk,
                    MessageBoxResult.Yes, MessageBoxOptions.RtlReading);
                return;
            }

            if (Data.UserID == "204412712")
            {
                MessageBox.Show("שלום מנהל, אנא הזן סיסמה", "", MessageBoxButton.OK, MessageBoxImage.None,
                    MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                //MainGrid.Children.Clear();
                IdTextBox.Visibility = Visibility.Collapsed;
                button1.Visibility = Visibility.Collapsed;
                ManagerPasswordBox.Visibility = Visibility.Visible;
                button2.Visibility = Visibility.Visible;
                return;
            }

            // trying to get a trainee or a tester that has the ID user inputed
            // by sending this id to search in the BL unction that sends it into the DAL
            BE.Tester TesterFound = bl.FindTester(Data.UserID);
            BE.Trainee TraineeFound = bl.FindTrainee(Data.UserID);

            // if not found - go to matching function 
            if (TesterFound == null && TraineeFound == null)
            {
                IdNotFound();
                return;
            }

            if (TesterFound != null)
            {
                MessageBox.Show("שלום  " + TesterFound.FullName, "", MessageBoxButton.OK, MessageBoxImage.None,
                    MessageBoxResult.OK, MessageBoxOptions.RtlReading);

                Data.UserType = Data.Usertype.בוחן;

                Data.logged = true;
                Data.MainUserControl = new HomePanel();
            }

            if (TraineeFound != null)
            {
                MessageBox.Show("שלום  " + TraineeFound.FullName, "", MessageBoxButton.OK, MessageBoxImage.None,
                    MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                Data.UserType = Data.Usertype.תלמיד;
                Data.logged = true;
                Data.MainUserControl = new HomePanel();
            }

        }

        BL.IBL bl = BL.BlFactory.GetBL();


        private void IdNotFound()
        {
            int x = (int) MessageBox.Show("תעודת זהות לא נמצאה במערכת, האם ברצונך להוסיף משתמש חדש?", 
                    "ת.ז לא נמצאה במערכת", MessageBoxButton.YesNo, MessageBoxImage.Question, 
                    MessageBoxResult.Yes,MessageBoxOptions.RtlReading);

            if(x==6)
            {
                Data.MainUserControl = new AddPerson();
            }

            else
            {
                Data.MainUserControl = new Login();
            }
        }


        private void IdTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                Button_Click(null, e);
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            if (BE.Encryption.VerifyHashPassword(ManagerPasswordBox.Password, BE.Configuration.MasterPassword))
            {
                Data.UserType = Data.Usertype.מנהל;
                Data.MainUserControl = new HomePanel();
            }

        }

        private void ManagerPasswordBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                Button2_Click(null, e);
        }
    }
}
