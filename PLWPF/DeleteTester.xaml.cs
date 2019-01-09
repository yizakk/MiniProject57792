﻿using System;
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
    public partial class DeleteTester : UserControl
    {

        BL.IBL bl = BL.BlFactory.GetBL();
        public static string Id;
        public DeleteTester()
        {
            InitializeComponent();
            if (Data.UserType == Data.Usertype.בוחן)
            {
                Id = Data.UserID;
                comboBox.Visibility = Visibility.Collapsed;
                TesterId.Text = Id;
                TesterId.Visibility = Visibility.Visible;

                //var item = new ComboBoxItem();
                //item.Content = Data.UserID;
                //comboBox.Items.Add(item);
                //comboBox.SelectedIndex = 0;
                //comboBox.IsEnabled = false;
                return;
            }

            else
            {
                var sourceList = bl.GetTesterIdList();
                if (!sourceList.Any())
                {
                    MessageBox.Show("אין בוחנים במאגר", "", MessageBoxButton.OK, MessageBoxImage.None,
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

            try
            {
                if (Data.UserType == Data.Usertype.בוחן)
                {
                    bl.DelTester(Data.UserID);
                }
                else
                bl.DelTester(comboBox.SelectedValue.ToString().Split(' ')[0]);
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
                Data.MainUserControl = new DeleteTester();
            }

            else
            {
                Data.MainUserControl = new  HomePanel();
            }
        }
    }
}
