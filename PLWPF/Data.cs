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

        public static event UserTypeChanged TypeChanged;

        static string _userId = "";
        public static string UserID { get { return _userId; } set { _userId = value; } }

        public static System.Windows.Controls.UserControl MainUserControl = null;

        public static bool logged = false;

        public static int BackPage { get; set; }

        static Usertype _userType = 0;
        public static Usertype UserType { get { return _userType; } set { _userType = value; TypeChanged?.Invoke(); } }

        static int _change;
        public static int Change { get { return _change; } set { TypeChanged?.Invoke(); } }

        public enum Usertype
        {
            בוחן,
            תלמיד,
            מנהל
        }
    }
}
