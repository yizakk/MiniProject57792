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
    /// Interaction logic for stam.xaml
    /// </summary>
    public partial class stam : UserControl
    {
        BL.IBL bl = BL.BlFactory.GetBL();
        Trainee tempTrainee;
        public stam()
        {
            InitializeComponent();
            MessageBox.Show("Testers grouped by car type list:");
            foreach (var item in bl.TestersGroupedByCarType(true))
            {
                ////Console.WriteLine();
                ////MessageBox.Show("Car type " + item.First().Car_type + " testers:");
                Data.MainUserControl = new noIdea();

                foreach (var tester1 in item)
                {
                    MessageBox.Show(tester1.ToString());

                }
                Console.WriteLine();

            }
            Console.WriteLine();
        }
    }
}
