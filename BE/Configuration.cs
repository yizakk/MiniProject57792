using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Configuration
    {
        public const string MasterPassword = "1234";
        public const int WorkDays = 5;
        public const int WorkHours = 7;
        public const int MinLessons=20;
        public const int TesterMaxAge = 0;
        public const int TraineeMinAge=18;
        public const int TesterMinAge=40;
        public const int MinDaysBetweenTests=7;
        public static int TestId = 10000000;
    }
}
