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

        private void UserTypeChanged()
        {
                GridMain.Children.Clear();
                GridMain.Children.Add(Data.MainUserControl);
        }
        
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Data.logged)
            {
                switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
                {
                    case "ItemHome":
                        Data.MainUserControl = new Login();
                        GridMain.Children.Clear();
                        GridMain.Children.Add(Data.MainUserControl);

                        break;
                    case "ItemCreate":
                        Data.MainUserControl = new AddStudent();
                        GridMain.Children.Clear();
                        GridMain.Children.Add(Data.MainUserControl);
                        break;

                    case "move":
                        Data.MainUserControl = new PrintOptions();
                        GridMain.Children.Clear();
                        GridMain.Children.Add(Data.MainUserControl);
                        break;
                    default:
                        break;
                }
            }

            else
            {
                MessageBox.Show("אתה חייב להיות מחובר כדי לנווט!", "נדרשת התחברות", MessageBoxButton.OK, MessageBoxImage.Error,
                    MessageBoxResult.OK, MessageBoxOptions.RtlReading);
            }
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch(Data.BackPage)
            {
                case 0:
                    Data.MainUserControl = new HomePanel();
                     
                    break;
            }
        }

    }
}
