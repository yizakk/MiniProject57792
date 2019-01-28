using System.Windows;
using System.Windows.Controls;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// 
    /// This class is made to log the user into the system, using his ID 
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
            IdTextBox.Focus() ; // focusing on the insertion password box
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // we save the ID user inputed in a local class named DATA , for re using later
            Data.UserID = IdTextBox.Text;
            // checking there are any char. in the textbox
            if (Data.UserID.Length == 0)
            {
                MessageBox.Show("אנא הכנס מספר!",
                    "", MessageBoxButton.OK, MessageBoxImage.Asterisk,
                    MessageBoxResult.Yes, MessageBoxOptions.RtlReading);
                return;
            }

            if (Data.UserID == "204412712" || Data.UserID == "029985090")
            {
                MessageBox.Show("שלום מנהל, אנא הזן סיסמה", "", MessageBoxButton.OK, MessageBoxImage.None,
                    MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                Label1.Content = "הזנת סיסמת מנהל";
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

            if (TesterFound != null) // if user ID is found as a tester object
            {
                MessageBox.Show("שלום  " + TesterFound.FullName, "", MessageBoxButton.OK, MessageBoxImage.None,
                    MessageBoxResult.OK, MessageBoxOptions.RtlReading);

                Data.UserType = Data.Usertype.בוחן; // marking user as a tester

                Data.logged = true;
                Data.MainUserControl = new HomePanel();  // sending to home panel
            }

            if (TraineeFound != null) // if it's a trainee - making the match
            {
                MessageBox.Show("שלום  " + TraineeFound.FullName, "", MessageBoxButton.OK, MessageBoxImage.None,
                    MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                Data.UserType = Data.Usertype.תלמיד;
                Data.logged = true;
                Data.MainUserControl = new HomePanel();
            }

        }

        BL.IBL bl = BL.BlFactory.GetBL(); // getting a bl instance

        private void IdNotFound() // if ID isn't found in DS , asking user if he wants to add a user to system
        {
            int x = (int) MessageBox.Show("תעודת זהות לא נמצאה במערכת, האם ברצונך להוסיף משתמש חדש?", 
                    "ת.ז לא נמצאה במערכת", MessageBoxButton.YesNo, MessageBoxImage.Question, 
                    MessageBoxResult.Yes,MessageBoxOptions.RtlReading);

            if(x==6) // is he wants - asking if he wants to add a trainee or a tester - in page AddPerson
            {
                Data.MainUserControl = new AddPerson();
            }

            else // else - leaving him in the same page
            {
                Data.MainUserControl = new Login();
            }
        }

        private void IdTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter) // responding to ENTER key like as a button click
                Button_Click(null, e);
        }

        private void Button2_Click(object sender, RoutedEventArgs e) // this button appears if the ID user
        {                                                           // inputed belongs to a manager
            if (BE.Encryption.VerifyHashPassword(ManagerPasswordBox.Password, BE.Configuration.MasterPassword))
            {// while clicking this button , we check if the hashing of the user input matches the manager password we have in DS
                Data.UserType = Data.Usertype.מנהל;
                Data.MainUserControl = new HomePanel(); // if yes - marking as manager and sending to home panel
            }
        }

        private void ManagerPasswordBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter) // responding enter key as a button click
                Button2_Click(null, e);
        }
    }
}
