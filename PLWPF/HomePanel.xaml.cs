using System.Windows;
using System.Windows.Controls;

namespace PLWPF.UC
{
    /// <summary>
    /// Interaction logic for HomePanel.xaml
    /// </summary>
    public partial class HomePanel : UserControl
    {
        public HomePanel()
        {

            InitializeComponent();

            if(Data.UserType == Data.Usertype.תלמיד)
            {
                delTesterButton.Visibility = Visibility.Collapsed;
                testerButton.Visibility = Visibility.Collapsed;
                UpTesterButton.Visibility = Visibility.Collapsed;
                UpTestButton.Visibility = Visibility.Collapsed;
            }

        }

        private void TraineeButton_Click(object sender, RoutedEventArgs e)
        {
            Data.MainUserControl = new AddStudent();
            Data.Change = 1;
        }

        private void TesterButton_Click(object sender, RoutedEventArgs e)
        {
            Data.MainUserControl = new AddTester();
            Data.Change = 1;
        }

        private void AddTestButton_Click(object sender, RoutedEventArgs e)
        {
            Data.MainUserControl = new AddTest();
            Data.Change = 1;
        }

        private void UpTraineeButton_Click(object sender, RoutedEventArgs e)
        {
            Data.MainUserControl = new UpStudent();
            Data.Change = 1;
        }

        private void UpTesterButton_Click(object sender, RoutedEventArgs e)
        {
            Data.MainUserControl = new UpTester();
            Data.Change = 1;
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            Data.MainUserControl = new PrintOptions();
            Data.Change = 1;
        }

        private void DelTesterButton_Click(object sender, RoutedEventArgs e)
        {
            Data.MainUserControl = new DeleteTester();
            Data.Change = 1;
        }

        private void DelTraineeButton_Click(object sender, RoutedEventArgs e)
        {
            Data.MainUserControl = new DeleteTrainee();
            Data.Change = 1;
        }

        private void UpTestButton1_Click(object sender, RoutedEventArgs e)
        {
            Data.MainUserControl = new Grouping();
            Data.Change = 1;
        }
    }
}
