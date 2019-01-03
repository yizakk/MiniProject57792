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
    /// Interaction logic for stam.xaml
    /// </summary>
    public partial class stam : UserControl
    {
        public stam()
        {
            InitializeComponent();
            BL.IBL bl = BL.BlFactory.GetBL();

            testerListView.DataContext = bl.GetTesters();
        }

      
    }
}
