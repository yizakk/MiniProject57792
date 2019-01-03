using System.Windows;
using System.Windows.Controls;


namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddPerson.xaml
    /// </summary>
    public partial class AddPerson : UserControl
    {
        public AddPerson()
        {
            InitializeComponent();
        }

        private void TraineeButton_Click(object sender, RoutedEventArgs e)
        {
             Data.MainUserControl = new AddStudent();
             Data.UserType = 1;
        }

        private void TesterButton_Click(object sender, RoutedEventArgs e)
        {
             Data.MainUserControl = new AddTester();
             Data.UserType = 1;
        }
    }
}
