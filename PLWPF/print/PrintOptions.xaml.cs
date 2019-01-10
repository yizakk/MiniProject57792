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
    /// Interaction logic for PrintOptions.xaml
    /// </summary>
    public partial class PrintOptions : UserControl
    {
        public PrintOptions()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var input = comboBox.SelectedIndex;
            Grid2.Children.Clear();
            switch (input)
            {
                case 0:
                    Grid2.Children.Add(new printAllStudent());
                    break;
                case 1:
                    Grid2.Children.Add(new printAllStudent(1));
                    break;
                case 2:
                    Grid2.Children.Add(new printAllStudent(2));
                    break;
                case 3:
                    Grid2.Children.Add(new printAllStudent(3));
                    break;
                case 4:
                    Grid2.Children.Add(new PrintAllTesters(0));
                    break;
                case 5:
                    Grid2.Children.Add(new PrintAllTesters(2));
                    break;
                case 6:
                    Grid2.Children.Add(new PrintAllTesters(1));
                    break;
                case 7:
                    Grid2.Children.Add(new PrintAllTests());
                    break;
            }
        }
    }
}
