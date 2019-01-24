using System.Windows;
using System.Windows.Controls;


namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddPerson.xaml. this class defines which options to add a person 
    /// to the system we have. basically one of two - trainee or tester. for tester - you have to enter
    /// a special password (given to you by the system admin- for details- talk to us)
    /// </summary>
    public partial class AddPerson : UserControl
    {
        int count = 0;
        public AddPerson()
        {
            InitializeComponent();
        }

        private void TraineeButton_Click(object sender, RoutedEventArgs e)
        {  // as we have to store which type the current user is, if he clicks the trainee button
            // he is probably not a manager or a tester (cause they can add trainee through there panel)
            Data.MainUserControl = new AddStudent();
            Data.UserType = Data.Usertype.תלמיד;
             
        }

        private void TesterButton_Click(object sender, RoutedEventArgs e)
        {
            // if user wants to add a tester - we hide all content of page and instead show the password 
            // require
            label.Visibility = Visibility.Visible;
            buttonIfIsTester.Visibility = Visibility.Visible;
            textBox.Visibility = Visibility.Visible;

            traineeButton.Visibility = Visibility.Hidden;
            testerButton.Visibility = Visibility.Hidden;

            textBox.Focus();
        }

        private void ButtonIfIsTester_Click(object sender, RoutedEventArgs e)
        {
            // if user clicked 3 times and inserted wrong password - he should back to login page and retry
            // ( rabbi Arye Wissen said it's not such a good idea to totally block him)
            if (count==3)
            {
                MessageBox.Show("גישתך נחסמה");
                return;
            }

            
            if (BE.Encryption.VerifyHashPassword(textBox.Password, BE.Configuration.TesterPassword))
            {
                Data.MainUserControl = new AddTester();
                Data.UserType = Data.Usertype.בוחן;
            }
            // wrong password - counting it and rejecting
            else
            {
                count++;

                if (count == 3)
                {
                    MessageBox.Show("גישתך נחסמה");
                    return;
                    
                }
                MessageBox.Show("!קוד שגוי");
                textBox.Clear();
                textBox.Focus();
            }
        }

        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // responding to ENTER key as a trigger to make a button click
            if (e.Key == System.Windows.Input.Key.Enter)
                ButtonIfIsTester_Click(null, e);
        }
    }
}
