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
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for DeletTeste.xaml
    /// </summary>
    public partial class DeleteTester : UserControl
    {

        BL.IBL bl = BL.BlFactory.GetBL();


        public DeleteTester()
        {
            InitializeComponent();
            var sourceList = bl.GetTesterIdList();
            if (!sourceList.Any())
            {
                MessageBox.Show("אין בוחנים במאגר", "", MessageBoxButton.OK, MessageBoxImage.None,
                                    MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                button.IsEnabled = false;
                comboBox.IsEnabled = false;
            }

            comboBox.ItemsSource = sourceList;
            comboBox.SelectedIndex = 0;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DelTester(comboBox.SelectedValue.ToString().Split(' ')[0]);
            }
            catch ( Exceptions a)
            {
                MessageBox.Show(a._message);

            }

            int choice;

            choice = (int)MessageBox.Show("המחיקה בוצעה בהצלחה, האם ברצונך לבצע עוד מחיקה?", "", MessageBoxButton.YesNo,
              MessageBoxImage.Asterisk, MessageBoxResult.Yes, MessageBoxOptions.RtlReading);
            if (choice == 6)
            {
                Data.MainUserControl = new DeleteTester();
                Data.Change = 1;
            }
        }
    }
}
