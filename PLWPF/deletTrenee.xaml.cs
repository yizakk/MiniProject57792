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
    /// Interaction logic for deletTrenee.xaml
    /// </summary>
    public partial class deletTrenee : UserControl
    {
        BL.IBL bl = BL.BlFactory.GetBL();
        Trainee tempTrainee;
        public deletTrenee()
        {
            InitializeComponent();
            var sourceList = bl.GetTraineesIdList();
            if(!sourceList.Any())
            {
                MessageBox.Show("אין תלמידים במאגר", "", MessageBoxButton.OK, MessageBoxImage.None,
                    MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                button.IsEnabled = false;
                comboBox.IsEnabled = false;
            }
            comboBox.ItemsSource = sourceList;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
              bl.DelTrainee(comboBox.SelectedValue.ToString());
            }
            catch (Exceptions a)
            {
                MessageBox.Show(a._message);
                return;
            }

            MessageBox.Show("העידכון הסתיים בהצלחה");



        }
    }
}
