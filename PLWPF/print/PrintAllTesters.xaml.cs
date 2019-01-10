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
        /// <summary>
        /// C-tor for printing without grouping
        /// </summary>
        public PrintAllTesters()
        {
            InitializeComponent();

            BL.IBL bl = BL.BlFactory.GetBL();
            testerListView.DataContext = bl.GetTesters();
        }

        /// <summary>
        /// C-tor switching which grouping to create in the list...
        /// </summary>
        /// <param name="index"></param>
        public PrintAllTesters(int index)
        {
            InitializeComponent();

            BL.IBL bl = BL.BlFactory.GetBL();
            if (index == 0)
            {
                testerListView.DataContext = bl.GetTesters();
            }
            else if (index == 2)
            {
                var DataSource = bl.TestersOver60YO(); ;
                if(!DataSource.Any())
                {
                    MessageBox.Show("אין בוחנים מתאימים לתנאי זה", "", MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.OK
                                        , MessageBoxOptions.RtlReading);
                    return;
                }
                testerListView.DataContext = DataSource;
            }


            else if (index == 1)
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
