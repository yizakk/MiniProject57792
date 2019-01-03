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
    /// Interaction logic for PrintAllTests.xaml
    /// </summary>
    public partial class PrintAllTests : UserControl
    {
        public PrintAllTests()
        {
            InitializeComponent();

            BL.IBL bl = BL.BlFactory.GetBL();
            testDataGrid.DataContext = bl.GetTests();
        }

    }
}
