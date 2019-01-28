using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLWPF
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Data.MainUserControl = new Login();
           
            GridMain.Children.Add(Data.MainUserControl);
            Data.UserControlChanged += UserTypeChanged; // adding local func. "userTypeChanged" to handle the change of user type
        }

        private void UserTypeChanged() // to respond to a user control change in one of the pages - and update it in this main window
        {
                GridMain.Children.Clear();
                GridMain.Children.Add(Data.MainUserControl);
        }
        
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible; // open menu button
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed; // close menu button
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                MessageBox.Show("התפריט בשלבי בנייה","", MessageBoxButton.OK, MessageBoxImage.Error,
                    MessageBoxResult.OK, MessageBoxOptions.RtlReading);
            //if (Data.logged)
            //{
            //    switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            //    {
            //        case "ItemHome":
            //            Data.MainUserControl = new Login();
            //            GridMain.Children.Clear();
            //            GridMain.Children.Add(Data.MainUserControl);

            //            break;
            //        case "ItemCreate":
            //            Data.MainUserControl = new AddStudent();
            //            GridMain.Children.Clear();
            //            GridMain.Children.Add(Data.MainUserControl);
            //            break;

            //        case "move":
            //            Data.MainUserControl = new PrintOptions();
            //            GridMain.Children.Clear();
            //            GridMain.Children.Add(Data.MainUserControl);
            //            break;
            //        default:
            //            break;
            //    }
            //}

            //else
            //{
            //}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch(Data.BackPage) // right now - only going to home panel. in the future- going to the page you came from
            {
                case 0:
                    Data.MainUserControl = new HomePanel();
                    break;
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); // sutting the program
        }

        private void HomePanelButton_Click(object sender, RoutedEventArgs e)
        {
            Data.MainUserControl = new HomePanel();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            Data.UserID = "";
            Data.UserType = Data.Usertype.אורח; // logging out button
            Data.logged = false;
            Data.MainUserControl = new Login();
        }
    }
}
