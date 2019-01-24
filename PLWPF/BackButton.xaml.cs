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
    /// <summary>
    /// Interaction logic for BackButton.xaml
    /// </summary>
    public partial class BackButton : UserControl
    {
        public BackButton()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // by clicking the button you are disconnected from the system.
            Data.UserID = "";
            Data.UserType = Data.Usertype.אורח;
            Data.logged = false;
            Data.MainUserControl = new Login();
        }
    }
}
