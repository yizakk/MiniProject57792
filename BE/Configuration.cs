using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BE
{
    // This configuration class holds data for all the program , for easy future updating
    // It also holds the passwords for managing the program - management password
    // and a password for adding tester to the DS
    public class Configuration
    {
        // the above 2 functions are made to get a string and encrypt it into a binary sequence
        // and so to decrypt it from a binary sequence to a string
        public static string Decrypt(string value)
        {
            if (true)
            {
                List<byte> byteList = new List<byte>();

                for (int i = 0; i < value.Length; i += 8)
                {
                    byteList.Add(Convert.ToByte(value.Substring(i, 8), 2));
                }
                return Encoding.ASCII.GetString(byteList.ToArray());
            }
           
        }
        private static string Encrypt(string value)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in value.ToCharArray())
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }
            return sb.ToString();
        }
        // returning the private fields of the passwords, where the passwords are holded encrypted,
        // for saving it into the xml file
        public static string EncryptedMasterPass { get => masterPassword; }
        public static string EncryptedTesterPass { get => testerPassword; }
        // the property for the two passwords getting a string , and encrypting it using the function
        // to set the value of the matching private fields. when returning it back- it uses the decrypt function
        public static string MasterPassword
        {
            get
            {
                return Decrypt(masterPassword);
            }
            set
            {
                masterPassword = Encrypt(value);
            }
        }
        public static string TesterPassword
        {
            get
            {
                return Decrypt(testerPassword);
            }
            set
            {
                testerPassword = Encrypt(value);
            }
        }
        public static int WorkDays { get => workDays; set => workDays = value; }
        public static int WorkHours { get => workHours; set => workHours = value; }
        public static int MinLessons { get => minLessons; set => minLessons = value; }
        public static int TesterMaxAge { get => testerMaxAge; set => testerMaxAge = value; }
        public static int TraineeMinAge { get => traineeMinAge; set => traineeMinAge = value; }
        public static int TesterMinAge { get => testerMinAge; set => testerMinAge = value; }
        public static int MinDaysBetweenTests { get => minDaysBetweenTests; set => minDaysBetweenTests = value; }
        public static int TestId { get => testId; set => testId = value; }

        private static string masterPassword = "00110001001100100011001100110100";
        private static string testerPassword = "";
        private static int workDays = 5;
        private static int workHours = 7;
        private static int minLessons = 20;
        private static int testerMaxAge = 0;
        private static int traineeMinAge = 18;
        private static int testerMinAge = 40;
        private static int minDaysBetweenTests = 7;
        private static int testId = 10000000;

    }
}
