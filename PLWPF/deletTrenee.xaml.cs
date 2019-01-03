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
            comboBox.ItemsSource = bl.GetTraineesIdList();

            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            string a = comboBox.SelectedValue.ToString();
            tempTrainee = bl.FindTrainee(a);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // bl.DelTrainee(tempTrainee);

            try
            {
              bl.DelTrainee(tempTrainee);
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
