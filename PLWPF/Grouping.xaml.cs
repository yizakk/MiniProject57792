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
    /// Interaction logic for Grouping.xaml
    /// </summary>
    public partial class Grouping : UserControl
    {
       
        BL.IBL bl = BL.BlFactory.GetBL();


        public Grouping()
        {
            InitializeComponent();
        }

        private void TraineeButton_Click(object sender, RoutedEventArgs e)
        {
           // Data.MainUserControl = new stam();
            

           

        }
    }
}
