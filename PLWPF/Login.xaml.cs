using System.Windows;
using System.Windows.Controls;
using PLWPF.UC;

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
                MessageBox.Show("שלום מנהל");

                Data.logged = true;
                Data.MainUserControl = new HomePanel();
                Data.Change = 1;
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
                MessageBox.Show("שלום  " + TesterFound.FullName);

                
                MainGrid.Children.Clear();
                Data.MainUserControl = new UpTester();
                MainGrid.Children.Add(Data.MainUserControl);
                return;
            }

            if(TraineeFound!=null)
            {

                MessageBox.Show("שלום  " + TraineeFound.FullName);
                
                MainGrid.Children.Clear();
                Data.MainUserControl = new UpStudent();
                MainGrid.Children.Add(Data.MainUserControl);
                
                return;
            }

        }

        BL.IBL bl = BL.BlFactory.GetBL();

        private void TraineeMenu()
        {
            Data.UserType = 1 ;
        }

        private void IdNotFound()
        {
            int x = (int) MessageBox.Show("תעודת זהות לא נמצאה במערכת, האם ברצונך להוסיף משתמש חדש?", 
                    "ת.ז לא נמצאה במערכת", MessageBoxButton.YesNo, MessageBoxImage.Question, 
                    MessageBoxResult.Yes,MessageBoxOptions.RtlReading);

            if(x==6)
            {
                Data.MainUserControl = new AddPerson();
                Data.UserType = 1;
                Data.logged = true;
                //Label1.Content = "הזן סיסמה ולחץ לאישור";
                //MessageBox.Show("אנא הזן סיסמת מנהל", "דרישת סיסמה",
                //MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK,
                //MessageBoxOptions.RtlReading);
                //MainGrid.Children.Remove(button1);
                //button1.Click -= Button_Click;
                //button1.Click += Button2_Click;
                //IdTextBox.Focus();
                //IdTextBox.Clear();

            }

            else
            {
                MessageBox.Show("שלום ולהתראות", "GB", MessageBoxButton.OK, MessageBoxImage.Hand,
                    MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                Application.Current.Shutdown();
            }


        }


        /// <summary>
        /// Button2 apperance means id wasn't found and now we are looking for admin password to insert a new user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            if(IdTextBox.GetLineText(0) == BE.Configuration.MasterPassword)
            {
                AddPerson();
            }

            else
            {
                MessageBox.Show("סיסמה שגויה!", "שגיאת סיסמה", MessageBoxButton.OK, 
                    MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.RtlReading);

                IdTextBox.Clear();
                IdTextBox.Focus();
            }
        }

        private void AddPerson()
        {
            Data.MainUserControl = new AddPerson();
            Data.Change = 1;
        }
    }
}
