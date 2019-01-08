using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLWPF
{
    public delegate void UserTypeChanged ();

    public static class Data
    {

        public static event UserTypeChanged UserControlChanged;

        static string _userId = "";
        public static string UserID { get { return _userId; } set { _userId = value; } }

        public static System.Windows.Controls.UserControl _MainUserControl = null;
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
    }
}
