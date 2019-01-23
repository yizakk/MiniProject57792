﻿using System.Windows;
using System.Windows.Controls;


namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddPerson.xaml
    /// </summary>
    public partial class AddPerson : UserControl
    {
        int count = 0;
        public AddPerson()
        {
            InitializeComponent();
        }

        private void TraineeButton_Click(object sender, RoutedEventArgs e)
        {
            Data.MainUserControl = new AddStudent();
            Data.UserType = Data.Usertype.תלמיד;
             
        }

        private void TesterButton_Click(object sender, RoutedEventArgs e)
        {
            label.Visibility = Visibility.Visible;
            buttonIfIsTester.Visibility = Visibility.Visible;
            textBox.Visibility = Visibility.Visible;

            traineeButton.Visibility = Visibility.Hidden;
            testerButton.Visibility = Visibility.Hidden;

            textBox.Focus();
        }

        private void ButtonIfIsTester_Click(object sender, RoutedEventArgs e)
        {
            if (count==3)
            {
                MessageBox.Show("גישתך נחסמה");
                return;
            }

            if (BE.Encryption.VerifyHashPassword(textBox.Password, BE.Configuration.TesterPassword))
            {
                Data.MainUserControl = new AddTester();
                Data.UserType = Data.Usertype.בוחן;
            }

            else
            {
                count++;

                if (count == 3)
                {
                    MessageBox.Show("גישתך נחסמה");
                    return;
                    
                }
                MessageBox.Show("!קוד שגוי");
                textBox.Clear();
                textBox.Focus();
            }
        }

        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                ButtonIfIsTester_Click(null, e);
        }
    }
}
