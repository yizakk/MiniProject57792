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
    /// Interaction logic for PrintAllTesters.xaml
    /// </summary>
    public partial class PrintAllTesters : UserControl
    {
        public PrintAllTesters()
        {
            InitializeComponent();

            BL.IBL bl = BL.BlFactory.GetBL();
            testerListView.DataContext = bl.GetTesters();
        }
        public PrintAllTesters(int index)
        {
            InitializeComponent();

            BL.IBL bl = BL.BlFactory.GetBL();
            if (index == 0)
            {
                testerListView.DataContext = bl.GetTesters();
            }
            else if(index == 1)
            {
                foreach (var item in bl.TestersGroupedByCarType(true))
                {
                    foreach (var tester in item)
                    {
                        testerListView.Items.Add(tester);
                    }
                    var split = new GridSplitter();

                    testerListView.Items.Add(new GridSplitter());

                }
            }
        }


    }
}
