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
    /// <summary>
    /// Interaction logic for AddTester.xaml
    /// </summary>
    public partial class AddTester : UserControl
    {
        Tester TempTester;
        BE.Address address = new Address();
        BL.IBL bl;
        public AddTester()
        {
            InitializeComponent();

            bl = BL.BlFactory.GetBL();
            TempTester = new Tester
            {
                BirthDate = DateTime.Now.AddYears(-Configuration.TesterMinAge)
            };

            try
            {
                grid1.DataContext = TempTester;
                grid2.DataContext = address;
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }

            TempTester.Id = Data.UserID;
            car_typeComboBox.ItemsSource = Enum.GetValues(typeof(CarType));
            genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TempTester.Address = address;
            CheckAndAdd();
        }

        private void CheckAndAdd()
        {
            if (TempTester.Id.Length == 0)
            {
                MessageBox.Show("נא להכניס תעודת זהות", "", MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK
                    , MessageBoxOptions.RtlReading);
                idTextBox.Clear();
                return;
            }

            TimeSpan a = DateTime.Now - birthDateDatePicker.SelectedDate.Value;
            if (a.Days / 365 < 40)
            {
                MessageBox.Show("אין אפשרות להוסיף בוחן שגילו מתחת ל" +Configuration.TesterMinAge.ToString() +" שנים");
                birthDateDatePicker.SelectedDate = DateTime.Parse("01 01 2000");

                return;
            }

            if (phoneNumberTextBox.GetLineLength(0) > 0 && phoneNumberTextBox.GetLineLength(0) < 9)
            {
                MessageBox.Show("מספר טלפון לא יכול להכיל פחות מ9 ספרות");
                return;
            }

            int i = 0, j = 0 , k=1;
            foreach(var item in ScheduleGrid.Children)
            {
                if(item is CheckBox)
                {
                    var value = item as CheckBox;
                    if (value.Name == "checkBox" + k++)
                    {
                        TempTester.WorkSchedule(i, j++, value.IsChecked);
                        if (j == 7)
                        {
                            j = 0;
                            i++;
                        }
                    }
                }
            }

            TempTester.Seniority = int.Parse(seniorityTextBox.Text);
            try
            {
                bl.AddTester(TempTester);
            }
            catch (MyExceptions c)
            {
                MessageBox.Show(c._message);
                return;
            }

            int choice = (int)MessageBox.Show("הבוחן נוסף בהצלחה , האם ברצונך להוסיף עוד בוחן?", "", MessageBoxButton.YesNo,
                MessageBoxImage.Asterisk, MessageBoxResult.None, MessageBoxOptions.RtlReading);
            if (choice == 6)
            {
                Data.MainUserControl = new AddTester();
            }
            else
                Data.MainUserControl = new HomePanel();
        }

        private void ScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(e.NewValue == -1)
            {
                SeniorityScrollBar.Value += 1;
            }
            if (seniorityTextBox.Text.Any())
            {
                int.TryParse(seniorityTextBox.Text, out int Tempsum);
                SeniorityScrollBar.Value = Tempsum;
                seniorityTextBox.Text = ((int) (e.NewValue- e.OldValue) + Tempsum).ToString();
            }
            else
                seniorityTextBox.Text = SeniorityScrollBar.Value.ToString();
        }

        private void KeyDownCheckIfNotNumber(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Data.NumericCheck(sender,e);
        }
    }
}

