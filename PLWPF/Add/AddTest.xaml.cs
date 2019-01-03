using System;
using System.Windows;
using System.Windows.Controls;
using BE;
using PLWPF.UC;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddTest.xaml
    /// </summary>
    public partial class AddTest : UserControl
    {

        Test TempTest;
        BL.IBL bl;
        public AddTest()
        {
            InitializeComponent();
            bl = BL.BlFactory.GetBL();
            TempTest = new Test();

            TempTest.Date = DateTime.Now;


            int[] array = new int[Configuration.WorkHours];

            for(int i=0;i<Configuration.WorkHours;i++)
            {
                array[i] = i + 9;
            }
            TimeComboBox.ItemsSource = array;

            try
            {
                FormGrid.DataContext = TempTest;
            }

            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TempTest.Date = new DateTime(TempTest.Date.Year, TempTest.Date.Month, TempTest.Date.Day, (int)TimeComboBox.SelectedValue, 0, 0);
            try
            {
                bl.AddTest(TempTest);
            }

            catch (Exceptions a)
            {
                if (a.SuggestedTest != null)
                {
                    int x = (int)MessageBox.Show(a._message, "הצעת טסט חלופי", MessageBoxButton.YesNo, MessageBoxImage.Question,
                        MessageBoxResult.No, MessageBoxOptions.RtlReading);
                    if (x == 6)
                    {
                        try
                        {
                            bl.AddTest(a.SuggestedTest);
                        }
                        catch (Exceptions cat)
                        {
                            MessageBox.Show(cat._message, "", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK,
                                MessageBoxOptions.RtlReading);
                        }
                    }
                    else
                    {
                        MessageBox.Show("נסה להוסיף טסט אחר ידנית");
                    }
                }
                else
                    MessageBox.Show(a._message);
            }

            if(Data.UserType==1)
            {
                Data.MainUserControl = new HomePanel();
                Data.Change = 1;
            }
        //    else

        }

    }
}

/*
 * 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TempTrainee.Id.Length < 7)
            {
                MessageBox.Show("תעודת זהות לא יכולה להכיל פחות מ7 ספרות");
                idTextBox.Clear();
                return;
            }
            TimeSpan a = DateTime.Now - birthDateDatePicker.SelectedDate.Value;
            if (a.Days / 365 < 18)
            {
                MessageBox.Show("אין אפשרות להוסיף תלמיד שטרם מלאו לו 18 שנים");
                birthDateDatePicker.SelectedDate = DateTime.Parse("01 01 2000");

                return;
            }

            if (phoneNumberTextBox.GetLineLength(0) > 0 && phoneNumberTextBox.GetLineLength(0) < 10)
            {
                MessageBox.Show("מספר טלפון לא יכול להכיל פחות מ10 ספרות");
                return;
            }


            string tempAddress = AddresstextBox.GetLineText(0);
            MessageBox.Show(tempAddress);

            try
            {
                bl.AddTrainee(TempTrainee);
            }
            catch(Exception c)
            {
                if (c is Exceptions )
                {
                    var b = (Exceptions)c;
                    MessageBox.Show( b._message);
                    
                    return;

                }

                MessageBox.Show(c.Message);
                return;

            }
            MessageBox.Show("התלמיד הוסף בהצלחה , האם ברצונך להוסיף עוד תלמיד?", "",MessageBoxButton.YesNo ,MessageBoxImage.Information);
        }
        }
}
 */
