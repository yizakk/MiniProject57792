
namespace BE
{
    public class Configuration
    {
        private static string masterPassword = "1234";
        private static string testerPassword = "123";
        private static int workDays = 5;
        private static int workHours = 7;
        private static int minLessons = 20;
        private static int testerMaxAge = 0;
        private static int traineeMinAge = 18;
        private static int testerMinAge = 40;
        private static int minDaysBetweenTests = 7;
        private static int testId = 10000000;

        public static string MasterPassword { get => masterPassword; set {masterPassword = value; } }
        public static string TesterPassword { get => testerPassword; set => testerPassword = value; }
        public static int WorkDays { get => workDays; set => workDays = value; }
        public static int WorkHours { get => workHours; set => workHours = value; }
        public static int MinLessons { get => minLessons; set => minLessons = value; }
        public static int TesterMaxAge { get => testerMaxAge; set => testerMaxAge = value; }
        public static int TraineeMinAge { get => traineeMinAge; set => traineeMinAge = value; }
        public static int TesterMinAge { get => testerMinAge; set => testerMinAge = value; }
        public static int MinDaysBetweenTests { get => minDaysBetweenTests; set => minDaysBetweenTests = value; }
        public static int TestId { get => testId; set => testId = value; }
    }
}
