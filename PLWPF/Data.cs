using System;
using System.Windows;
using System.Windows.Input;

namespace PLWPF
{ 
    /// <summary>
/// this Class is made for holding data for the PL. it holds the user ID, user type, 
/// the user control currently showed in the main window etc.
/// </summary>
    public delegate void UserTypeChanged ();

    public static class Data
    {
        // an event for changing the user control in the main window, to navigate between screens
        public static event UserTypeChanged UserControlChanged;

        // user ID
        static string _userId = "";
        public static string UserID { get { return _userId; } set { _userId = value; } }

        // the user control instance, which we replace for screen - changing , and property for it,
        // that invoke an event that the main window is signed to handle, to replace current user control
        // in main window
        static System.Windows.Controls.UserControl _MainUserControl = null;
        public static System.Windows.Controls.UserControl MainUserControl
        {
            get
            { return _MainUserControl; }

            set
            {
                _MainUserControl = value;
                UserControlChanged?.Invoke();
            }
        }

        public static bool logged = false;

        public static int BackPage { get; set; }

        static Usertype _userType = Usertype.אורח;
        public static Usertype UserType { get { return _userType; } set { _userType = value; UserControlChanged?.Invoke(); } }

        public enum Usertype
        {
            אורח,
            בוחן,
            תלמיד,
            מנהל
        }

        /// <summary>
        /// Validating that all chars inserted to a field are numbers and not other chars.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void NumericCheck(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter || e.Key == Key.LeftShift || e.Key == Key.RightShift
                || e.Key == Key.LeftCtrl || e.Key == Key.LeftAlt || e.Key == Key.RightCtrl || e.Key == Key.RightAlt
                || e.Key == Key.End || e.Key == Key.Back || e.Key == Key.Home || e.Key == Key.Delete || e.Key== Key.CapsLock 
                || e.Key== Key.NumLock)
            { return; }

            try
            {
                string key = e.Key.ToString().TrimStart('D');
                int value = -1;
                bool convert = int.TryParse(key, out value);

                if (!convert)
                {
                    e.Handled = true;
                    MessageBox.Show("השדה " + " יכול להכיל מספרים בלבד!", "", MessageBoxButton.OK, MessageBoxImage.None,
                                        MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                }
            }

            catch (Exception)
            {
                return;
            }
        }
    }
}
