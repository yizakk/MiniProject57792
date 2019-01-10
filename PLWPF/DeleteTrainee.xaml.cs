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
using System.Xml.Linq;
using System.Xml;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for deletTrenee.xaml
    /// </summary>
    public partial class DeleteTrainee : UserControl
    {
        BL.IBL bl = BL.BlFactory.GetBL();
        public DeleteTrainee()
        {
            InitializeComponent();

            if (Data.UserType == Data.Usertype.תלמיד)
            {
                textBlock.Text = "באפשרותך להסיר רק את עצמך";
                comboBox.Visibility = Visibility.Collapsed;
                TesterId.Text = Data.UserID;
                TesterId.Visibility = Visibility.Visible;
            }

            else
            {
                var sourceList = bl.GetTraineesIdList();
                if (!sourceList.Any())
                {
                    MessageBox.Show("אין תלמידים במאגר", "", MessageBoxButton.OK, MessageBoxImage.None,
                        MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                    button.IsEnabled = false;
                    comboBox.IsEnabled = false;
                }
                comboBox.ItemsSource = sourceList;
                comboBox.SelectedIndex = 0;
            }

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Data.UserType == Data.Usertype.תלמיד)
            {
                TraineeDeletion();
                return;
            }

            if (comboBox.SelectedIndex != -1)
            {
                string IdString = comboBox.SelectedValue.ToString();
                IdString = IdString.Split(' ')[0];

                try
                {
                    bl.DelTrainee(IdString);
                }
                catch (Exceptions a)
                {
                    MessageBox.Show(a._message);
                    return;
                }

                int choice = (int)MessageBox.Show("המחיקה בוצעה בהצלחה, האם ברצונך לבצע עוד מחיקה?", "", MessageBoxButton.YesNo,
                                                    MessageBoxImage.Asterisk, MessageBoxResult.Yes, MessageBoxOptions.RtlReading);
                if (choice == 6)
                {
                    Data.MainUserControl = new DeleteTrainee();
                }
                else
                    Data.MainUserControl = new HomePanel();
            }
        }

        private void TraineeDeletion()
        {
            int choice = (int)MessageBox.Show("אתה עומד להסיר את עצמך מהמערכת, האם אתה בטוח שברצונך להמשיך?", "", MessageBoxButton.YesNo,
                                    MessageBoxImage.Asterisk, MessageBoxResult.Yes, MessageBoxOptions.RtlReading);

            if (choice == 6)
            {
                try
                {
                    bl.DelTrainee(Data.UserID);
                }
                catch (Exceptions a)
                {
                    MessageBox.Show(a._message);
                    return;
                }
                Data.UserType = Data.Usertype.אורח;
                Data.logged = false;
                MessageBox.Show("מחקת את עצמך ולכן גם נותקת מהמערכת!", "", MessageBoxButton.OK,
                                    MessageBoxImage.Asterisk, MessageBoxResult.OK, MessageBoxOptions.RtlReading);

                Data.MainUserControl = new Login();
            }
            else
            {
                // not doing any thing...
            }
            //try
            //{
            //    bl.DelTrainee(Data.UserID);
            //}
            //catch (Exceptions a)
            //{
            //    MessageBox.Show(a._message);
            //    return;
            //}
            //Data.logged = false;
            //Data.UserType = Data.Usertype.אורח;
            //Data.MainUserControl = new Login();
        }
    }
}
