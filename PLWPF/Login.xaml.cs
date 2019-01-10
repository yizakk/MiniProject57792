using System.Windows;
using System.Windows.Controls;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for Login.xaml
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
            Data.UserID = IdTextBox.GetLineText(0);

            if (Data.UserID.Length == 0)
            {
                MessageBox.Show("אנא הכנס מספר!",
                    "", MessageBoxButton.OK, MessageBoxImage.Asterisk,
                    MessageBoxResult.Yes, MessageBoxOptions.RtlReading);
                return;
            }

            if (Data.UserID == BE.Configuration.MasterPassword)
            {
                MessageBox.Show("שלום מנהל", "", MessageBoxButton.OK, MessageBoxImage.None,
                    MessageBoxResult.OK, MessageBoxOptions.RtlReading);

                Data.logged = true;
                Data.UserType = Data.Usertype.מנהל;
                Data.MainUserControl = new HomePanel();
                 
                return;
            }

            // trying to get a trainee or a tester that has this id user inputed
            // by sending this id to search in the BL that sends it into the DAL
            BE.Tester TesterFound =  bl.FindTester(Data.UserID);
            BE.Trainee TraineeFound = bl.FindTrainee(Data.UserID);

            // if not found - go to matching function 
            if (TesterFound==null && TraineeFound==null)
            {
                IdNotFound();
                return;
            }

                 

            if(TesterFound!=null)
            {
                MessageBox.Show("שלום  " + TesterFound.FullName,"",MessageBoxButton.OK,MessageBoxImage.None,
                    MessageBoxResult.OK,MessageBoxOptions.RtlReading);

                Data.UserType = Data.Usertype.בוחן;

                Data.logged = true;
                Data.MainUserControl = new HomePanel();
            }

            if(TraineeFound!=null)
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
                MessageBox.Show("שלום ולהתראות", "GB", MessageBoxButton.OK, MessageBoxImage.Hand,
                    MessageBoxResult.OK, MessageBoxOptions.RtlReading);
            }
        }


        private void IdTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                Button_Click(null, e);
        }
    }
}
