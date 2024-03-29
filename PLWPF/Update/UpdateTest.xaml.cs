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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for UpdateTest.xaml
    /// </summary>
    public partial class UpdateTest : UserControl
    {
        BE.Address Address = new BE.Address();
        BE.Test TestItem = new BE.Test();
        BL.IBL bl = BL.BlFactory.GetBL();

        public UpdateTest()
        {
            InitializeComponent();

            var sourceList = bl.GetTestsIdList();
            if (!sourceList.Any())
            {
                MessageBox.Show("אין טסטים במאגר", "", MessageBoxButton.OK, MessageBoxImage.None,
                    MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                button.IsEnabled = false;
                comboBox.IsEnabled = false;
            }
            button.IsEnabled = false;

            comboBox.ItemsSource = sourceList;
            grid2.DataContext = Address;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox.SelectedIndex != -1)
            {
                button.IsEnabled = true;
                TestItem = bl.FindTest(int.Parse(comboBox.SelectedValue.ToString()));
                grid1.DataContext = TestItem;
                ParametersGrid.DataContext = TestItem.Paramet;
                grid2.DataContext = TestItem.BeginAddress;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(CityTextBox.Text))
            {
                MessageBox.Show("נא להכניס את שם העיר", "", MessageBoxButton.OK, MessageBoxImage.None,
                    MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                return;
            }
            if (string.IsNullOrEmpty(streetTextBox.Text))
            {
                MessageBox.Show("נא להכניס את שם הרחוב", "", MessageBoxButton.OK, MessageBoxImage.None,
                    MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                return;
            }
            if (string.IsNullOrEmpty(buildingNumberTextBox.Text))
            {
                MessageBox.Show("נא להכניס את מספר בנין", "", MessageBoxButton.OK, MessageBoxImage.None,
                    MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                return;
            }
            try
            {
                TestItem.BeginAddress = Address;
                //string temp = cityTextBox.Text + " " + streetTextBox.Text + " " + buildingNumberTextBox;
                //TestItem.adress = temp;
                bl.UpdateTest(TestItem);
            }

            catch (BE.MyExceptions cat)
            {
                MessageBox.Show(cat._message, "", MessageBoxButton.YesNo,
                                MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.RtlReading);
                return;
            }
            int choice = (int)MessageBox.Show("המבחן עודכן בהצלחה, האם ברצונך לעדכן עוד מבחן?", "", MessageBoxButton.YesNo,
                                MessageBoxImage.Asterisk, MessageBoxResult.None, MessageBoxOptions.RtlReading);
            if (choice == 6)
            {
                Data.MainUserControl = new UpdateTest();
            }
            else
                Data.MainUserControl = new HomePanel();

        }

        private void KeyDownCheckIfNotNumber(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Data.NumericCheck(sender, e);
        }
    }
}
