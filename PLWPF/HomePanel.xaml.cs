using System.Windows;
using System.Windows.Controls;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for HomePanel.xaml
    /// </summary>
    public partial class HomePanel : UserControl
    {
        public HomePanel()
        {

            InitializeComponent();

            if (Data.UserType == Data.Usertype.תלמיד)
            {
                delTesterButton.Visibility = Visibility.Collapsed;
                testerButton.Visibility = Visibility.Collapsed;
                UpTesterButton.Visibility = Visibility.Collapsed;
                UpTestButton.Visibility = Visibility.Collapsed;
                UpTraineeButton.Content = "עדכון פרטים אישיים";
            }
            if (Data.UserType == Data.Usertype.בוחן)
            {
                UpTesterButton.Content = "עדכון פרטים אישיים";
            }
        }

        private void TraineeButton_Click(object sender, RoutedEventArgs e)
        {
            Data.MainUserControl = new AddStudent();
             
        }

        private void TesterButton_Click(object sender, RoutedEventArgs e)
        {
            Data.MainUserControl = new AddTester();
             
        }

        private void AddTestButton_Click(object sender, RoutedEventArgs e)
        {
            Data.MainUserControl = new AddTest();
             
        }

        private void UpTraineeButton_Click(object sender, RoutedEventArgs e)
        {
            Data.MainUserControl = new UpStudent();
             
        }

        private void UpTesterButton_Click(object sender, RoutedEventArgs e)
        {
            Data.MainUserControl = new UpTester();
             
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            Data.MainUserControl = new PrintOptions();
             
        }

        private void DelTesterButton_Click(object sender, RoutedEventArgs e)
        {
            Data.MainUserControl = new DeleteTester();
             
        }

        private void DelTraineeButton_Click(object sender, RoutedEventArgs e)
        {
            Data.MainUserControl = new DeleteTrainee();
             
        }

        private void UpTestButton1_Click(object sender, RoutedEventArgs e)
        {
            Data.MainUserControl = new UpdateTest();
             
        }

        private void GroupingButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
