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

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Data.MainUserControl = new Login();

            GridMain.Children.Add(Data.MainUserControl);
            Data.UserControlChanged += UserTypeChanged; // adding local func. "userTypeChanged" to handle the change of user type

            //MessageBox.Show("בודק יקר! (במידה ואכן תראה הודעה זו...) המערכת שלנו אמורה לתמוך בהתחברות , " +
            //    "לכן אנו מעניקים לך גישה קלה - סיסמת מנהל == 1234 סיסמת בוחנים==123 "
            //    + "נסה להתחבר עם תז של תלמיד (למשל 202020101) או של בוחן (1111111) ולראות את השינויים בכל המסכים..."
            //    +"כרגע השארנו את חץ הניווט למעלה זמין לפעולה עבור כל המשתמשים, כדי שיהיה נוח וקל לערוך בדיקות, תוכל להשתמש בו כדי לעקוף את מסך ההתחברות "
            //    + "חלק מהפקדים עוד צריכים ליטוש , אך לפי בדיקות שערכנו התפקוד הבסיסי עובד מעולה"
            //    + " אשר על כן נודה לך אם לא תלך אחר מראה עיניים בלבד, אלא בעיקר אחר יעילות הקוד(השקענו בזה זמן מרובה) - לחסוך משאבים,"
            //    + "לא לזרוק אלגוריתמים בעלות שרק מיליארדר יכול לעמוד בו וכו'" 
            //    + " כמו כן ניסינו במהלך בניית הUI להמיר את כל התכנים שיהיו בלשון הקודש וגם בכיוון זרימה מותאם, "
            //    +"ייתכן ששכחנו אי-אילו הודעות קטנות פה ושם בBL  או בשכבות אחרות... עמך הסליחה וזה יתוקן מתישהו"
            //    +" תהנה מבדיקותייך! "
            //    , "הודעה לבודק", MessageBoxButton.OK,
            //    MessageBoxImage.Hand, MessageBoxResult.OK, MessageBoxOptions.RtlReading);
        }

        private void UserTypeChanged()
        {
                GridMain.Children.Clear();
                GridMain.Children.Add(Data.MainUserControl);
        }
        
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Data.logged)
            {
                switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
                {
                    case "ItemHome":
                        Data.MainUserControl = new Login();
                        GridMain.Children.Clear();
                        GridMain.Children.Add(Data.MainUserControl);

                        break;
                    case "ItemCreate":
                        Data.MainUserControl = new AddStudent();
                        GridMain.Children.Clear();
                        GridMain.Children.Add(Data.MainUserControl);
                        break;

                    case "move":
                        Data.MainUserControl = new PrintOptions();
                        GridMain.Children.Clear();
                        GridMain.Children.Add(Data.MainUserControl);
                        break;
                    default:
                        break;
                }
            }

            else
            {
                MessageBox.Show("אתה חייב להיות מחובר כדי לנווט!", "נדרשת התחברות", MessageBoxButton.OK, MessageBoxImage.Error,
                    MessageBoxResult.OK, MessageBoxOptions.RtlReading);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch(Data.BackPage)
            {
                case 0:
                    Data.MainUserControl = new HomePanel();
                    break;
            }
        }

    }
}
