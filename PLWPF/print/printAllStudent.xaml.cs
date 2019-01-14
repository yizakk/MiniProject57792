using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for printAllStudent.xaml
    /// </summary>
    public partial class printAllStudent : UserControl
    {
            BL.IBL bl = BL.BlFactory.GetBL();
        public printAllStudent()
        {
            InitializeComponent();
            traineeListView.DataContext = bl.GetTraineeList();
        }

        public printAllStudent(int index)
        {
            InitializeComponent();

            if (index==1) // Getting all Trainees Grouped by teacher name
            {
                foreach (var item in bl.TraineesGroupedByTeacherName(true))
                {
                    //var split = new TextBlock();
                    //split.HorizontalAlignment = HorizontalAlignment.Center;
                    //split.VerticalAlignment = VerticalAlignment.Center;
                    //split.Width = 200;
                    //split.FontSize = 20;
                    //split.Text = item.First().Car_type.ToString();


                    foreach (var tester in item)
                    {
                        traineeListView.Items.Add(tester);
                    }
                    traineeListView.Items.Add(new { ItemName = item.First().CarType.ToString() });
                   
                }
            }
            else if( index ==2)
            {
                foreach (var item in bl.TraineesGroupedBySchoolName(true))
                {
                    foreach (var tester in item)
                    {
                        traineeListView.Items.Add(tester);
                    }

                    traineeListView.Items.Add(new GridSplitter());
                }
            }

            else if (index == 3)
            {
                foreach (var item in bl.TraineesGroupedByNumOfTestsDone(true))
                {
                    foreach (var tester in item)
                    {
                        traineeListView.Items.Add(tester);
                    }
                    var split = new GridSplitter();

                    traineeListView.Items.Add(new GridSplitter());
                }
            }
        }

        private void TraineeListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PopJump.IsOpen = true;
        }

        private void PopJump_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PopJump.IsOpen = false;
        }
    }
}
