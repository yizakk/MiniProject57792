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
    /// Interaction logic for TestersByCarType.xaml
    /// </summary>
    public partial class TestersByCarType : UserControl
    {
        BL.IBL bl = BL.BlFactory.GetBL();

        public TestersByCarType()
        {
            InitializeComponent();

            //testerDataGrid.DataContext = bl.TestersGroupedByCarType(true);
            foreach (var item in bl.TestersGroupedByCarType(true))
            {
                foreach( var tester in item)
                {
                    testerDataGrid.Items.Add(tester);
                }
                var split = new GridSplitter();
                
                testerDataGrid.Items.Add(new GridSplitter());
                
            }

        }

 
    }
}
