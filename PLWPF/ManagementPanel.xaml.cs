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
    /// Interaction logic for ManagementPanel.xaml
    /// </summary>
    public partial class ManagementPanel : UserControl
    {
        public ManagementPanel()
        {
            InitializeComponent();

            FieldsPanel.DataContext = typeof(BE.Configuration).GetProperties();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BE.Configuration.MasterPassword = MasterPass.Password;
            BL.BlFactory.GetBL().UpdateConfig();
        }
    }
}
