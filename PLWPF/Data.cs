﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PLWPF
{
    public delegate void UserTypeChanged ();

    public static class Data
    {

        public static event UserTypeChanged UserControlChanged;

        static string _userId = "";
        public static string UserID { get { return _userId; } set { _userId = value; } }

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


        public static void NumericCheck(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter || e.Key == Key.LeftShift || e.Key == Key.RightShift
                || e.Key == Key.LeftCtrl || e.Key == Key.LeftAlt || e.Key == Key.RightCtrl || e.Key == Key.RightAlt
                || e.Key == Key.End || e.Key == Key.Back || e.Key == Key.Home || e.Key == Key.Delete)
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
